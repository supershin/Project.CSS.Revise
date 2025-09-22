using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.UserAndPermission;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IUserAndPermissionRepo
    {
        List<UserAndPermissionModel.GetlistUser> GetlistUser(UserAndPermissionModel.FiltersGetlistUser filters);
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
                                     T1.[DepartmentID]
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


    }
}
