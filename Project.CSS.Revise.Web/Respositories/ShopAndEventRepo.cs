using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.Shop_Event;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IShopAndEventRepo
    {
        public CreateEventsTagsResponse CreateEventsAndTags(CreateEvents_Tags model);
        public CreateEventsShopsResponse CreateEventsAndShops(CreateEvent_Shops model);
        public GetDataCreateEvent_Shops GetDataCreateEventsAndShops(GetDataCreateEvent_Shops filter);
        public List<GetListShopAndEventCalendar.ListData> GetListShopAndEventSCalendar(GetListShopAndEventCalendar.FilterData filter);
    }
    public class ShopAndEventRepo : IShopAndEventRepo
    {
        private readonly CSSContext _context;

        public ShopAndEventRepo(CSSContext context)
        {
            _context = context;
        }

        public CreateEventsTagsResponse CreateEventsAndTags(CreateEvents_Tags model)
        {
            var response = new CreateEventsTagsResponse();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var newTagIds = new List<int>();

                    // ✅ 1. ตรวจสอบและเพิ่ม Tag ใหม่
                    if (model.TagItems != null)
                    {
                        foreach (var tag in model.TagItems)
                        {
                            var existingTag = _context.tm_Tags.FirstOrDefault(t => t.Name == tag.Value && t.FlagActive == true);

                            if (existingTag != null)
                            {
                                newTagIds.Add(existingTag.ID);
                            }
                            else
                            {
                                var newTag = new tm_Tag
                                {
                                    Name = tag.Value?.Trim(),
                                    FlagActive = true,
                                    CreateDate = DateTime.Now,
                                    CreateBy = model.UserID,
                                    UpdateDate = DateTime.Now,
                                    UpdateBy = model.UserID
                                };
                                _context.tm_Tags.Add(newTag);
                                _context.SaveChanges();
                                newTagIds.Add(newTag.ID);
                            }
                        }
                    }

                    var eventIdsCreated = new List<int>(); // 👉 เก็บ EventID ที่ถูกสร้าง

                    // ✅ 2. สร้าง Event เพียงครั้งเดียว
                    var newEvent = new tm_Event
                    {
                        Name = model.EventName?.Trim(),
                        Location = model.EventLocation?.Trim(),
                        StartDate = Commond.FormatExtension.ToDateFromddmmyyy(model.StartDateTime!),
                        EndDate = Commond.FormatExtension.ToDateFromddmmyyy(model.EndDateTime!),
                        FlagActive = model.IsActive,
                        CraeteDate = DateTime.Now,
                        CreateBy = model.UserID,
                        UpdateDate = DateTime.Now,
                        UpdateBy = model.UserID
                    };

                    _context.tm_Events.Add(newEvent);
                    _context.SaveChanges(); // 👈 Insert only once

                    // ✅ 3. เพิ่มความสัมพันธ์ Project ↔ Event
                    if (model.ProjectIds != null && model.ProjectIds.Any())
                    {
                        foreach (var projectId in model.ProjectIds)
                        {
                            var relation = new TR_ProjectEvent
                            {
                                ProjectID = projectId,
                                EventID = newEvent.ID,
                                FlagActive = true,
                                CreateDate = DateTime.Now,
                                CreateBy = model.UserID,
                                UpdateDate = DateTime.Now,
                                UpdateBy = model.UserID
                            };
                            _context.TR_ProjectEvents.Add(relation);
                        }
                        _context.SaveChanges(); // save all TR_ProjectEvent records
                    }

                    // ✅ 4. เพิ่มความสัมพันธ์ Tag ↔ Event
                    foreach (var tagId in newTagIds)
                    {
                        var tagEvent = new TR_TagEvent
                        {
                            EventID = newEvent.ID,
                            TagID = tagId
                        };
                        _context.TR_TagEvents.Add(tagEvent);
                    }
                    _context.SaveChanges(); // save all TR_TagEvent



                    // ✅ 5. เพิ่มความสัมพันธ์ EventType ↔ Event
                    var NewEventType = new tm_EventType
                    {
                        Name = model.EventType?.Trim(),
                        EventID = newEvent.ID,
                        ColorCode = model.EventColor?.Trim(),
                        FlagActive = true,
                        CraeteDate = DateTime.Now,
                        CreateBy = model.UserID,
                        UpdateDate = DateTime.Now,
                        UpdateBy = model.UserID

                    };
                    _context.tm_EventTypes.Add(NewEventType);
                    _context.SaveChanges();

                    // ✅ 6. บันทึกการเปลี่ยนแปลงทั้งหมด
                    transaction.Commit();

                    // ✅ 7. ส่งผลลัพธ์กลับ
                    response.ID = newEvent.ID;
                    response.Message = "Event and tags created successfully, and tag linkage completed.";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var message = ex.InnerException != null
                        ? $"INNER ERROR: {ex.InnerException.Message}"
                        : $"ERROR: {ex.Message}";

                    response.Message = $"An error occurred: {message}";
                }

            }

            return response;
        }

        public CreateEventsShopsResponse CreateEventsAndShops(CreateEvent_Shops model)
        {
            var response = new CreateEventsShopsResponse();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var newShopsIds = new List<int>();

                    // ✅ 1. ตรวจสอบและเพิ่ม Shops ใหม่
                    if (model.ShopsItems != null)
                    {
                        foreach (var tag in model.ShopsItems)
                        {
                            if (tag.ID == -1) // ✅ INSERT ใหม่
                            {
                                var newShop = new tm_Shop
                                {
                                    Name = tag.Name?.Trim(),
                                    FlagActive = true,
                                    CreateDate = DateTime.Now,
                                    CreateBy = model.UserID,
                                    UpdateDate = DateTime.Now,
                                    UpdateBy = model.UserID
                                };
                                _context.tm_Shops.Add(newShop);
                                _context.SaveChanges();
                                newShopsIds.Add(newShop.ID);
                            }
                            else // ✅ UPDATE ชื่อร้าน
                            {
                                var existingShop = _context.tm_Shops.FirstOrDefault(s => s.ID == tag.ID && s.FlagActive == true);
                                if (existingShop != null)
                                {
                                    existingShop.Name = tag.Name?.Trim();
                                    existingShop.UpdateDate = DateTime.Now;
                                    existingShop.UpdateBy = model.UserID;

                                    _context.tm_Shops.Update(existingShop);
                                    _context.SaveChanges();
                                    newShopsIds.Add(existingShop.ID);
                                }
                            }
                        }
                    }


                    // ✅ 2. สร้าง ProjectShopEvent
                    if (model.DatesEvent != null && model.DatesEvent.Any())
                    {
                        foreach (var DateInsert in model.DatesEvent)
                        {
                            if (model.ProjectIds != null && model.ProjectIds.Any())
                            {
                                foreach (var projectId in model.ProjectIds)
                                {
                                    if (newShopsIds != null && newShopsIds.Any())
                                    {
                                        foreach (var newShopId in newShopsIds)
                                        {
                                            var shop = model.ShopsItems.FirstOrDefault(x => x.ID == newShopId || x.Name?.Trim() == _context.tm_Shops.FirstOrDefault(s => s.ID == newShopId)?.Name);

                                            var ProjectShopEvent = new TR_ProjectShopEvent
                                            {
                                                ProjectID = projectId,
                                                EventID = model.EventID,
                                                EventDate = Commond.FormatExtension.ToDateFromddmmyyy(DateInsert),
                                                ShopID = newShopId,
                                                UnitQuota = shop?.UnitQuota ?? 0, // Default to 0 if not found
                                                ShopQuota = shop?.ShopQuota ?? 0, // Default to 0 if not found
                                                IsUsed = shop?.IsUsed ?? false, // Default to false if not found
                                                FlagActive = true,
                                                CreateDate = DateTime.Now,
                                                CreateBy = model.UserID,
                                                UpdateDate = DateTime.Now,
                                            };
                                            _context.TR_ProjectShopEvents.Add(ProjectShopEvent);
                                            _context.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // ✅ 3. บันทึกการเปลี่ยนแปลงทั้งหมด
                    transaction.Commit();

                    // ✅ 4. ส่งผลลัพธ์กลับ
                    response.ID = 1;
                    response.Message = "Shops created successfully";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var message = ex.InnerException != null
                        ? $"INNER ERROR: {ex.InnerException.Message}"
                        : $"ERROR: {ex.Message}";

                    response.Message = $"An error occurred: {message}";
                }

            }

            return response;
        }

        public GetDataCreateEvent_Shops GetDataCreateEventsAndShops(GetDataCreateEvent_Shops filter)
        {
            var targetDate = Commond.FormatExtension.ToDateFromddmmyyy(filter.EventDates);

            // 🔹 data1: ดึง Project ทั้งหมด ไม่สน IsUsed
            var data1 = _context.TR_ProjectShopEvents
                .Where(e => e.EventID == filter.EventID
                         && e.EventDate == targetDate
                         && e.FlagActive == true)
                .ToList();

            // 🔹 data2: ดึงเฉพาะร้านที่ IsUsed == true
            var data2 = data1.Where(e => e.IsUsed == true).ToList();

            var response = new GetDataCreateEvent_Shops
            {
                EventID = filter.EventID,
                EventDates = filter.EventDates,
                IsHaveData = data2.Any(),

                Projects = data1
                    .Select(e => e.ProjectID)
                    .Distinct()
                    .Select(projectId => new ListProjects
                    {
                        ProjectID = projectId,
                        ProjectName = _context.tm_Projects.FirstOrDefault(p => p.ProjectID == projectId)?.ProjectName ?? "",
                        IsUsed = true // หากต้องการให้เช็คจาก data2 ก็เปลี่ยน logic ได้
                    }).ToList(),

                Shops = data2
                    .GroupBy(e => e.ShopID)
                    .Select(g => new ListShops
                    {
                        ID = g.Key,
                        Name = _context.tm_Shops.FirstOrDefault(s => s.ID == g.Key)?.Name ?? "",
                        UnitQuota = g.First().UnitQuota,
                        ShopQuota = g.First().ShopQuota,
                        IsUsed = g.First().IsUsed,
                    }).ToList()
            };

            return response;
        }

        public List<GetListShopAndEventCalendar.ListData> GetListShopAndEventSCalendar(GetListShopAndEventCalendar.FilterData filter)
        {
            List<GetListShopAndEventCalendar.ListData> result = new List<GetListShopAndEventCalendar.ListData>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "";

                if (filter.L_ShowBy == 1) // Show by ProjectID
                {
                    sql = @"
                            IF @L_Month = ''
                            BEGIN
                                SELECT 
                                       T1.[ID]
                                      ,T2.[ProjectID]
                                      ,T3.[ProjectName]
                                      ,T3.[ProjectName_Eng]
                                      ,T1.[Name] AS EventName
		                              ,T4.[Name] AS EventType 
                                      ,T4.[ColorCode] AS EventColor
                                      ,T1.[Location]
		                              ,T5.TagNames
                                      ,T1.[StartDate]
                                      ,T1.[EndDate]
                                FROM [tm_Event](NOLOCK) T1
                                     LEFT JOIN [TR_ProjectEvent] (NOLOCK) T2 ON T1.ID = T2.[EventID]
		                             LEFT JOIN [tm_Project] (NOLOCK) T3 ON T2.[ProjectID] = T3.ProjectID
		                             LEFT JOIN [tm_EventType] (NOLOCK) T4 ON T1.[ID] = T4.EventID
		                             LEFT JOIN (
						                            SELECT 
							                            T1.EventID,
							                            STRING_AGG(T2.Name, ',') AS TagNames
						                            FROM [TR_TagEvent] (NOLOCK) T1
						                            LEFT JOIN [tm_Tag] (NOLOCK) T2 ON T1.TagID = T2.ID
						                            GROUP BY T1.EventID
		                                       ) T5 ON T5.EventID = T1.ID
                                WHERE 
                                    T1.FlagActive = 1
                                    AND T2.FlagActive = 1
                                    AND (
                                        @L_ProjectID = '' 
                                        OR (',' + @L_ProjectID + ',' LIKE '%,' + T2.ProjectID + ',%')
                                    )
                                    AND (
                                        YEAR(T1.StartDate) = @L_Year
                                        OR YEAR(T1.EndDate) = @L_Year
                                    )
                                ORDER BY T1.StartDate;
                            END
                            ELSE
                            BEGIN
                                SELECT       
                                       T1.[ID]
                                      ,T2.[ProjectID]
                                      ,T3.[ProjectName]
                                      ,T3.[ProjectName_Eng]
                                      ,T1.[Name] AS EventName
		                              ,T4.[Name] AS EventType 
                                      ,T4.[ColorCode] AS EventColor
                                      ,T1.[Location]
                                      ,T5.TagNames
                                      ,T1.[StartDate]
                                      ,T1.[EndDate]
                                FROM [tm_Event](NOLOCK) T1
                                     LEFT JOIN [TR_ProjectEvent](NOLOCK) T2 ON T1.ID = T2.[EventID]
		                             LEFT JOIN [tm_Project] (NOLOCK) T3 ON T2.[ProjectID] = T3.ProjectID
		                             LEFT JOIN [tm_EventType] (NOLOCK) T4 ON T1.[ID] = T4.EventID
		                             LEFT JOIN (
						                            SELECT 
							                            T1.EventID,
							                            STRING_AGG(T2.Name, ',') AS TagNames
						                            FROM [TR_TagEvent] (NOLOCK) T1
						                            LEFT JOIN [tm_Tag] (NOLOCK) T2 ON T1.TagID = T2.ID
						                            GROUP BY T1.EventID
		                                       ) T5 ON T5.EventID = T1.ID
                                WHERE 
                                    T1.FlagActive = 1
                                    AND T2.FlagActive = 1
                                    AND (
                                        @L_ProjectID = '' 
                                        OR (',' + @L_ProjectID + ',' LIKE '%,' + T2.ProjectID + ',%')
                                    )
                                    AND 
		                            (
                                        (
                                            YEAR(T1.StartDate) = @L_Year
                                            AND MONTH(T1.StartDate) = CAST(@L_Month AS INT)
                                        )
                                        OR (
                                            YEAR(T1.EndDate) = @L_Year
                                            AND MONTH(T1.EndDate) = CAST(@L_Month AS INT)
                                        )
                                        OR (
                                            T1.StartDate <= EOMONTH(CONVERT(DATE, CAST(@L_Year AS NVARCHAR(4)) + '-' + @L_Month + '-01'))
                                            AND T1.EndDate >= CONVERT(DATE, CAST(@L_Year AS NVARCHAR(4)) + '-' + @L_Month + '-01')
                                        )
                                    )
                                ORDER BY T1.StartDate;
                            END
                           "
                    ;
                }
                else if (filter.L_ShowBy == 2) // Show by Event
                {
                    sql = @"
                            IF @L_Month = ''
                            BEGIN
                                SELECT 
                                       T1.[ID]
                                      ,T1.[Name] AS EventName
		                              ,T4.[Name] AS EventType 
                                      ,T4.[ColorCode] AS EventColor
                                      ,T1.[Location]
		                              ,T5.TagNames
                                      ,T1.[StartDate]
                                      ,T1.[EndDate]
                                FROM [tm_Event](NOLOCK) T1
		                             LEFT JOIN [tm_EventType] (NOLOCK) T4 ON T1.[ID] = T4.EventID
		                             LEFT JOIN (
						                            SELECT 
							                            T1.EventID,
							                            STRING_AGG(T2.Name, ',') AS TagNames
						                            FROM [TR_TagEvent] (NOLOCK) T1
						                            LEFT JOIN [tm_Tag] (NOLOCK) T2 ON T1.TagID = T2.ID
						                            GROUP BY T1.EventID
		                                       ) T5 ON T5.EventID = T1.ID
	                                 INNER JOIN (
		                                SELECT 
		                                   T1.EventID
		                                FROM [TR_ProjectEvent] (NOLOCK) T1
		                                WHERE T1.FlagActive = 1 
			                                AND (
				                                @L_ProjectID = '' 
				                                OR (',' + @L_ProjectID + ',' LIKE '%,' + T1.ProjectID + ',%')
			                                )
		                                GROUP BY T1.EventID 
	                                 ) T3 ON T1.[ID] = T3.EventID
                                WHERE 
                                    T1.FlagActive = 1
                                    AND (
                                        YEAR(T1.StartDate) = @L_Year
                                        OR YEAR(T1.EndDate) = @L_Year
                                    )
                                ORDER BY T1.StartDate;
                            END
                            ELSE
                            BEGIN
                                SELECT       
                                       T1.[ID]
                                      ,T1.[Name] AS EventName
		                              ,T4.[Name] AS EventType 
                                      ,T4.[ColorCode] AS EventColor
                                      ,T1.[Location]
                                      ,T5.TagNames
                                      ,T1.[StartDate]
                                      ,T1.[EndDate]
                                FROM [tm_Event](NOLOCK) T1
		                            LEFT JOIN [tm_EventType] (NOLOCK) T4 ON T1.[ID] = T4.EventID
		                            LEFT JOIN (
						                            SELECT 
							                            T1.EventID,
							                            STRING_AGG(T2.Name, ',') AS TagNames
						                            FROM [TR_TagEvent] (NOLOCK) T1
						                            LEFT JOIN [tm_Tag] (NOLOCK) T2 ON T1.TagID = T2.ID
						                            GROUP BY T1.EventID
		                                       ) T5 ON T5.EventID = T1.ID
                                    INNER JOIN (
                                        SELECT 
                                        T1.EventID
                                        FROM [TR_ProjectEvent] (NOLOCK) T1
                                        WHERE T1.FlagActive = 1 
                                            AND (
                                                @L_ProjectID = '' 
                                                OR (',' + @L_ProjectID + ',' LIKE '%,' + T1.ProjectID + ',%')
                                            )
                                        GROUP BY T1.EventID 
                                    ) T3 ON T1.[ID] = T3.EventID
                                WHERE 
                                    T1.FlagActive = 1
                                    AND 
		                            (
                                        (
                                            YEAR(T1.StartDate) = @L_Year
                                            AND MONTH(T1.StartDate) = CAST(@L_Month AS INT)
                                        )
                                        OR (
                                            YEAR(T1.EndDate) = @L_Year
                                            AND MONTH(T1.EndDate) = CAST(@L_Month AS INT)
                                        )
                                        OR (
                                            T1.StartDate <= EOMONTH(CONVERT(DATE, CAST(@L_Year AS NVARCHAR(4)) + '-' + @L_Month + '-01'))
                                            AND T1.EndDate >= CONVERT(DATE, CAST(@L_Year AS NVARCHAR(4)) + '-' + @L_Month + '-01')
                                        )
                                    )
                                ORDER BY T1.StartDate;
                            END
                           "
                    ;
                }

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", filter.L_ProjectID ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Month", filter.L_Month ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Year", filter.L_Year ?? 0));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (filter.L_ShowBy == 1)
                        {
                            while (reader.Read())
                            {
                                result.Add(new GetListShopAndEventCalendar.ListData
                                {
                                    title = "โครงการ: " + Commond.FormatExtension.NullToString(reader["ProjectName"]) + " " + "Event: " + Commond.FormatExtension.NullToString(reader["EventName"]) ,
                                    start = Commond.FormatExtension.NullToString(reader["StartDate"]),
                                    end = Commond.FormatExtension.NullToString(reader["EndDate"]),
                                    color = Commond.FormatExtension.NullToString(reader["EventColor"]),
                                    modaltype = 1, // 1 = Project

                                    Eventname = "โครงการ: " + Commond.FormatExtension.NullToString(reader["ProjectName"]) + " " + "Event: " + Commond.FormatExtension.NullToString(reader["EventName"]),
                                    Eventlocation = Commond.FormatExtension.NullToString(reader["Location"]),
                                    Eventtags = Commond.FormatExtension.NullToString(reader["TagNames"])
                                });
                            }
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                result.Add(new GetListShopAndEventCalendar.ListData
                                {
                                    title = Commond.FormatExtension.NullToString(reader["EventName"]),
                                    start = Commond.FormatExtension.NullToString(reader["StartDate"]),
                                    end = Commond.FormatExtension.NullToString(reader["EndDate"]),
                                    color = Commond.FormatExtension.NullToString(reader["EventColor"]),
                                    modaltype = 2, // 2 = Event

                                    Eventname = Commond.FormatExtension.NullToString(reader["EventName"]),
                                    Eventlocation = Commond.FormatExtension.NullToString(reader["Location"]),
                                    Eventtags = Commond.FormatExtension.NullToString(reader["TagNames"])
                                });
                            }
                        }

                    }
                }
            }


            return result;
        }
    }
}
