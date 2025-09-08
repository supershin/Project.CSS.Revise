using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.UserBank;
using static QRCoder.PayloadGenerator;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IUserBankRepo
    {
        public List<CountUserByBankModel.ListData> GetListCountUserByBank();
        public List<GetlistUserBank.ListData> GetListUserBank(GetlistUserBank.FilterData filter);
        Task<UserBankEditModel?> GetUserBankByIdAsync(int id);
        public List<GetlistUserBankInTeam> GetlistUserBankInTeam(GetlistUserBankInTeam model);
        Task<int> InsertUserBankAsync(UserBankEditModel model);
    }
    public class UserBankRepo : IUserBankRepo
    {
        private readonly CSSContext _context;

        public UserBankRepo(CSSContext context)
        {
            _context = context;
        }

        public List<CountUserByBankModel.ListData> GetListCountUserByBank()
        {
            var result = new List<CountUserByBankModel.ListData>();

            // ใช้ EF Core connection เดิม
            using var conn = (SqlConnection)_context.Database.GetDbConnection();
            var mustClose = conn.State != System.Data.ConnectionState.Open;
            if (mustClose) conn.Open();

            var sql = @"
                        SELECT COUNT(U.[ID]) CntUserByBank, B.[BankCode], B.[BankName]
                        FROM [PR_User] U WITH (NOLOCK)
                        LEFT JOIN [PR_UserBank_Mapping] UBM WITH (NOLOCK) ON U.[ID] = UBM.[UserID]
                        LEFT JOIN [tm_Bank] B WITH (NOLOCK) ON UBM.[BankID] = B.[ID]
                        WHERE U.[FlagActive] = 1 AND U.[UserTypeID] = 74 AND B.[FlagActive] = 1
                        GROUP BY B.[BankCode], B.[BankName]
                        ORDER BY B.[BankCode];
                    ";

            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new CountUserByBankModel.ListData
                {
                    CntUserByBank = Commond.FormatExtension.Nulltoint(reader["CntUserByBank"]),
                    BankCode = Commond.FormatExtension.NullToString(reader["BankCode"]),
                    BankName = Commond.FormatExtension.NullToString(reader["BankName"])
                });
            }

            if (mustClose) conn.Close();
            return result;
        }

        public List<GetlistUserBank.ListData> GetListUserBank(GetlistUserBank.FilterData filter)
        {
            var result = new List<GetlistUserBank.ListData>();
            var connectionString = _context.Database.GetConnectionString();
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("No connection string configured for CSSContext.");
            }
                
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            var sql = @"
                        SELECT
                             U.[ID]
                           , U.[FirstName] + N' ' + U.[LastName] AS FullName
                           , B.[BankCode]
                           , B.[BankName]
                           , CASE 
                                WHEN U.[IsLeadBank] = 1           THEN 1
                                WHEN U.[ParentBankID] IS NOT NULL THEN 2
                                ELSE 3
                             END AS Role
                        FROM [PR_User] U WITH (NOLOCK)
                        LEFT JOIN [PR_UserBank_Mapping] UBM WITH (NOLOCK) ON U.[ID] = UBM.[UserID]
                        LEFT JOIN [tm_Bank] B WITH (NOLOCK) ON UBM.[BankID] = B.[ID]
                        WHERE U.[FlagActive] = 1
                          AND B.[FlagActive] = 1
                          AND U.[UserTypeID] = 74
                          AND (
                                @L_BankIDs = N''
                             OR (N',' + @L_BankIDs + N',' LIKE N'%,' + CONVERT(NVARCHAR(20), B.[ID]) + N',%')
                          )
                          AND (
                                @L_Name = N''
                             OR U.[FirstName] LIKE N'%' + @L_Name + N'%'
                             OR U.[LastName]  LIKE N'%' + @L_Name + N'%'
                             OR (U.[FirstName] + N' ' + U.[LastName]) LIKE N'%' + @L_Name + N'%'
                          )
                        ORDER BY
                            B.[BankCode],
                            COALESCE(U.[ParentBankID], U.[ID]),
                            CASE WHEN U.[ParentBankID] IS NULL OR U.[IsLeadBank] = 1 THEN 0 ELSE 1 END,
                            U.[FirstName];
                    ";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@L_BankIDs", (object?)filter.L_BankIDs ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@L_Name", (object?)filter.L_Name ?? string.Empty));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new GetlistUserBank.ListData
                        {
                            ID = Commond.FormatExtension.Nulltoint(reader["ID"]),
                            FullName = Commond.FormatExtension.NullToString(reader["FullName"]),
                            BankCode = Commond.FormatExtension.NullToString(reader["BankCode"]),
                            BankName = Commond.FormatExtension.NullToString(reader["BankName"]),
                            Role = Commond.FormatExtension.Nulltoint(reader["Role"])
                        });
                    }
                }
            }

   
            return result;
        }

        public async Task<UserBankEditModel?> GetUserBankByIdAsync(int id)
        {
            // LEFT JOIN: PR_User -> PR_UserBank_Mapping -> tm_Bank
            var row = await (
                from u in _context.PR_Users.AsNoTracking()
                where u.ID == id
                join ubm in _context.PR_UserBank_Mappings.AsNoTracking()
                     on u.ID equals ubm.UserID into j1
                from ubm in j1.DefaultIfEmpty() // LEFT
                join b in _context.tm_Banks.AsNoTracking()
                     on ubm.BankID equals b.ID into j2
                from b in j2.DefaultIfEmpty() // LEFT
                select new UserBankEditModel
                {
                    ID = u.ID,
                    UserTypeID = u.UserTypeID,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Mobile = u.Mobile,
                    Email = u.Email,
                    UserName = u.UserName,
                    Password = u.Password,      // ถ้าไม่อยากส่งออก ให้เป็น null
                    ConsentAccept = u.ConsentAccept,
                    FlagActive = u.FlagActive,
                    CreateDate = u.CreateDate,
                    CreateBy = u.CreateBy,
                    UpdateDate = u.UpdateDate,
                    UpdateBy = u.UpdateBy,
                    IsLeadBank = u.IsLeadBank,
                    ParentBankID = u.ParentBankID,

                    // ธนาคารจาก JOIN
                    BankID = ubm != null ? ubm.BankID : null,
                    BankCode = b != null ? b.BankCode : null,
                    BankName = b != null ? b.BankName : null
                }
            )
            // ถ้ามีหลายแถว (กรณีแมปหลายธนาคาร) จะเอาแถวแรกพอ
            .FirstOrDefaultAsync();

            if (row == null) return null;

            row.ProjectUserBank = await _context.PR_ProjectBankUser_Mappings
                .AsNoTracking()
                .Where(m => m.BankUserID == id)
                .Select(m => new ListProject
                {
                    ProjectID = m.ProjectID,
                    BankUserID = m.BankUserID
                })
                .ToListAsync();

            return row;
        }

        public List<GetlistUserBankInTeam> GetlistUserBankInTeam(GetlistUserBankInTeam model)
        {
            var result = from u in _context.PR_Users
                         join e in _context.tm_Exts on u.AreaID equals e.ID into gj
                         from e in gj.DefaultIfEmpty()               
                         where u.FlagActive == true
                             && u.ParentBankID == model.ParentBankID
                             && u.UserTypeID == Constants.Ext.UserBank
                         select new GetlistUserBankInTeam
                         {
                            ID = u.ID,
                            FullName = (u.FirstName ?? "") + " " + (u.LastName ?? ""),
                            Mobile = u.Mobile,
                            Email = u.Email,
                            AreaID = u.AreaID,
                            AreaName = e != null ? e.Name : null
                         };

            return result.ToList();
        }

        public async Task<int> InsertUserBankAsync(UserBankEditModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FirstName) ||
                string.IsNullOrWhiteSpace(model.LastName) ||
                string.IsNullOrWhiteSpace(model.UserName) ||
                string.IsNullOrWhiteSpace(model.Password))
            {
                throw new ArgumentException("FirstName, LastName, UserName, Password are required.");
            }

            using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1) INSERT PR_User
                var user = new PR_User
                {
                    // ค่าจำเป็น/จากฟอร์ม
                    UserTypeID = model.UserTypeID ?? 74, // fix 74
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    UserName = model.UserName,
                    Password = model.Password,      // TODO: ถ้าต้องการ hash ให้ทำที่นี่

                    // ค่า state
                    FlagActive = model.FlagActive ?? true,
                    ConsentAccept = model.ConsentAccept,

                    // team flags
                    IsLeadBank = model.IsLeadBank,
                    ParentBankID = model.ParentBankID, // ถ้าเป็น lead อาจเป็น null; ถ้าเป็น crew อาจเซ็ตไว้

                    AreaID = model.AreaID,

                    // audit
                    CreateDate = model.CreateDate ?? DateTime.Now,
                    CreateBy = model.CreateBy ?? "system",
                    UpdateDate = model.UpdateDate,
                    UpdateBy = model.UpdateBy
                };

                _context.PR_Users.Add(user);
                await _context.SaveChangesAsync(); // ได้ user.ID

                var newUserId = user.ID;

                // 2) (แนะนำ) INSERT PR_UserBank_Mapping ถ้ามี BankID
                if (model.BankID.HasValue)
                {
                    var map = new PR_UserBank_Mapping
                    {
                        UserID = newUserId,
                        BankID = model.BankID.Value
                    };
                    _context.PR_UserBank_Mappings.Add(map);
                    await _context.SaveChangesAsync();
                }

                // 3) INSERT PR_ProjectBankUser_Mapping (ProjectID เป็น string เช่น "PJ001")
                if (model.ProjectUserBank != null && model.ProjectUserBank.Count > 0)
                {
                    foreach (var p in model.ProjectUserBank)
                    {
                        var pid = (p.ProjectID ?? string.Empty).Trim();
                        if (string.IsNullOrEmpty(pid)) continue; // ข้ามว่าง

                        // (ออปชัน) กันซ้ำ: project เดิม-คนเดิม
                        var exists = await _context.PR_ProjectBankUser_Mappings
                            .AsNoTracking()
                            .AnyAsync(x => x.ProjectID == pid && x.BankUserID == newUserId);
                        if (exists) continue;

                        var pm = new PR_ProjectBankUser_Mapping
                        {
                            ProjectID = pid,
                            BankUserID = newUserId
                        };
                        _context.PR_ProjectBankUser_Mappings.Add(pm);
                    }

                    await _context.SaveChangesAsync();
                }

                await tx.CommitAsync();
                return newUserId;
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

    }
}
