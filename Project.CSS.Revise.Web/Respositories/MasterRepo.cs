using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Master;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IMasterRepo
    {
        public List<BUModel> GetlistBU(BUModel model);
        public List<ProjectModel> GetlistPrject(ProjectModel model);
    }
    public class MasterRepo : IMasterRepo
    {
        private readonly CSSContext _context;

        public MasterRepo(CSSContext context)
        {
            _context = context;
        }

        public List<BUModel> GetlistBU(BUModel model)
        {
            var query = _context.tm_BUs.Where(g => g.FlagActive == true)
                                        .Select(t1 => new BUModel
                                        {
                                            ID = t1.ID,
                                            Name = t1.Name
                                        }).ToList();
            return query;
        }
        public List<ProjectModel> GetlistPrject(ProjectModel filter)
        {
            List<ProjectModel> result = new List<ProjectModel>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                                SELECT 
                                    p.ProjectID, 
                                    b.ID AS BUID, 
                                    b.Name AS BUname, 
                                    p.ProjectName, 
                                    p.ProjectName_Eng
                                FROM tm_Project p
                                LEFT JOIN tm_BUProject_Mapping m ON p.ProjectID = m.ProjectID
                                LEFT JOIN tm_BU b ON m.BUID = b.ID
                                WHERE p.FlagActive = 1
                                  AND (
                                        @L_BUID = ''
                                        OR CHARINDEX(',' + CAST(b.ID AS NVARCHAR) + ',', ',' + @L_BUID + ',') > 0
                                    )
                             ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_BUID", filter.L_BUID ?? ""));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ProjectModel
                            {
                                ProjectID = reader["ProjectID"].ToString(),
                                ProjectNameTH = reader["ProjectName"].ToString(),
                                ProjectNameEN = reader["ProjectName_Eng"].ToString()
                                // BUname และ BUID สามารถเพิ่มได้ ถ้าอยากเก็บไว้ใน Model
                            });
                        }
                    }
                }
            }

            return result;
        }



    }
}
