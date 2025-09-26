using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.UserAndPermission;
using static Project.CSS.Revise.Web.Models.Pages.UserAndPermission.UserAndPermissionModel;
using Project.CSS.Revise.Web.Commond;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IUserAndPermissionRepo
    {
        List<UserAndPermissionModel.GetlistUser> GetlistUser(UserAndPermissionModel.FiltersGetlistUser filters);
        public int InsertUser(UserAndPermissionModel.CreateUserRequest model, int currentUserId);
        public bool UpdateUser(UserAndPermissionModel.UpdateUserRequest model, int currentUserId);
        public DuplicateCheckResult CheckDuplicate(string? email, string? userId, string? firstNameTh, string? lastNameTh, string? firstNameEn, string? lastNameEn, int? excludeId = null);
        public UserAndPermissionModel.UserDetail? GetDetailsUser(UserAndPermissionModel.FiltersGetlistUser filters);
        public List<UserAndPermissionModel.GetlistProjects> GetlistProjects(UserAndPermissionModel.FiltersGetlistUser filters);
        public bool IUDProjectUserMapping(UserAndPermissionModel.IUDProjectUserMapping model, int currentUserId);
        public List<UserAndPermissionModel.PermissionMatrixRow> GetPermissionMatrix(int qcTypeId = 10);
        bool SaveRolePermissions(UserAndPermissionModel.SaveRolePermissionRequest req, int currentUserId);
    }
    public class UserAndPermissionRepo : IUserAndPermissionRepo
    {
        private readonly CSSContext _context;

        public UserAndPermissionRepo(CSSContext context)
        {
            _context = context;
        }

        public List<UserAndPermissionModel.GetlistUser> GetlistUser(UserAndPermissionModel.FiltersGetlistUser filters)
        {
            var result = new List<UserAndPermissionModel.GetlistUser>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                                SELECT 
                                     T1.[ID]
                                    ,LTRIM(RTRIM(ISNULL(T1.[FirstName], N'') + N' ' + ISNULL(T1.[LastName], N'')))            AS FullnameTH
                                    ,LTRIM(RTRIM(ISNULL(T1.[FirstName_Eng], N'') + N' ' + ISNULL(T1.[LastName_Eng], N'')))    AS FullnameEN
                                    ,T1.[Email]
                                    ,T1.[Mobile]
                                    ,T1.[DepartmentID]
                                    ,ISNULL(T2.[Name], N'ยังไม่ได้ระบุแผนก') AS DepartmentName
                                    ,T1.[RoleID]
                                    ,ISNULL(T3.[Name], N'ยังไม่ได้บทบาท')     AS RoleName
                                FROM [tm_User] T1 WITH (NOLOCK)
                                LEFT JOIN [tm_Ext]  T2 WITH (NOLOCK) ON T1.[DepartmentID] = T2.[ID]
                                LEFT JOIN [tm_Role] T3 WITH (NOLOCK) ON T1.[RoleID]      = T3.[ID]
                                WHERE T1.[FlagActive] = 1
                                AND T1.QCTypeID = 10

                                AND (
                                        @L_DepartmentID = N''
                                    OR  (N',' + @L_DepartmentID + N',' LIKE N'%,' + CONVERT(NVARCHAR(50), T1.[DepartmentID]) + N',%')
                                    )

                                AND (
                                        @L_RoleID = N''
                                    OR  (N',' + @L_RoleID + N',' LIKE N'%,' + CONVERT(NVARCHAR(50), T1.[RoleID]) + N',%')
                                    )

                                AND (
                                        @L_Name = N''
                                    OR  ISNULL(T1.[FirstName],      N'') LIKE N'%' + @L_Name + N'%'
                                    OR  ISNULL(T1.[LastName],       N'') LIKE N'%' + @L_Name + N'%'
                                    OR  LTRIM(RTRIM(ISNULL(T1.[FirstName], N'') + N' ' + ISNULL(T1.[LastName], N''))) LIKE N'%' + @L_Name + N'%'
                                    OR  ISNULL(T1.[FirstName_Eng],  N'') LIKE N'%' + @L_Name + N'%'
                                    OR  ISNULL(T1.[LastName_Eng],   N'') LIKE N'%' + @L_Name + N'%'
                                    OR  LTRIM(RTRIM(ISNULL(T1.[FirstName_Eng], N'') + N' ' + ISNULL(T1.[LastName_Eng], N''))) LIKE N'%' + @L_Name + N'%'
                                    )

                                ORDER BY 
                                     T1.[UpdateDate] DESC
                                    ,T1.[FirstName];
                                ";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_Name", (object?)filters?.L_Name ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@L_DepartmentID", (object?)filters?.L_DepartmentID ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@L_RoleID", (object?)filters?.L_RoleID ?? string.Empty));

                    using (var reader = cmd.ExecuteReader())
                    {
                        int idx = 0;
                        while (reader.Read())
                        {
                            idx++;

                            result.Add(new UserAndPermissionModel.GetlistUser
                            {
                                ID = Commond.FormatExtension.Nulltoint(reader["ID"])
                                ,
                                index = idx
                                ,
                                FullnameTH = Commond.FormatExtension.NullToString(reader["FullnameTH"])
                                ,
                                FullnameEN = Commond.FormatExtension.NullToString(reader["FullnameEN"])
                                ,
                                Email = Commond.FormatExtension.NullToString(reader["Email"])
                                ,
                                Mobile = Commond.FormatExtension.NullToString(reader["Mobile"])
                                ,
                                DepartmentID = Commond.FormatExtension.Nulltoint(reader["DepartmentID"])
                                ,
                                DepartmentName = Commond.FormatExtension.NullToString(reader["DepartmentName"])
                                ,
                                RoleID = Commond.FormatExtension.Nulltoint(reader["RoleID"])
                                ,
                                RoleName = Commond.FormatExtension.NullToString(reader["RoleName"])
                            });
                        }
                    }
                }
            }

            return result;
        }

        public int InsertUser(UserAndPermissionModel.CreateUserRequest model, int currentUserId)
        {
            using var tx = _context.Database.BeginTransaction();

            var now = DateTime.Now;

            var entity = new tm_User
            {
                TitleID = model.TitleID,
                TitleID_Eng = model.TitleID == 1 ? 4 : model.TitleID == 2 ? 5 : model.TitleID == 3 ? 6 : null,
                QCTypeID = Constants.Ext.QC6,
                UserID = model.UserID?.Trim(),
                Password = SecurityManager.EnCryptPassword(model.Password),
                FirstName = model.FirstName?.Trim(),
                LastName = model.LastName?.Trim(),
                FirstName_Eng = model.FirstName_Eng?.Trim(),
                LastName_Eng = model.LastName_Eng?.Trim(),
                Email = model.Email?.Trim(),
                Mobile = model.Mobile?.Trim(),
                DepartmentID = model.DepartmentID,
                RoleID = model.RoleID,
                FlagAdmin = model.FlagAdmin ?? false,
                FlagReadonly = model.FlagReadonly ?? false,
                FlagActive = true,
                CreateDate = now,
                CreateBy = currentUserId,
                UpdateDate = now,
                UpdateBy = currentUserId
            };

            _context.tm_Users.Add(entity);
            _context.SaveChanges();

            // ===== Insert BU Mapping =====
            if (model.BUIds != null && model.BUIds.Count > 0)
            {

                foreach (var buId in model.BUIds.Distinct())
                {

                    var map = new tm_BU_Mapping
                    {
                        BUID = buId,
                        UserID = entity.ID,
                        FlagActive = true,
                        CreateDate = now,
                        CreateBy = currentUserId,
                        UpdateDate = now,
                        UpdateBy = currentUserId
                    };
                    _context.tm_BU_Mappings.Add(map);
                }
            }

            _context.SaveChanges();
            tx.Commit();

            return entity.ID;
        }

        public bool UpdateUser(UserAndPermissionModel.UpdateUserRequest model, int currentUserId)
        {
            using var tx = _context.Database.BeginTransaction();

            var entity = _context.tm_Users.FirstOrDefault(x => x.ID == model.ID && x.FlagActive == true);
            if (entity == null) return false;

            // --------- อัปเดตข้อมูลผู้ใช้หลัก ---------
            entity.TitleID = model.TitleID;
            if (model.TitleID.HasValue)
            {
                switch (model.TitleID.Value)
                {
                    case 1: entity.TitleID_Eng = 4; break; // นาย -> Mr.
                    case 2: entity.TitleID_Eng = 5; break; // นาง -> Mrs.
                    case 3: entity.TitleID_Eng = 6; break; // นางสาว -> Miss
                    default: entity.TitleID_Eng = null; break;
                }
            }
            else
            {
                entity.TitleID_Eng = null;
            }

            entity.UserID = model.UserID?.Trim();
            entity.FirstName = model.FirstName?.Trim();
            entity.LastName = model.LastName?.Trim();
            entity.FirstName_Eng = model.FirstName_Eng?.Trim();
            entity.LastName_Eng = model.LastName_Eng?.Trim();
            entity.Email = model.Email?.Trim();
            entity.Mobile = model.Mobile?.Trim();

            // อัปเดตรหัสผ่านเมื่อกรอกมาเท่านั้น
            if (!string.IsNullOrEmpty(model.Password))
            {
                entity.Password = SecurityManager.EnCryptPassword(model.Password);
            }

            entity.DepartmentID = model.DepartmentID;
            entity.RoleID = model.RoleID;

            entity.FlagAdmin = model.FlagAdmin;
            entity.FlagReadonly = model.FlagReadonly;

            entity.UpdateDate = DateTime.Now;
            entity.UpdateBy = currentUserId;

            _context.tm_Users.Update(entity);

            // --------- ซิงก์ BU Mapping ---------
            var now = DateTime.Now;

            // 1) ดึง mapping ทั้งหมดของ user นี้มาก่อน
            var existingMaps = _context.tm_BU_Mappings
                .Where(m => m.UserID == entity.ID)
                .ToList();

            // 2) ทำ inactive ทั้งหมด
            foreach (var m in existingMaps)
            {
                if (m.FlagActive) // ลดการเขียนทับโดยไม่จำเป็น
                {
                    m.FlagActive = false;
                    m.UpdateDate = now;
                    m.UpdateBy = currentUserId;
                    _context.tm_BU_Mappings.Update(m);
                }
            }

            // 3) เปิด active ตาม BUIds ใหม่ (และ insert ถ้ายังไม่เคยมี)
            var wanted = new HashSet<int>(model.BUIds ?? new List<int>());

            foreach (var buId in wanted)
            {
                var map = existingMaps.FirstOrDefault(x => x.BUID == buId);
                if (map != null)
                {
                    // มีอยู่แล้ว → เปิด active
                    if (!map.FlagActive)
                    {
                        map.FlagActive = true;
                        map.UpdateDate = now;
                        map.UpdateBy = currentUserId;
                        _context.tm_BU_Mappings.Update(map);
                    }
                }
                else
                {
                    // ไม่มี → insert ใหม่ (Active)
                    var newMap = new tm_BU_Mapping
                    {
                        BUID = buId,
                        UserID = entity.ID,
                        FlagActive = true,
                        CreateDate = now,
                        CreateBy = currentUserId,
                        UpdateDate = now,
                        UpdateBy = currentUserId
                    };
                    _context.tm_BU_Mappings.Add(newMap);
                }
            }

            // --------- commit ---------
            _context.SaveChanges();
            tx.Commit();

            return true;
        }

        public DuplicateCheckResult CheckDuplicate(string? email, string? userId, string? firstNameTh, string? lastNameTh, string? firstNameEn, string? lastNameEn, int? excludeId = null)
        {
            email = (email ?? string.Empty).Trim();
            userId = (userId ?? string.Empty).Trim();
            firstNameTh = (firstNameTh ?? string.Empty).Trim();
            lastNameTh = (lastNameTh ?? string.Empty).Trim();
            firstNameEn = (firstNameEn ?? string.Empty).Trim();
            lastNameEn = (lastNameEn ?? string.Empty).Trim();

            var q = _context.tm_Users.AsNoTracking().Where(x => x.FlagActive == true);

            if (excludeId.HasValue)
                q = q.Where(x => x.ID != excludeId.Value);

            // Create query variables
            int? emailId = null, userIdId = null, fullThId = null, fullEnId = null;

            // --- Email check (disabled) ---
            // if (!string.IsNullOrEmpty(email))
            // {
            //     emailId = q.Where(x => (x.Email ?? "").Trim().ToLower() == email.ToLower())
            //                .Select(x => (int?)x.ID)
            //                .FirstOrDefault();
            // }

            // --- UserID check (enabled) ---
            if (!string.IsNullOrEmpty(userId))
            {
                userIdId = q.Where(x => (x.UserID ?? "").Trim().ToLower() == userId.ToLower())
                            .Select(x => (int?)x.ID)
                            .FirstOrDefault();
            }

            // --- FullName TH check (disabled) ---
            // if (!string.IsNullOrEmpty(firstNameTh) && !string.IsNullOrEmpty(lastNameTh))
            // {
            //     fullThId = q.Where(x => (x.FirstName ?? "").Trim() == firstNameTh
            //                          && (x.LastName ?? "").Trim() == lastNameTh)
            //                 .Select(x => (int?)x.ID)
            //                 .FirstOrDefault();
            // }

            // --- FullName EN check (disabled) ---
            // if (!string.IsNullOrEmpty(firstNameEn) && !string.IsNullOrEmpty(lastNameEn))
            // {
            //     fullEnId = q.Where(x => (x.FirstName_Eng ?? "").Trim().ToLower() == firstNameEn.ToLower()
            //                          && (x.LastName_Eng ?? "").Trim().ToLower() == lastNameEn.ToLower())
            //                 .Select(x => (int?)x.ID)
            //                 .FirstOrDefault();
            // }

            return new DuplicateCheckResult
            {
                // EmailExists = emailId.HasValue,
                UserIdExists = userIdId.HasValue,
                // FullNameThExists = fullThId.HasValue,
                // FullNameEnExists = fullEnId.HasValue,

                // EmailConflictId = emailId,
                UserIdConflictId = userIdId,
                // FullNameThConflictId = fullThId,
                // FullNameEnConflictId = fullEnId
            };
        }

        public UserAndPermissionModel.UserDetail? GetDetailsUser(UserAndPermissionModel.FiltersGetlistUser filters)
        {
            UserAndPermissionModel.UserDetail? result = null;
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                                -- รวบรวม BUID เป็นสตริงในตัวแปร
                                DECLARE @BUMap NVARCHAR(MAX) = N'';

                                SELECT @BUMap =
                                    CASE WHEN @BUMap = N'' THEN CONVERT(NVARCHAR(20), M.BUID)
                                         ELSE @BUMap + N',' + CONVERT(NVARCHAR(20), M.BUID)
                                    END
                                FROM [tm_BU_Mapping] AS M WITH (NOLOCK)
                                WHERE M.[UserID] = @L_UserID
                                  AND M.[FlagActive] = 1
                                ORDER BY M.[BUID];

                                -- ยิง select ผู้ใช้ พร้อม BUMaping ที่ทำไว้
                                SELECT 
                                     T1.[ID]
                                    ,T1.[UserID]
                                    ,T1.[TitleID]
                                    ,T1.[FirstName]
                                    ,T1.[FirstName_Eng]
                                    ,T1.[TitleID_Eng]
                                    ,T1.[LastName]
                                    ,T1.[LastName_Eng]
                                    ,T1.[Password]
                                    ,T1.[Email]
                                    ,T1.[Mobile]
                                    ,T1.[DepartmentID]
                                    ,@BUMap AS BUMaping
                                    ,T1.[RoleID]
                                    ,T1.[FlagAdmin]
                                    ,T1.[FlagReadonly]
                                    ,T1.[FlagActive]
                                FROM [tm_User] AS T1 WITH (NOLOCK)
                                WHERE T1.[FlagActive] = 1
                                  AND T1.[ID] = @L_UserID;
                            ";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_UserID", (object?)filters?.L_UserID ?? DBNull.Value));

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new UserAndPermissionModel.UserDetail
                            {
                                ID = Commond.FormatExtension.Nulltoint(reader["ID"]),
                                UserID = Commond.FormatExtension.NullToString(reader["UserID"]),
                                TitleID = Commond.FormatExtension.Nulltoint(reader["TitleID"]),
                                FirstName = Commond.FormatExtension.NullToString(reader["FirstName"]),
                                LastName = Commond.FormatExtension.NullToString(reader["LastName"]),
                                FirstName_Eng = Commond.FormatExtension.NullToString(reader["FirstName_Eng"]),
                                LastName_Eng = Commond.FormatExtension.NullToString(reader["LastName_Eng"]),
                                Password = SecurityManager.DecodeFrom64(Commond.FormatExtension.NullToString(reader["Password"])),
                                Email = Commond.FormatExtension.NullToString(reader["Email"]),
                                Mobile = Commond.FormatExtension.NullToString(reader["Mobile"]),
                                DepartmentID = Commond.FormatExtension.Nulltoint(reader["DepartmentID"]),
                                BUMaping = Commond.FormatExtension.NullToString(reader["BUMaping"]),
                                RoleID = Commond.FormatExtension.Nulltoint(reader["RoleID"]),
                                FlagAdmin = reader["FlagAdmin"] != DBNull.Value && (bool)reader["FlagAdmin"],
                                FlagReadonly = reader["FlagReadonly"] != DBNull.Value && (bool)reader["FlagReadonly"]
                            };
                        }
                    }
                }
            }

            return result;
        }

        public List<UserAndPermissionModel.GetlistProjects> GetlistProjects(UserAndPermissionModel.FiltersGetlistUser filters)
        {
            var result = new List<UserAndPermissionModel.GetlistProjects>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                                SELECT T3.[Name] AS BUName
                                      ,T1.[ProjectID]
                                      ,T1.[ProjectName]
                                      ,T1.[ProjectName_Eng]
	                                  ,CASE WHEN T4.[UserID] IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS ISCheck
                                  FROM [tm_Project] T1 WITH (NOLOCK)
                                  LEFT JOIN [tm_BUProject_Mapping] T2 WITH (NOLOCK) ON T1.[ProjectID] = T2.ProjectID
                                  LEFT JOIN [tm_BU] T3 WITH (NOLOCK) ON T2.BUID = T3.ID
                                  LEFT JOIN [tm_ProjectUser_Mapping] T4 WITH (NOLOCK) ON T1.[ProjectID] = T4.ProjectID AND T4.UserID = @L_UserID
                                  WHERE T1.FlagActive = 1
                                  ORDER BY T3.[Name],T1.[ProjectName]
                                ";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_UserID", (object?)filters?.L_UserID ?? string.Empty));

                    using (var reader = cmd.ExecuteReader())
                    {
                        int idx = 0;
                        while (reader.Read())
                        {
                            idx++;

                            result.Add(new UserAndPermissionModel.GetlistProjects
                            {

                                index = idx
                                ,
                                BUName = Commond.FormatExtension.NullToString(reader["BUName"])
                                ,
                                ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"])
                                ,
                                ProjectName = Commond.FormatExtension.NullToString(reader["ProjectName"])
                                ,
                                ProjectName_Eng = Commond.FormatExtension.NullToString(reader["ProjectName_Eng"])
                                ,
                                ISCheck = reader["ISCheck"] != DBNull.Value && (bool)reader["ISCheck"]

                            });
                        }
                    }
                }
            }

            return result;
        }

        public bool IUDProjectUserMapping(UserAndPermissionModel.IUDProjectUserMapping model, int currentUserId)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            using var tx = _context.Database.BeginTransaction();
            try
            {
                var now = DateTime.Now;

                // 1) DELETE all existing mappings for this user
                var existing = _context.tm_ProjectUser_Mappings
                    .Where(m => m.UserID == model.UserID)
                    .ToList();

                if (existing.Count > 0)
                {
                    _context.tm_ProjectUser_Mappings.RemoveRange(existing);
                    _context.SaveChanges();
                }

                // 2) INSERT new mappings
                var projectIds = new HashSet<string>(
                    model.ProjectID ?? new List<string>(),
                    StringComparer.OrdinalIgnoreCase
                );

                foreach (var pid in projectIds.Where(p => !string.IsNullOrWhiteSpace(p)))
                {
                    var newMap = new tm_ProjectUser_Mapping
                    {
                        ProjectID = pid.Trim(),
                        UserID = model.UserID,
                        CreateDate = now,
                        CreateBy = currentUserId,
                        UpdateDate = now,
                        UpdateBy = currentUserId
                    };
                    _context.tm_ProjectUser_Mappings.Add(newMap); // ✅ correct DbSet
                }

                _context.SaveChanges();
                tx.Commit();
                return true;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        public List<UserAndPermissionModel.PermissionMatrixRow> GetPermissionMatrix(int qcTypeId = 10)
        {
            var result = new List<UserAndPermissionModel.PermissionMatrixRow>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Build the 3-level tree and pull action availability from tm_MenuAction
                string sql = @"
                    ;WITH MenuCTE AS (
                        -- Level 1 (Mom)
                        SELECT 
                            m.ID,
                            m.ParentID,
                            m.Name,
                            CAST(m.Name AS NVARCHAR(1000)) AS PathName,
                            1 AS Depth
                        FROM dbo.tm_Menu m WITH (NOLOCK)
                        WHERE m.QCTypeID = @QCTypeID
                          AND m.ParentID IS NULL

                        UNION ALL

                        -- Children/Grandchildren
                        SELECT 
                            c.ID,
                            c.ParentID,
                            c.Name,
                            CAST(p.PathName + N' / ' + c.Name AS NVARCHAR(1000)) AS PathName,
                            p.Depth + 1 AS Depth
                        FROM dbo.tm_Menu c WITH (NOLOCK)
                        JOIN MenuCTE p ON c.ParentID = p.ID
                        WHERE c.QCTypeID = @QCTypeID
                    ),
                    Agg AS (
                        SELECT 
                            t.ID AS MenuID, 
                            t.ParentID,
                            t.Name,
                            t.Depth,
                            -- If a row exists in tm_MenuAction, it defines which actions are available.
                            ISNULL(a.[View], 0)      AS CanView,
                            ISNULL(a.[Add], 0)       AS CanAdd,
                            ISNULL(a.[Update], 0)    AS CanEdit,
                            ISNULL(a.[Delete], 0)    AS CanDelete,
                            ISNULL(a.[Download], 0)  AS CanDownload,
                            -- Detect leaf: no child in tm_Menu
                            CASE 
                                WHEN EXISTS (
                                    SELECT 1 
                                    FROM dbo.tm_Menu ch WITH (NOLOCK) 
                                    WHERE ch.ParentID = t.ID 
                                      AND ch.QCTypeID = @QCTypeID
                                ) THEN CAST(0 AS BIT) 
                                ELSE CAST(1 AS BIT) 
                            END AS IsLeaf,
                            -- Parent name (names only)
                            (SELECT TOP (1) p.Name FROM dbo.tm_Menu p WITH (NOLOCK) WHERE p.ID = t.ParentID) AS ParentName,
                            -- Sorting key (keeps hierarchy order stable)
                            t.PathName
                        FROM MenuCTE t
                        LEFT JOIN dbo.tm_MenuAction a WITH (NOLOCK) ON a.MenuID = t.ID
                    )
                    SELECT 
                        MenuID,
                        Name,
                        Depth AS [Level],
                        CanView,
                        CanAdd,
                        CanEdit,
                        CanDelete,
                        CanDownload,
                        IsLeaf,
                        ParentName
                    FROM Agg
                    ORDER BY PathName;
                ";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@QCTypeID", qcTypeId));

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new UserAndPermissionModel.PermissionMatrixRow
                            {
                                MenuID = Commond.FormatExtension.Nulltoint(reader["MenuID"]),
                                Name = Commond.FormatExtension.NullToString(reader["Name"]),
                                Level = Commond.FormatExtension.Nulltoint(reader["Level"]),
                                CanView = reader["CanView"] != DBNull.Value && (bool)reader["CanView"],
                                CanAdd = reader["CanAdd"] != DBNull.Value && (bool)reader["CanAdd"],
                                CanEdit = reader["CanEdit"] != DBNull.Value && (bool)reader["CanEdit"],
                                CanDelete = reader["CanDelete"] != DBNull.Value && (bool)reader["CanDelete"],
                                CanDownload = reader["CanDownload"] != DBNull.Value && (bool)reader["CanDownload"],
                                IsLeaf = reader["IsLeaf"] != DBNull.Value && (bool)reader["IsLeaf"],
                                ParentName = Commond.FormatExtension.NullToString(reader["ParentName"])
                            });
                        }
                    }
                }
            }

            return result;
        }

        public bool SaveRolePermissions(UserAndPermissionModel.SaveRolePermissionRequest req, int currentUserId)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));

            using var tx = _context.Database.BeginTransaction();
            var now = DateTime.Now;

            try
            {
                // Build Name->ID map once (QC-scoped) for when Items only provide Name
                var nameToId = _context.tm_Menus
                    .AsNoTracking()
                    .Where(m => m.QCTypeID == req.QCTypeID)
                    .ToDictionary(m => (m.Name ?? "").Trim(), m => m.ID);

                // 1) Wipe existing rows for this Dept/Role/QC
                _context.Database.ExecuteSqlRaw(@"DELETE FROM dbo.TR_MenuRolePermission WHERE QCTypeID = {0} AND DepartmentID = {1} AND RoleID = {2};", req.QCTypeID, req.DepartmentID, req.RoleID);

                // 2) Insert only rows that have any action = true
                foreach (var it in req.Items ?? Enumerable.Empty<UserAndPermissionModel.PermissionSelectionItem>())
                {
                    bool any = it.View || it.Add || it.Update || it.Delete || it.Download;
                    if (!any) continue;

                    int menuId;
                    if (it.MenuID.HasValue)
                    {
                        menuId = it.MenuID.Value;
                    }
                    else
                    {
                        var key = (it.Name ?? "").Trim();
                        if (string.IsNullOrEmpty(key) || !nameToId.TryGetValue(key, out menuId))
                            continue; // cannot resolve -> skip
                    }

                    var row = new TR_MenuRolePermission
                    {
                        QCTypeID = req.QCTypeID,
                        MenuID = menuId,
                        DepartmentID = req.DepartmentID,
                        RoleID = req.RoleID,
                        View = it.View,
                        Add = it.Add,
                        Update = it.Update,
                        Delete = it.Delete,
                        Download = it.Download,
                        FlagActive = true,
                        CreateDate = now,
                        CreateBy = currentUserId,
                        UpdateDate = now,
                        UpdateBy = currentUserId
                    };

                    _context.Set<TR_MenuRolePermission>().Add(row);
                }

                _context.SaveChanges();
                tx.Commit();
                return true;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }



    }
}
