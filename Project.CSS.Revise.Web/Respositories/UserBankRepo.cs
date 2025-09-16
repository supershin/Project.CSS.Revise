using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Pages.UserBank;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
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
        public bool MoveUserbankToTeam(int UserBankID, int ParrentID, string UserID);
        public bool LeavUserbankFromTeam(int UserBankID, string UserID);
        Task<int> UpdateUserBankAsync(UserBankEditModel model);
        Task<bool> SoftDeleteUserBankAsync(int id, string updatedBy);
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

                // u -> (own) bank mapping
                join ubm in _context.PR_UserBank_Mappings.AsNoTracking()
                    on u.ID equals ubm.UserID into j1
                from ubm in j1.DefaultIfEmpty()   // LEFT

                    // (own) bank info
                join b in _context.tm_Banks.AsNoTracking()
                    on ubm.BankID equals b.ID into j2
                from b in j2.DefaultIfEmpty()     // LEFT

                    // parent user (u.ParentBankID -> PP.ID)
                join pp0 in _context.PR_Users.AsNoTracking()
                    on u.ParentBankID equals (int?)pp0.ID into j3
                from pp in j3.DefaultIfEmpty()    // LEFT

                    // parent user's bank mapping
                join ubmp0 in _context.PR_UserBank_Mappings.AsNoTracking()
                    on pp.ID equals ubmp0.UserID into j4
                from ubmp in j4.DefaultIfEmpty()  // LEFT

                    // parent bank info
                join bp0 in _context.tm_Banks.AsNoTracking()
                    on ubmp.BankID equals bp0.ID into j5
                from bp in j5.DefaultIfEmpty()    // LEFT

                select new UserBankEditModel
                {
                    ID = u.ID,
                    UserTypeID = u.UserTypeID,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Mobile = u.Mobile,
                    Email = u.Email,
                    UserName = u.UserName,
                    Password = SecurityManager.DecodeFrom64(u.Password),   // หรือ null ถ้าไม่อยากส่งออก
                    ConsentAccept = u.ConsentAccept,
                    FlagActive = u.FlagActive,
                    CreateDate = u.CreateDate,
                    CreateBy = u.CreateBy,
                    UpdateDate = u.UpdateDate,
                    UpdateBy = u.UpdateBy,
                    IsLeadBank = u.IsLeadBank,
                    ParentBankID = u.ParentBankID,
                    AreaID = u.AreaID,

                    // ธนาคารของ user นี้
                    BankID = ubm != null ? ubm.BankID : null,
                    BankCode = b != null ? b.BankCode : null,
                    BankName = b != null ? b.BankName : null,

                    // ทีมของ Parent:  bp.BankName + ' ' + PP.FirstName + ' ' + PP.LastName
                    ParentTeam =
                        ((bp != null ? bp.BankName : null) ?? "") + " " +
                        ((pp != null ? pp.FirstName : null) ?? "") + " " +
                        ((pp != null ? pp.LastName : null) ?? "")
                }
            ).FirstOrDefaultAsync();


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

        //public List<GetlistUserBankInTeam> GetlistUserBankInTeam(GetlistUserBankInTeam model)
        //{
        //    var result = from u in _context.PR_Users
        //                 //join e in _context.tm_Exts on u.AreaID equals e.ID into gj
        //                 //from e in gj.DefaultIfEmpty()
        //                 where u.FlagActive == true
        //                     && u.ParentBankID == model.ParentBankID
        //                     && u.UserTypeID == Constants.Ext.UserBank
        //                 select new GetlistUserBankInTeam
        //                 {
        //                     ID = u.ID,
        //                     FullName = (u.FirstName ?? "") + " " + (u.LastName ?? ""),
        //                     Mobile = u.Mobile,
        //                     Email = u.Email,
        //                     AreaID = "",
        //                     AreaName = ""
        //                 };

        //    return result.ToList();
        //}

        public List<GetlistUserBankInTeam> GetlistUserBankInTeam(GetlistUserBankInTeam model)
        {
            // ดึงแผนที่ ID → Name ของ Area (ExtTypeID = 67)
            var areaMap = _context.tm_Exts
                .AsNoTracking()
                .Where(e => e.FlagActive == true && e.ExtTypeID == 67)
                .Select(e => new { e.ID, e.Name })
                .ToList()
                .ToDictionary(x => x.ID, x => x.Name ?? "");

            // ดึงผู้ใช้ที่อยู่ใต้หัวหน้าตามที่ส่งมา
            var users = _context.PR_Users
                .AsNoTracking()
                .Where(u => u.FlagActive == true
                         && u.ParentBankID == model.ParentBankID
                         && u.UserTypeID == Constants.Ext.UserBank)
                .Select(u => new
                {
                    u.ID,
                    u.FirstName,
                    u.LastName,
                    u.Mobile,
                    u.Email,
                    u.AreaID // CSV เช่น "461,463,464"
                })
                .ToList();

            // helper แปลง CSV → ชื่อ ด้วยลำดับตาม CSV
            static string ToAreaName(string? csv, IDictionary<int, string> map)
            {
                if (string.IsNullOrWhiteSpace(csv)) return "";
                var names = csv
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(s => int.TryParse(s, out var id) ? id : (int?)null)
                    .Where(id => id.HasValue && map.ContainsKey(id.Value))
                    .Select(id => map[id!.Value])
                    .ToList();

                return names.Count == 0 ? "" : string.Join(" , ", names); // ช่องว่างทั้งสองข้างตามที่ขอ
            }

            // map กลับเป็นรุ่นที่ต้องการ
            var result = users.Select(u => new GetlistUserBankInTeam
            {
                ID = u.ID,
                FullName = $"{u.FirstName ?? ""} {u.LastName ?? ""}".Trim(),
                Mobile = u.Mobile,
                Email = u.Email,
                AreaID = u.AreaID ?? "",
                AreaName = ToAreaName(u.AreaID, areaMap)
            })
            .ToList();

            return result;
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

        public bool MoveUserbankToTeam(int UserBankID, int ParrentID, string UserID)
        {
            var response = false;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    var existing = _context.PR_Users.FirstOrDefault(e => e.ID == UserBankID);

                    if (existing != null)
                    {
                        existing.ParentBankID = ParrentID;
                        existing.UpdateDate = DateTime.Now;
                        existing.UpdateBy = UserID;

                        _context.PR_Users.Update(existing);
                    }

                    _context.SaveChanges();


                    transaction.Commit();

                    response = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var message = ex.InnerException != null
                        ? $"INNER ERROR: {ex.InnerException.Message}"
                        : $"ERROR: {ex.Message}";
                    response = false;
                }

            }

            return response;
        }

        public bool LeavUserbankFromTeam(int UserBankID, string UserID)
        {
            var response = false;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    var existing = _context.PR_Users.FirstOrDefault(e => e.ID == UserBankID);

                    if (existing != null)
                    {
                        existing.ParentBankID = null;
                        existing.UpdateDate = DateTime.Now;
                        existing.UpdateBy = UserID;

                        _context.PR_Users.Update(existing);
                    }

                    _context.SaveChanges();


                    transaction.Commit();

                    response = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var message = ex.InnerException != null
                        ? $"INNER ERROR: {ex.InnerException.Message}"
                        : $"ERROR: {ex.Message}";
                    response = false;
                }

            }

            return response;
        }

        public async Task<int> UpdateUserBankAsync(UserBankEditModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (model.ID <= 0) throw new ArgumentException("Invalid ID.");

            // basic required fields (password optional on edit)
            if (string.IsNullOrWhiteSpace(model.FirstName)
                || string.IsNullOrWhiteSpace(model.LastName)
                || string.IsNullOrWhiteSpace(model.UserName))
                throw new ArgumentException("FirstName, LastName, and UserName are required.");

            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var existing = await _context.PR_Users.FirstOrDefaultAsync(e => e.ID == model.ID);
                if (existing == null) throw new KeyNotFoundException("User not found.");

                // --- PR_User updates ---
                existing.FirstName = model.FirstName?.Trim();
                existing.LastName = model.LastName?.Trim();
                existing.Mobile = model.Mobile;
                existing.Email = model.Email;
                existing.UserName = model.UserName?.Trim();

                // update password only if provided (avoid overwriting with empty)
                if (!string.IsNullOrWhiteSpace(model.Password))
                    existing.Password = model.Password; // TODO: hash if needed

                existing.FlagActive = model.FlagActive ?? (existing.FlagActive ?? true);
                existing.ConsentAccept = model.ConsentAccept ?? existing.ConsentAccept;

                existing.IsLeadBank = model.IsLeadBank;
                existing.ParentBankID = model.ParentBankID;
                existing.AreaID = model.AreaID;

                // keep original CreateDate/CreateBy; only update audit
                existing.UpdateDate = DateTime.Now;
                existing.UpdateBy = string.IsNullOrWhiteSpace(model.UpdateBy) ? "system" : model.UpdateBy;

                await _context.SaveChangesAsync();

                // --- PR_UserBank_Mapping (optional) ---
                if (model.BankID.HasValue)
                {
                    var map = await _context.PR_UserBank_Mappings.FirstOrDefaultAsync(m => m.UserID == existing.ID);

                    if (map == null)
                    {
                        _context.PR_UserBank_Mappings.Add(new PR_UserBank_Mapping
                        {
                            UserID = existing.ID,
                            BankID = model.BankID.Value
                        });
                    }
                    else if (map.BankID != model.BankID.Value)
                    {
                        map.BankID = model.BankID.Value;
                        _context.PR_UserBank_Mappings.Update(map);
                    }

                    await _context.SaveChangesAsync();
                }

                // --- PR_ProjectBankUser_Mapping (hard replace: delete all then insert new) ---
                if (model.ProjectUserBank != null)
                {
                    // 1) ลบ mapping เดิมทั้งหมดของ user นี้
                    var oldMaps = await _context.PR_ProjectBankUser_Mappings
                                                .Where(m => m.BankUserID == existing.ID)
                                                .ToListAsync();

                    if (oldMaps.Count > 0)
                    {
                        _context.PR_ProjectBankUser_Mappings.RemoveRange(oldMaps);
                        await _context.SaveChangesAsync();
                    }

                    // 2) เตรียมชุดใหม่จาก payload (กันซ้ำ/ว่าง)
                    var toAdd = model.ProjectUserBank
                        .Select(p => (p.ProjectID ?? string.Empty).Trim())
                        .Where(pid => !string.IsNullOrEmpty(pid))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .Select(pid => new PR_ProjectBankUser_Mapping
                        {
                            ProjectID = pid,          // string เช่น "PJ001"
                            BankUserID = existing.ID
                        })
                        .ToList();

                    if (toAdd.Count > 0)
                    {
                        await _context.PR_ProjectBankUser_Mappings.AddRangeAsync(toAdd);
                        await _context.SaveChangesAsync();
                    }
                }

                // ถ้า "เคยเป็นหัวหน้า" แต่ "ตอนนี้ไม่ใช่หัวหน้าแล้ว"
                // → ถอดลูกทีมทุกคนออกจากทีม (ParentBankID = null)
                if (!model.IsLeadBank)
                {
                    var crews = await _context.PR_Users.Where(u => u.ParentBankID == existing.ID).ToListAsync();

                    foreach (var c in crews)
                    {
                        c.ParentBankID = null;
                        c.UpdateDate = DateTime.Now;
                        c.UpdateBy = existing.UpdateBy; // หรือ model.UpdateBy
                    }

                    await _context.SaveChangesAsync();
                }

                await tx.CommitAsync();
                return existing.ID;
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> SoftDeleteUserBankAsync(int id, string updatedBy)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            var u = await _context.PR_Users.FirstOrDefaultAsync(x => x.ID == id);
            if (u == null) return false;

            // 1) Soft delete this user
            u.FlagActive = false;
            u.UpdateBy = updatedBy;
            u.UpdateDate = DateTime.Now;

            // 2) If this user is a Lead → detach all crews
            if (u.IsLeadBank)
            {
                var crews = await _context.PR_Users
                                          .Where(c => c.ParentBankID == u.ID)
                                          .ToListAsync();
                foreach (var c in crews)
                {
                    c.ParentBankID = null;
                    c.UpdateBy = updatedBy;
                    c.UpdateDate = DateTime.Now;
                }
            }
            else
            {
                // Crew (or no-crew): ensure their own ParentBankID is cleared
                if (u.ParentBankID != null)
                {
                    u.ParentBankID = null;
                }
            }

            await _context.SaveChangesAsync();
            await tx.CommitAsync();
            return true;
        }

    }
}
