using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Pages.ProjectCounter;
using Project.CSS.Revise.Web.Models.Pages.Shop_Event;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IProjectCounterRepo
    {
        public List<ProjectCounterMappingModel.ListData> GetListsProjectCounterMapping(ProjectCounterMappingModel.FilterData filter);
        public CreateCounterRequest.Response CreateEventsAndShops(CreateCounterRequest model);
        Task<GetdataEditProjectCounter.ProjectCounterDetailVm?> GetProjectCounterDetailAsync(int id);
    }
    public class ProjectCounterRepo : IProjectCounterRepo
    {
        private readonly CSSContext _context;

        public ProjectCounterRepo(CSSContext context)
        {
            _context = context;
        }
        public List<ProjectCounterMappingModel.ListData> GetListsProjectCounterMapping(ProjectCounterMappingModel.FilterData filter)
        {
            List<ProjectCounterMappingModel.ListData> result = new List<ProjectCounterMappingModel.ListData>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "";
                sql = @"
                                -- ===== TEST CASE =====
                                --DECLARE @L_Bu        NVARCHAR(100) = '';
                                --DECLARE @L_ProjectID NVARCHAR(100) = '';
                                --DECLARE @L_QueueType INT = -1;
                                -- ===== TEST CASE =====

                                SELECT 
                                     T1.[ID] 
                                    ,T1.[ProjectID] 
                                    ,T2.ProjectName 
                                    ,T1.[QueueTypeID] 
                                    ,QTN.[Name] AS QueueTypeName 
                                    ,T1.[StartCounter]
                                    ,T1.[EndCounter] 
                                    ,ISNULL(SubBankStaff.COUNT_Bank, 0)  AS COUNT_Bank 
                                    ,ISNULL(SubBankStaff.COUNT_Staff, 0) AS COUNT_Staff 
                                FROM [TR_ProjectCounter_Mapping] AS T1 WITH (NOLOCK)
                                LEFT JOIN tm_Project              AS T2  WITH (NOLOCK) ON T1.ProjectID   = T2.ProjectID
                                LEFT JOIN tm_BUProject_Mapping    AS BPM WITH (NOLOCK) ON T1.ProjectID   = BPM.ProjectID
                                LEFT JOIN tm_Ext                  AS QTN WITH (NOLOCK) ON T1.QueueTypeID = QTN.ID
                                LEFT JOIN (
                                    SELECT 
                                         RPS.[ProjectID]
                                        ,COUNT(RPS.[BankID])  AS COUNT_Bank
                                        ,COUNT(RPS.[Staff])   AS COUNT_Staff
                                    FROM [TR_Register_ProjectBankStaff] AS RPS WITH (NOLOCK)
                                    GROUP BY RPS.[ProjectID]
                                ) AS SubBankStaff
                                    ON T1.[ProjectID] = SubBankStaff.[ProjectID]
                                   AND T1.[QueueTypeID] = 48
                                WHERE T1.[FlagActive] = 1
                                  AND (@L_Bu = '' OR (',' + @L_Bu + ',' LIKE '%,' + CONVERT(VARCHAR(20), BPM.BUID) + ',%'))
                                  AND (@L_ProjectID = '' OR (',' + @L_ProjectID + ',' LIKE '%,' + T1.ProjectID + ',%'))
                                  AND (@L_QueueType = -1 OR T1.QueueTypeID = @L_QueueType);
                           "
                                    ;

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_Bu", filter.L_Bu ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", filter.L_ProjectID ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_QueueType", filter.L_QueueType ?? -1));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ProjectCounterMappingModel.ListData
                            {
                                ID = Commond.FormatExtension.Nulltoint(reader["ID"]) ,
                                ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]),
                                ProjectName = Commond.FormatExtension.NullToString(reader["ProjectName"]),
                                QueueTypeID = Commond.FormatExtension.Nulltoint(reader["QueueTypeID"]),
                                QueueTypeName = Commond.FormatExtension.NullToString(reader["QueueTypeName"]),
                                StartCounter = Commond.FormatExtension.Nulltoint(reader["StartCounter"]),
                                EndCounter = Commond.FormatExtension.Nulltoint(reader["EndCounter"]),
                                COUNT_Bank = Commond.FormatExtension.Nulltoint(reader["COUNT_Bank"]),
                                COUNT_Staff = Commond.FormatExtension.Nulltoint(reader["COUNT_Staff"])
                            });
                        }
                    }
                }
            }


            return result;
        }

        public CreateCounterRequest.Response CreateEventsAndShops(CreateCounterRequest model)
        {
            var response = new CreateCounterRequest.Response();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // ✅ validate ขั้นต้น
                    if (model == null) throw new ArgumentNullException(nameof(model));
                    if (model.ProjectIds == null || model.ProjectIds.Count == 0)
                        throw new ArgumentException("ProjectIds is required.");
                    if (model.CounterTypeIds == null || model.CounterTypeIds.Count == 0)
                        throw new ArgumentException("CounterTypeIds is required.");
                    if (model.CounterTypeIds.Any(x => x != 48 && x != 49))
                        throw new ArgumentException("CounterTypeIds must be only 48 or 49.");
                    if (model.CounterQty < 0)
                        throw new ArgumentException("CounterQty must be >= 0.");

                    var now = DateTime.Now;

                    // ✅ วน ProjectIds × CounterTypeIds แล้ว INSERT
                    foreach (var projectId in model.ProjectIds)
                    {
                        foreach (var queueTypeId in model.CounterTypeIds)
                        {
                            if (_context.TR_ProjectCounter_Mappings.Any(e => e.ProjectID == projectId && e.QueueTypeID == queueTypeId && e.FlagActive == true))
                                continue;

                            var row = new TR_ProjectCounter_Mapping
                            {
                                ProjectID = projectId,
                                QueueTypeID = queueTypeId,  // 48 หรือ 49
                                StartCounter = 1,
                                EndCounter = model.CounterQty,
                                FlagActive = true,
                                CreateDate = now,
                                CreateBy = model.UserID,
                                UpdateDate = now,
                                UpdateBy = model.UserID
                            };

                            _context.TR_ProjectCounter_Mappings.Add(row);
                        }
                    }

                    if (model.CounterTypeIds.Contains(48))
                    {
                        if (model == null)
                        {
                            throw new ArgumentNullException(nameof(model));
                        }
                        if (model.ProjectIds == null || model.ProjectIds.Count == 0)
                        {
                            throw new ArgumentException("ProjectIds is required.");
                        }                       
                        if (model.Banks == null || model.Banks.Count == 0)
                        {
                            throw new ArgumentException("Banks is required.");
                        }

                        // ใช้เฉพาะธนาคารที่ติ๊ก Checked
                        var enabledBanks = model.Banks.Where(b => b.Checked).ToList();

                        // (ออปชัน) รวมยอดต้องไม่เกิน CounterQty
                        var sumStaff = enabledBanks.Sum(b => b.Qty);
                        if (sumStaff > model.CounterQty)
                        {
                            throw new ArgumentException($"Sum of bank staff ({sumStaff}) must be <= CounterQty ({model.CounterQty}).");
                        }

                        foreach (var projectId in model.ProjectIds)
                        {
                            foreach (var bank in enabledBanks)
                            {
                                // กันซ้ำ: ถ้ามีคู่ (ProjectID, BankID, FlagActive=1) แล้ว ให้ข้าม
                                if (_context.TR_Register_ProjectBankStaffs.Any(e => e.ProjectID == projectId && e.BankID == bank.BankId && e.FlagActive == true))
                                {
                                    continue;
                                }

                                var row = new TR_Register_ProjectBankStaff
                                {
                                    ProjectID = projectId,
                                    BankID = bank.BankId,
                                    Staff = bank.Qty,
                                    FlagActive = bank.Qty > 0,
                                    CreateDate = now,
                                    CreateBy = model.UserID,
                                    UpdateDate = now,
                                    updateBy = model.UserID
                                };

                                _context.TR_Register_ProjectBankStaffs.Add(row);

                            }
                        }
                    }

                    _context.SaveChanges();
                    transaction.Commit();

                    response.ID = 1;
                    response.Message = $"Inserted Project Counter is successfully.";

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var message = ex.InnerException != null
                        ? $"INNER ERROR: {ex.InnerException.Message}"
                        : $"ERROR: {ex.Message}";

                    response.Message = $"An error occurred: {message}";
                }
            }

            return response;
        }

        public async Task<GetdataEditProjectCounter.ProjectCounterDetailVm?> GetProjectCounterDetailAsync(int id)
        {
            // 1) main row
            var main = await (
                from t1 in _context.TR_ProjectCounter_Mappings.AsNoTracking()
                join t2 in _context.tm_Projects.AsNoTracking()
                    on t1.ProjectID equals t2.ProjectID into pj
                from t2 in pj.DefaultIfEmpty()
                join t3 in _context.tm_Exts.AsNoTracking()
                    on t1.QueueTypeID equals t3.ID into ex
                from t3 in ex.DefaultIfEmpty()
                    // if UpdateBy (string) vs tm_User.ID (int) mismatch, use: on t1.UpdateBy equals t4.ID.ToString()
                join t4 in _context.tm_Users.AsNoTracking()
                    on t1.UpdateBy equals t4.ID into us
                from t4 in us.DefaultIfEmpty()
                where t1.ID == id
                select new
                {
                    Vm = new GetdataEditProjectCounter.ProjectCounterDetailVm
                    {
                        ID = t1.ID.ToString(),
                        ProjectName = t2.ProjectName,
                        QueueType = t3.Name,
                        EndCounter = t1.EndCounter.ToString(),
                        UpdateDate = (t1.UpdateDate.HasValue ? t1.UpdateDate.Value.ToString("dd/MM/yyyy HH:mm") : null),
                        UpdateName = ((t4.FirstName ?? "") + (string.IsNullOrEmpty(t4.LastName) ? "" : " " + t4.LastName))
                    },
                    t1.ProjectID,
                    t1.QueueTypeID
                }
            ).FirstOrDefaultAsync();

            if (main == null) return null;

            // 2) banks only when QueueTypeID = 48
            if (main.QueueTypeID == 48)
            {
                var banks = await (
                    from t1 in _context.TR_ProjectCounter_Mappings.AsNoTracking()
                    join t2 in _context.TR_Register_ProjectBankStaffs.AsNoTracking()
                        on t1.ProjectID equals t2.ProjectID into bs
                    from t2 in bs.DefaultIfEmpty()
                    join t3 in _context.tm_Banks.AsNoTracking()
                        on t2.BankID equals t3.ID into bk
                    from t3 in bk.DefaultIfEmpty()
                    where t1.ID == id
                    select new GetdataEditProjectCounter.BankStaffVm
                    {
                        BankID = (t2 != null ? t2.BankID.ToString() : null),
                        BankCode = t3 != null ? t3.BankCode : null,
                        BankName = t3 != null ? t3.BankName : null,
                        Staff = (t2 != null ? t2.Staff.ToString() : "0"),
                        // per your SQL: use mapping’s FlagActive/UpdateDate
                        FlagActive = t1.FlagActive,
                        UpdateDate = (t1.UpdateDate.HasValue ? t1.UpdateDate.Value.ToString("dd/MM/yyyy HH:mm") : null)
                    }
                ).ToListAsync();

                main.Vm.Banks = banks.Where(b => !string.IsNullOrEmpty(b.BankID)).ToList();
            }

            return main.Vm;
        }

    }
}
