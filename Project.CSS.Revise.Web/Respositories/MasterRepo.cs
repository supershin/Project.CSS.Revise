using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.Shop_Event;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IMasterRepo
    {
        public List<BUModel> GetlistBU(BUModel model);
        public List<ProjectModel> GetlistPrject(ProjectModel model);
        public List<EventsModel> GetlistEvents(EventsModel model);
        public List<Monthevents> GetlistCountEventByMonth(Monthevents model);
        public List<GetDDLModel> GetlisDDl(GetDDLModel model);
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
                                IF @L_Month = ''
                                BEGIN
                                    -- 📅 กรณีดูทั้งปี
                                    SELECT 
	                                       [ID]
                                          ,[ProjectID]
                                          ,[Name]
                                          ,[Location]
                                          ,[StartDate]
                                          ,[EndDate]
                                    FROM [tm_Event]
                                    WHERE 
                                        ProjectID = @L_ProjectID
                                        AND FlagActive = 1
                                        AND (
                                            YEAR(StartDate) = CAST(@L_Year AS INT)
                                            OR YEAR(EndDate) = CAST(@L_Year AS INT)
                                        )
                                    ORDER BY StartDate;
                                END
                                ELSE
                                BEGIN
                                    -- 📅 กรณีระบุเดือน (เช่น 2 = Feb)
                                    SELECT 	       
	                                       [ID]
                                          ,[ProjectID]
                                          ,[Name]
                                          ,[Location]
                                          ,[StartDate]
                                          ,[EndDate]
                                    FROM [tm_Event]
                                    WHERE 
                                        ProjectID = @L_ProjectID
                                        AND FlagActive = 1
                                        AND (
                                            (
                                                YEAR(StartDate) = CAST(@L_Year AS INT)
                                                AND MONTH(StartDate) = CAST(@L_Month AS INT)
                                            )
                                            OR (
                                                YEAR(EndDate) = CAST(@L_Year AS INT)
                                                AND MONTH(EndDate) = CAST(@L_Month AS INT)
                                            )
                                            OR (
                                                StartDate <= EOMONTH(CONVERT(DATE, @L_Year + '-' + @L_Month + '-01'))
                                                AND EndDate >= CONVERT(DATE, @L_Year + '-' + @L_Month + '-01')
                                            )
                                        )
                                    ORDER BY StartDate;
                                END
                            ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", filter.L_ProjectID ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Month", filter.L_month ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Year", filter.L_year ?? ""));

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
                                Color = GenerateUniqueColor()
                            });
                        }
                    }
                }
            }


            return result;
        }

        public List<Monthevents> GetlistCountEventByMonth(Monthevents filter)
        {
            List<Monthevents> result = new List<Monthevents>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                                SELECT 
                                    MonthName,
                                    MonthNumber,
                                    COUNT(*) AS EventCount
                                FROM (
                                    SELECT 
                                        DATENAME(MONTH, [StartDate]) AS MonthName,
                                        MONTH([StartDate]) AS MonthNumber,
                                        [ID]
                                    FROM [tm_Event]
                                    WHERE [ProjectID] = @L_ProjectID
                                      AND [FlagActive] = 1
                                      AND YEAR([StartDate]) = @L_Year

                                    UNION

                                    SELECT 
                                        DATENAME(MONTH, [EndDate]) AS MonthName,
                                        MONTH([EndDate]) AS MonthNumber,
                                        [ID]
                                    FROM [tm_Event]
                                    WHERE [ProjectID] = @L_ProjectID
                                      AND [FlagActive] = 1
                                      AND YEAR([EndDate]) = @L_Year
                                      AND MONTH([StartDate]) <> MONTH([EndDate]) -- ✅ ป้องกันซ้ำเดือนเดียวกัน
                                ) AS Combined
                                GROUP BY MonthName, MonthNumber
                                ORDER BY MonthNumber
                            ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", filter.L_ProjectID ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Year", filter.L_year ?? ""));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Monthevents
                            {
                                MonthName = reader["MonthName"].ToString(),
                                MonthNumber = Convert.ToInt32(reader["MonthNumber"]),
                                EventCount = Convert.ToInt32(reader["EventCount"])
                            });
                        }
                    }
                }
            }

            return result;
        }

        public List<GetDDLModel> GetlisDDl(GetDDLModel model)
        {
            switch (model.Act)
            {
                case "Ext":
                    var extQuery = from ext in _context.tm_Exts
                                   where ext.ExtTypeID == model.ID && ext.FlagActive == true
                                   orderby ext.LineOrder
                                   select new GetDDLModel
                                   {
                                       ValueInt = ext.ID,
                                       Text = ext.Name
                                   };

                    return extQuery.ToList();

                case "listDDlAllProject":
                    var ListProject = from t1 in _context.tm_Projects
                                      join t2 in _context.tm_BUProject_Mappings on t1.ProjectID equals t2.ProjectID into joined
                                      from t2 in joined.DefaultIfEmpty()
                                      join t3 in _context.tm_BUs on t2.BUID equals t3.ID into joinedt3
                                      from t3 in joinedt3.DefaultIfEmpty()
                                      where t1.FlagActive == true && t3.FlagActive == true
                                      select new GetDDLModel
                                      {
                                          ValueString = t1.ProjectID,
                                          Text = t1.ProjectName
                                      };

                   return ListProject.ToList();

                case "listAlltag":
                    var ListAlltag = from t1 in _context.tm_Tags
                                     where t1.FlagActive == true 
                                      select new GetDDLModel
                                      {
                                          ValueInt = t1.ID,
                                          Text =  t1.Name
                                      };

                    return ListAlltag.ToList();

                case "listEventdateByID":
                    var eventItem = _context.tm_Events.FirstOrDefault(e => e.ID == model.ID);

                    if (eventItem != null)
                    {
                        if (eventItem?.StartDate == null || eventItem?.EndDate == null)
                        {
                            return new List<GetDDLModel>();
                        }
                        else
                        {
                            var dateList = Enumerable.Range(0, (eventItem.EndDate.Value.Date - eventItem.StartDate.Value.Date).Days + 1)
                                        .Select(offset => eventItem.StartDate.Value.Date.AddDays(offset))
                                        .Select(date => new GetDDLModel
                                        {
                                            Text = Commond.FormatExtension.FormatDateToThaiShortString(date),
                                            ValueString = date.ToString("yyyy-MM-dd")
                                        }).ToList();

                            return dateList;
                        }
                    }
                    return new List<GetDDLModel>();

                case "listEventProjectByID":
                    var EventProject = from t1 in _context.TR_ProjectEvents
                                       join t2 in _context.tm_Projects on t1.ProjectID equals t2.ProjectID into joined
                                       from t2 in joined.DefaultIfEmpty()
                                       where t1.FlagActive == true && t1.EventID == model.ID
                                       select new GetDDLModel
                                      {
                                          ValueString = t1.ProjectID,
                                          Text = t2.ProjectName
                                       };

                    return EventProject.ToList();

                case "listShop":
                    var listShop = from t1 in _context.tm_Shops
                                    where t1.FlagActive == true
                                    select new GetDDLModel
                                    {
                                        ValueInt = t1.ID,
                                        Text = t1.Name
                                    };

                    return listShop.ToList();

                case "DDLEventType":
                    var listEventType = from t1 in _context.tm_EventTypes
                                        where t1.FlagActive == true
                                   select new GetDDLModel
                                   {
                                       ValueInt = t1.ID,
                                       Text = t1.Name,
                                       Color = t1.ColorCode
                                   };

                    return listEventType.ToList();

                case "listEventInID":
                    {
                        var valueString = model.ValueString?.Trim(',');
                        if (string.IsNullOrWhiteSpace(valueString))
                            return new List<GetDDLModel>();

                        var result = new List<GetDDLModel>();
                        var connectionString = _context.Database.GetDbConnection().ConnectionString;

                        using (var conn = new SqlConnection(connectionString))
                        {
                            conn.Open();

                            string sql = $@"
                                            SELECT 
                                                T1.[ID] AS EVENTID,
                                                T1.[ProjectID],
                                                T2.ProjectName
                                            FROM [tm_Event] T1
                                            LEFT JOIN [tm_Project] T2 ON T1.ProjectID = T2.[ProjectID]
                                            WHERE T1.FlagActive = 1
                                              AND T1.ID IN ({valueString})
                                        ";

                            using (var cmd = new SqlCommand(sql, conn))
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    result.Add(new GetDDLModel
                                    {
                                        ValueInt = Convert.ToInt32(reader["EVENTID"]),
                                        ValueString = reader["ProjectID"].ToString(),
                                        Text = reader["ProjectName"].ToString()
                                    });
                                }
                            }
                        }

                        return result;
                    }




                default:

                return new List<GetDDLModel>();
            }
        }
    }
}
