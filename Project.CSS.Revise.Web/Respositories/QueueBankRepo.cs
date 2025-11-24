using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.QueueBank;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IQueueBankRepo
    {
        public List<ListUnitForRegisterBankModel> GetListUnitForRegisterBank(string ProjectID);
    }
    public class QueueBankRepo : IQueueBankRepo
    {
        private readonly CSSContext _context;

        public QueueBankRepo(CSSContext context)
        {
            _context = context;
        }

        public List<ListUnitForRegisterBankModel> GetListUnitForRegisterBank(string ProjectID)
        {
            List<ListUnitForRegisterBankModel> result = new List<ListUnitForRegisterBankModel>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                                SELECT 
                                      u.ID
                                    , u.UnitCode
                                    , u.AddrNo
                                FROM tm_Unit u
                                WHERE u.ID NOT IN (
                                    SELECT UnitID
                                    FROM TR_RegisterLog
                                    WHERE FlagActive = 1  
                                      AND QCTypeID = 10
                                      AND QueueTypeID = 48
                                )
                                AND u.ProjectID = @L_ProjectID
                                AND u.FlagActive = 1
                                ORDER BY u.UnitCode;
                            ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", ProjectID ?? string.Empty));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int idx = 1; 

                        while (reader.Read())
                        {
                            result.Add(new ListUnitForRegisterBankModel
                            {
                                Index = idx++, 
                                ID = Commond.FormatExtension.NulltoGuid(reader["ID"]),
                                UnitCode = Commond.FormatExtension.NullToString(reader["UnitCode"]),
                                AddrNo = Commond.FormatExtension.NullToString(reader["AddrNo"]),
                            });
                        }
                    }
                }
            }

            return result;
        }

    }
}
