using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.CSResponse;
using Project.CSS.Revise.Web.Models.Pages.Shop_Event;
using System.Linq;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface ICSResponseRepo
    {
        public List<GetlistUnitCSResponseModel.ListData> GetlistUnitCSResponse(GetlistUnitCSResponseModel.FilterData filter);
        public UpdateInsertCsmapping UpdateorInsertCsmapping(UpdateInsertCsmapping model);
        Task<List<GetlistCountByCS>> GetListCountByCSAsync();
    }
    public class CSResponseRepo : ICSResponseRepo
    {
        private readonly CSSContext _context;

        public CSResponseRepo(CSSContext context)
        {
            _context = context;
        }

        public List<GetlistUnitCSResponseModel.ListData> GetlistUnitCSResponse(GetlistUnitCSResponseModel.FilterData filter)
        {
            List<GetlistUnitCSResponseModel.ListData> result = new List<GetlistUnitCSResponseModel.ListData>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "";
                sql = @"
                                -- ===== TEST CASE =====
	                            --DECLARE @L_UserID NVARCHAR(100) = '';
                                --DECLARE @L_ProjectID NVARCHAR(100) = '102C028';
                                --DECLARE @L_UnitStatus NVARCHAR(100) = '';
	                            --DECLARE @L_Build NVARCHAR(100) = 'A,B';
	                            --DECLARE @L_Floor NVARCHAR(100) = '';
	                            --DECLARE @L_Room NVARCHAR(100) = '10';
                                --DECLARE @L_TypeUserShow   NVARCHAR(10)  = '-1';  -- '1' = เฉพาะของฉัน, '-1' = ทั้งหมด, '0' = ของคนอื่น
                                -- ===== TEST CASE =====


                            SELECT   P.[ProjectID]
                                    ,P.[ProjectName]
                                    ,P.[ProjectName_Eng]
                                    ,P.[ProjectType]
	                                ,U.[UnitCode]
                                    ,U.[AddrNo]
                                    ,U.[Build]
                                    ,U.[Floor]
                                    ,U.[Room]
                                    ,U.[UnitType]
                                    ,U.[Area]
                                    ,U.[UnitStatus]
	                                ,UUM.[UserID] AS CSUserID
                                    ,US.[FirstName] + ' ' + US.[LastName] AS CSFullNameThai
                                    ,US.[FirstName_Eng] + ' ' + US.[LastName_Eng] AS CSFullNameEng
	                                ,CASE WHEN CONVERT(NVARCHAR(100), UUM.[UserID]) = @L_UserID THEN 1 ELSE 0 END AS IsCheck
									,UPUS.[FirstName] + ' ' + UPUS.[LastName] AS UpdateBy
									,UUM.[UpdateDate] 
                                FROM [tm_Project] P WITH (NOLOCK) 
                                LEFT JOIN [tm_Unit] U WITH (NOLOCK) ON U.ProjectID = P.ProjectID
                                LEFT JOIN [TR_UnitUser_Mapping] UUM WITH (NOLOCK) ON P.ProjectID = UUM.ProjectID AND UUM.UnitCode = U.[UnitCode]
                                LEFT JOIN [tm_User] US WITH (NOLOCK) ON UUM.UserID = US.[ID]
								LEFT JOIN [tm_User] UPUS WITH (NOLOCK) ON UUM.UpdateBy = UPUS.[ID]
                            WHERE U.[FlagActive] = 1
                                AND P.[FlagActive] = 1
                                AND P.[ProjectID] =  @L_ProjectID
                                AND (
		                            @L_Build = N''
		                            OR (N',' + @L_Build + N',' LIKE N'%,' + CONVERT(NVARCHAR(100), U.[Build]) + N',%')
	                                )
                                AND (
		                            @L_UnitStatus = N''
		                            OR (N',' + @L_UnitStatus + N',' LIKE N'%,' + CONVERT(NVARCHAR(100), U.[UnitStatus]) + N',%')
	                                )
                                AND (
                                        -- 3) ถ้าไม่ส่ง @Pairs มา → ผ่าน
                                        @L_Floor = N''
                                        -- 4) ถ้าส่ง @Pairs มา → จับคู่ Build-Floor แบบเป๊ะ ๆ
                                    OR (',' + @L_Floor + ',' LIKE '%,' + CONVERT(varchar(50), U.[Build]) + '-' + CONVERT(varchar(50), U.[Floor]) + ',%')
                                    )
                                AND (
		                            @L_Room = N''
		                            OR (N',' + @L_Room + N',' LIKE N'%,' + CONVERT(NVARCHAR(100), U.[Room]) + N',%')
	                                )
                                AND (
                                        @L_TypeUserShow = '-1'
                                    OR (@L_TypeUserShow =  '1' AND CONVERT(NVARCHAR(100), UUM.[UserID]) =  @L_UserID)  -- เฉพาะของฉัน
                                    OR (@L_TypeUserShow =  '0' AND (UUM.[UserID] IS NULL OR CONVERT(NVARCHAR(100), UUM.[UserID]) <> @L_UserID))
                                    )
                                ORDER BY P.[ProjectID]
                                        ,U.[UnitCode]

                           "
                                    ;

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_UserID", filter.L_UserID ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", filter.L_ProjectID ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_UnitStatus", filter.L_UnitStatus ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Build", filter.L_Build ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Floor", filter.L_Floor ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Room", filter.L_Room ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_TypeUserShow", filter.L_TypeUserShow ?? "-1"));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new GetlistUnitCSResponseModel.ListData
                            {
                                ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]),
                                ProjectName = Commond.FormatExtension.NullToString(reader["ProjectName"]),
                                ProjectName_Eng = Commond.FormatExtension.NullToString(reader["ProjectName_Eng"]),
                                ProjectType = Commond.FormatExtension.NullToString(reader["ProjectType"]),

                                UnitCode = Commond.FormatExtension.NullToString(reader["UnitCode"]),
                                AddrNo = Commond.FormatExtension.NullToString(reader["AddrNo"]),
                                Build = Commond.FormatExtension.NullToString(reader["Build"]),
                                Floor = Commond.FormatExtension.NullToString(reader["Floor"]),
                                Room = Commond.FormatExtension.NullToString(reader["Room"]),
                                UnitType = Commond.FormatExtension.NullToString(reader["UnitType"]),
                                Area = Commond.FormatExtension.NullToString(reader["Area"]),
                                UnitStatus = Commond.FormatExtension.NullToString(reader["UnitStatus"]),

                                CSUserID = Commond.FormatExtension.NullToString(reader["CSUserID"]),
                                CSFullNameThai = Commond.FormatExtension.NullToString(reader["CSFullNameThai"]),
                                CSFullNameEng = Commond.FormatExtension.NullToString(reader["CSFullNameEng"]),
                                UpdateBy = Commond.FormatExtension.NullToString(reader["UpdateBy"]),
                                UpdateDate = Commond.FormatExtension.ToStringFrom_DD_MM_YYYY_To_DD_MM_YYYY(reader["UpdateDate"]),
                                IsCheck = Commond.FormatExtension.Nulltoint(reader["IsCheck"])
                            });
                        }
                    }
                }
            }


            return result;
        }

        public UpdateInsertCsmapping UpdateorInsertCsmapping(UpdateInsertCsmapping model)
        {
            try
            {
                if (model == null)
                {
                    return new UpdateInsertCsmapping { Issuccess = false, Message = "Model is null." };
                }

                if (string.IsNullOrWhiteSpace(model.ProjectID))
                {
                    return new UpdateInsertCsmapping { Issuccess = false, Message = "ProjectID is required." };
                }

                if (model.CSUserID <= 0)
                {
                    return new UpdateInsertCsmapping { Issuccess = false, Message = "CSUserID is required." };
                }

                if (model.ListUnitCode == null || model.ListUnitCode.Count == 0)
                {
                    return new UpdateInsertCsmapping { Issuccess = false, Message = "Unit list is empty." };
                }

                var now = DateTime.Now;
                var projectId = model.ProjectID.Trim();
                var unitCodes = model.ListUnitCode
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                int updated = 0;
                int inserted = 0;

                using (var tx = _context.Database.BeginTransaction())
                {
                    foreach (var code in unitCodes)
                    {
                        try
                        {
                            var row = _context.TR_UnitUser_Mappings.FirstOrDefault(x => x.ProjectID == projectId && x.UnitCode == code);

                            if (row != null)
                            {
                                // UPDATE
                                row.UserID = model.CSUserID;
                                row.UpdateDate = now;
                                row.UpdateBy = model.UpdateBy;
                                _context.TR_UnitUser_Mappings.Update(row);
                                updated++;
                            }
                            else
                            {
                                // INSERT
                                var newRow = new TR_UnitUser_Mapping
                                {
                                    ID = Guid.NewGuid(),
                                    ProjectID = projectId,
                                    UnitCode = code,
                                    UserID = model.CSUserID,
                                    CreateDate = now,
                                    CreateBy = model.UpdateBy,
                                    UpdateDate = now,
                                    UpdateBy = model.UpdateBy
                                };

                                _context.TR_UnitUser_Mappings.Add(newRow);
                                inserted++;
                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }

                    _context.SaveChanges();
                    tx.Commit();
                }

                return new UpdateInsertCsmapping
                {
                    Issuccess = true,
                    Message = $"Updated {updated}, Inserted {inserted}"
                };
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null
                    ? $"INNER ERROR: {ex.InnerException.Message}"
                    : $"ERROR: {ex.Message}";

                return new UpdateInsertCsmapping
                {
                    Issuccess = false,
                    Message = msg
                };
            }
        }

        public async Task<List<GetlistCountByCS>> GetListCountByCSAsync()
        {
            var query =
                from u in _context.tm_Users
                join tTH in _context.tm_TitleNames on u.TitleID equals tTH.ID into thg
                from tTH in thg.DefaultIfEmpty()
                join tEN in _context.tm_TitleNames on u.TitleID_Eng equals tEN.ID into engg
                from tEN in engg.DefaultIfEmpty()
                where u.FlagActive == true
                      && u.QCTypeID == 10
                      && u.DepartmentID == 31
                select new GetlistCountByCS
                {
                    ID = u.ID,
                    FullnameTH = ((tTH.Name ?? "") + " " + (u.FirstName ?? "") + " " + (u.LastName ?? "")).Trim(),
                    FullnameEN = ((tEN.Name ?? "") + " " + (u.FirstName_Eng ?? "") + " " + (u.LastName_Eng ?? "")).Trim(),
                    Email = u.Email,
                    Mobile = u.Mobile,

                    Project = (
                        from m in _context.TR_UnitUser_Mappings
                        join p in _context.tm_Projects on m.ProjectID equals p.ProjectID into pg
                        from p in pg.DefaultIfEmpty()
                        where m.UserID == u.ID
                        group m by new { m.ProjectID, p.ProjectName } into g
                        select new ListProjectAndCountUnit
                        {
                            ProjectID = g.Key.ProjectID.ToString(),
                            ProjectName = g.Key.ProjectName ?? "",
                            CountUnit = g.Count(x => x.UnitCode != null),

                            Unit = (
                                from unit in _context.tm_Units
                                join us in _context.tm_UnitStatuses on unit.UnitStatus equals us.ID into usg
                                from us in usg.DefaultIfEmpty()
                                where unit.ProjectID == g.Key.ProjectID
                                group unit by new { unit.UnitStatus, UnitStatusName = us.Name } into ug
                                orderby ug.Key.UnitStatus
                                select new ListUnitCoutstatus
                                {
                                    UnitStatus = ug.Key.UnitStatus ?? 0,
                                    UnitStatusName = ug.Key.UnitStatusName ?? "",
                                    TotalUnit = ug.Count()
                                }
                            ).ToList()
                        }
                    ).ToList()
                };

            return await query.ToListAsync();
        }

    }
}
