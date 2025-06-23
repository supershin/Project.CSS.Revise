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
        public List<EventsModel> GetlistEvents(EventsModel model);
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
                                ORDER BY b.ID
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
                                ProjectNameTH = reader["BUname"].ToString() + " - " + reader["ProjectName"].ToString(),
                                ProjectNameEN = reader["ProjectName_Eng"].ToString()
                                // BUname และ BUID สามารถเพิ่มได้ ถ้าอยากเก็บไว้ใน Model
                            });
                        }
                    }
                }
            }

            return result;
        }

        public List<EventsModel> GetlistEvents(EventsModel filter)
        {
            List<EventsModel> result = new List<EventsModel>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            // 🎨 Set สำหรับเก็บสีที่เคยใช้แล้ว
            HashSet<string> usedColors = new HashSet<string>();
            Random rand = new Random();

            // 🔧 ฟังก์ชันสุ่มสีแบบไม่ซ้ำ
            string GenerateUniqueColor()
            {
                string color;
                do
                {
                    color = $"#{rand.Next(0x1000000):X6}"; // สุ่ม hex เช่น #A3F2C1
                }
                while (usedColors.Contains(color));

                usedColors.Add(color);
                return color;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                                SELECT [ID]
                                      ,[ProjectID]
                                      ,[Name]
                                      ,[Location]
                                      ,[StartDate]
                                      ,[EndDate]
                                FROM [CSS_UAT_2].[dbo].[tm_Event]
                                WHERE ProjectID = @L_ProjectID
                                  AND FlagActive = 1
                                  AND CONVERT(DATE, [StartDate]) >= CONVERT(DATE, @L_Startdate)
                                  AND CONVERT(DATE, [EndDate]) <= CONVERT(DATE, @L_Enddate)
                                ORDER BY [StartDate]
                            ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", filter.L_ProjectID ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Startdate", filter.L_Startdate ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Enddate", filter.L_Enddate ?? ""));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new EventsModel
                            {
                                Name = reader["Name"].ToString(),
                                Location = reader["Location"].ToString(),
                                StartDate = reader["StartDate"].ToString(),
                                EndDate = reader["EndDate"].ToString(),
                                Color = GenerateUniqueColor() // 🎨 สุ่มสีไม่ซ้ำ
                            });
                        }
                    }
                }
            }

            return result;
        }




    }
}
