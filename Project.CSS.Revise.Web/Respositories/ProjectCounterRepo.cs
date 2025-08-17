using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.ProjectCounter;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IProjectCounterRepo
    {
        public List<ProjectCounterMappingModel.ListData> GetListsProjectCounterMapping(ProjectCounterMappingModel.FilterData filter);
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
    }
}
