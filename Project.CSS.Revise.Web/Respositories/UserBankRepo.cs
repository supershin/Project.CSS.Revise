using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.ProjectCounter;
using Project.CSS.Revise.Web.Models.Pages.Shop_Event;
using Project.CSS.Revise.Web.Models.Pages.UserBank;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IUserBankRepo
    {
        public List<GetlistUserBank.ListData> GetListUserBank(GetlistUserBank.FilterData filter);
    }
    public class UserBankRepo : IUserBankRepo
    {
        private readonly CSSContext _context;

        public UserBankRepo(CSSContext context)
        {
            _context = context;
        }

        public List<GetlistUserBank.ListData> GetListUserBank(GetlistUserBank.FilterData filter)
        {
            List<GetlistUserBank.ListData> result = new List<GetlistUserBank.ListData>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                            -- ===== TEST CASE =====
                            --DECLARE @L_BankIDs NVARCHAR(100) = N'';
                            --DECLARE @L_Name    NVARCHAR(100) = N'';
                            -- ===== TEST CASE =====

                            SELECT
                                 U.[ID]
                                ,U.[FirstName] + N' ' + U.[LastName] AS FullName
	                            ,B.[BankCode]
                                ,B.[BankName]
                                ,CASE 
                                    WHEN U.[IsLeadBank] = 1                 THEN 1
                                    WHEN U.[ParentBankID] IS NOT NULL       THEN 2
                                    ELSE 3
                                  END AS Role

                            FROM [PR_User] U WITH (NOLOCK)
                            LEFT JOIN [PR_UserBank_Mapping] UBM WITH (NOLOCK) ON U.[ID] = UBM.[UserID]
                            LEFT JOIN [tm_Bank] B WITH (NOLOCK) ON UBM.[BankID] = B.[ID]
                            WHERE
                                  U.[FlagActive] = 1
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
                                  B.[BankCode]
                                 ,COALESCE(U.[ParentBankID], U.[ID])
	                             ,CASE WHEN U.[ParentBankID] IS NULL OR U.[IsLeadBank] = 1 THEN 0 ELSE 1 END -- LeaderID: กลุ่มเดียวกัน
	                             , U.[FirstName] COLLATE Thai_CI_AS;
                ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_BankIDs", filter.L_BankIDs ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Name", filter.L_Name ?? ""));

                    using (SqlDataReader reader = cmd.ExecuteReader())
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
            }


            return result;
        }
    }
}
