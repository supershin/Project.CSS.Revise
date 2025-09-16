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
                                   where ext.FlagActive == true && (model.ID == -1 || ext.ExtTypeID == model.ID)
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

                case "ListProjectCounterMapping":
                {
                    var result = new List<GetDDLModel>();
                    var connectionString = _context.Database.GetDbConnection().ConnectionString;

                    using (var conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string sql = $@"
                                        SELECT P.ProjectID
                                                , P.ProjectName
                                        FROM tm_Project AS P WITH (NOLOCK)
                                        WHERE NOT (
                                            EXISTS (
                                                SELECT 1
                                                FROM TR_ProjectCounter_Mapping AS M WITH (NOLOCK)
                                                WHERE M.ProjectID = P.ProjectID AND M.QueueTypeID = 48
                                            )
                                            AND
                                            EXISTS (
                                                SELECT 1
                                                FROM TR_ProjectCounter_Mapping AS M WITH (NOLOCK)
                                                WHERE M.ProjectID = P.ProjectID AND M.QueueTypeID = 49
                                            )
                                        )
                                        ";

                        using (var cmd = new SqlCommand(sql, conn))
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new GetDDLModel
                                {
                                    ValueString = reader["ProjectID"].ToString(),
                                    Text = reader["ProjectName"].ToString()
                                });
                            }
                        }
                    }
                    return result;
                }

                case "listAllBank":
                    var listAllBank = from t1 in _context.tm_Banks
                                      where t1.FlagActive == true
                                      orderby t1.LineOrder
                                     select new GetDDLModel
                                     {
                                         ValueInt = t1.ID,
                                         ValueString = t1.BankCode,
                                         Text = t1.BankName
                                     };

                    return listAllBank.ToList();

                case "listUserBankInTeamForAdd":
                    var listUserBankInTeamForAdd =
                        from u in _context.PR_Users.AsNoTracking()
                        join ubm in _context.PR_UserBank_Mappings.AsNoTracking()
                            on u.ID equals ubm.UserID into j1
                        from ubm in j1.DefaultIfEmpty() // LEFT JOIN
                        join b in _context.tm_Banks.AsNoTracking()
                            on ubm.BankID equals b.ID into j2
                        from b in j2.DefaultIfEmpty()   // LEFT JOIN
                        where u.FlagActive == true
                           && u.UserTypeID == Constants.Ext.UserBank
                           && u.IsLeadBank == false
                           && b.ID == model.ID             // << ธนาคารที่ต้องการ filter
                           && (u.ParentBankID == null || u.ParentBankID != model.ID2)
                        select new GetDDLModel
                        {
                            ValueInt = u.ID,
                            Text = (u.ParentBankID != null)
                                ? (u.FirstName + " " + u.LastName + "  (มีทีมแล้ว) ")
                                : (u.FirstName + " " + u.LastName)
                        };

                return listUserBankInTeamForAdd.ToList();

                case "listAllCSUser":
                {
                    var listAllCSUser =
                        from t1 in _context.tm_Users
                        where t1.FlagActive == true
                            && t1.DepartmentID == Constants.Ext.Customer_Service   // = 31
                            && t1.QCTypeID == 10
                        join t2 in _context.tm_TitleNames
                                on t1.TitleID equals t2.ID into gj
                        from t2 in gj.DefaultIfEmpty() // LEFT JOIN
                        select new GetDDLModel
                        {
                            ValueInt = t1.ID,
                            Text = (
                                (t2.Name ?? "") + " " +
                                (t1.FirstName ?? "") + " " +
                                (t1.LastName ?? "")
                            ).Trim()
                        };

                    return listAllCSUser.ToList();
                }

                case "listBuildInProject":
                    {
                        var projectId = (model?.IDString ?? model?.ValueString ?? string.Empty).Trim();

                        if (string.IsNullOrWhiteSpace(projectId))
                        {
                            return new List<GetDDLModel>();
                        }
                            
                        var builds =
                            _context.tm_Units
                                .AsNoTracking()
                                .Where(u => u.FlagActive == true
                                            && u.ProjectID == projectId
                                            && !string.IsNullOrEmpty(u.Build))
                                .Select(u => u.Build!)
                                .Distinct()
                                .OrderBy(b => b)
                                .Select(b => new GetDDLModel
                                {
                                    ValueString = b,
                                    Text = b
                                });

                        return builds.ToList();
                    }

                case "ListFloorInBuildInProject":
                    {
                        var result = new List<GetDDLModel>();
                        var connectionString = _context.Database.GetDbConnection().ConnectionString;

                        using (var conn = new SqlConnection(connectionString))
                        {
                            conn.Open();

                            string sql = $@"
                                            -- ===== TEST CASE =====
                                            --DECLARE @ProjectID nvarchar(100) = N'122C001';
                                            --DECLARE @Build     nvarchar(100) = N'B,C';
                                            -- ===== TEST CASE =====

                                            SELECT
                                                  x.[Value]
                                                , x.[Text]
                                            FROM (
                                                SELECT DISTINCT
                                                      CONVERT(varchar(50), U.[Build]) + N'-' + CONVERT(varchar(50), U.[Floor]) AS [Value]
                                                    , N'ตึก ' + CONVERT(varchar(50), U.[Build]) + N' ชั้น ' + CONVERT(varchar(50), U.[Floor]) AS [Text]
                                                    , U.[Build]
                                                    , U.[Floor]
                                                FROM dbo.tm_Unit AS U
                                                WHERE U.FlagActive = 1
                                                  AND U.ProjectID  = @ProjectID
                                                  AND (
                                                         @Build = N''
                                                      OR (',' + @Build + ',' LIKE '%,' + CONVERT(varchar(50), U.[Build]) + ',%')
                                                      )
                                            ) AS x
                                            ORDER BY
                                                  x.[Build]
                                                , x.[Floor];
                                        ";

                            using (var cmd = new SqlCommand(sql, conn))
                            {
                                cmd.Parameters.Add(new SqlParameter("@ProjectID", model.IDString ?? ""));
                                cmd.Parameters.Add(new SqlParameter("@Build", model.IDString2 ?? ""));

                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        result.Add(new GetDDLModel
                                        {
                                            ValueString = reader["Value"].ToString(),
                                            Text = reader["Text"].ToString()
                                        });
                                    }
                                }
                            }
                        }
                        return result;
                    }

                case "ListUnitInFloorInBuildInProject":
                    {
                        var result = new List<GetDDLModel>();
                        var connectionString = _context.Database.GetDbConnection().ConnectionString;

                        using (var conn = new SqlConnection(connectionString))
                        {
                            conn.Open();

                            string sql = $@"
                                            -- ===== TEST CASE =====
                                            --DECLARE @ProjectID nvarchar(100) = N'122C001';
                                            --DECLARE @Build     nvarchar(100) = N'B,C';
                                            --DECLARE @Pairs     nvarchar(4000) = N'A-3,A-4,B-1,B-2,B-3';
                                            -- ===== TEST CASE =====

                                            SELECT
                                                  [UnitCode] As [Value]
	                                            , [UnitCode] As [Text]
                                            FROM  dbo.tm_Unit AS U
                                            WHERE U.FlagActive = 1
                                              AND U.ProjectID  = @ProjectID
                                              AND (
                                                     -- 1) ถ้าไม่ส่ง @Build มา → ผ่าน
                                                     @Build = N''
                                                     -- 2) ถ้าส่ง @Build มา → กรองด้วย CSV LIKE (ใช้ได้ทุกเวอร์ชัน)
                                                  OR (',' + @Build + ',' LIKE '%,' + CONVERT(varchar(50), U.[Build]) + ',%')
                                                  )
                                              AND (
                                                     -- 3) ถ้าไม่ส่ง @Pairs มา → ผ่าน
                                                     @Pairs = N''
                                                     -- 4) ถ้าส่ง @Pairs มา → จับคู่ Build-Floor แบบเป๊ะ ๆ
                                                  OR (',' + @Pairs + ',' LIKE '%,' + CONVERT(varchar(50), U.[Build]) + '-' + CONVERT(varchar(50), U.[Floor]) + ',%')
                                                  )
                                            ORDER BY
                                                  U.[Build]
                                                , U.[Floor]
                                                , U.[Room]
                                        ";

                            using (var cmd = new SqlCommand(sql, conn))
                            {
                                cmd.Parameters.Add(new SqlParameter("@ProjectID", model.IDString ?? ""));
                                cmd.Parameters.Add(new SqlParameter("@Build", model.IDString2 ?? ""));
                                cmd.Parameters.Add(new SqlParameter("@Pairs", model.IDString3 ?? ""));

                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        result.Add(new GetDDLModel
                                        {
                                            ValueString = reader["Value"].ToString(),
                                            Text = reader["Text"].ToString()
                                        });
                                    }
                                }
                            }
                        }
                        return result;
                    }

                case "ListArea":
                    var ListArea = from ext in _context.tm_Exts
                                   where ext.FlagActive == true && ext.ExtTypeID == 67
                                   orderby ext.LineOrder
                                   select new GetDDLModel
                                   {
                                       ValueInt = ext.ID,
                                       Text = ext.Name
                                   };

                    return ListArea.ToList();

                default:

                return new List<GetDDLModel>();
            }
        }
    }
}
