using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.Shop_Event;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IShopAndEventRepo
    {
        public CreateEventsTagsResponse CreateEventsAndTags(CreateEvents_Tags model);
        public CreateEventsShopsResponse CreateEventsAndShops(CreateEvent_Shops model);
        public GetDataCreateEvent_Shops GetDataCreateEventsAndShops(GetDataCreateEvent_Shops filter);
        public List<GetListShopAndEventCalendar.ListData> GetListShopAndEventSCalendar(GetListShopAndEventCalendar.FilterData filter);
        public GetDataEditEvents.EditEventInProjectModel GetDataEditEventInProject(GetDataEditEvents.GetEditEventInProjectFilterModel filter);
        public List<GetDataEditEvents.ListEditShopsModel> GetDataTrEventsAndShopsinProject(int enventID, string projectID, string eventDate);
        public bool UpdateDateTimeEvent(int EnventID, string ProjectID, string DateStart, string DateEnd, int UserID);
        public bool InActiveEvent(int EnventID, string ProjectID, int UserID);
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

                    if (model.ProjectIds != null && model.ProjectIds.Any())
                    {
                        foreach (var projectId in model.ProjectIds)
                        {

                            var newEvent = new tm_Event
                            {
                                ProjectID = projectId, // 👈 ใส่ ProjectID ตรงนี้เลย
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
                            _context.SaveChanges(); 

                            eventIdsCreated.Add(newEvent.ID);
                        }
                    }

                    if (model.EventType.HasValue)
                    {
                        foreach (var eventId in eventIdsCreated)
                        {
                            var eventTypeRelation = new TR_Event_EventType
                            {
                                EventID = eventId,
                                EventTypeID = model.EventType.Value
                            };

                            _context.TR_Event_EventTypes.Add(eventTypeRelation);
                        }
                        _context.SaveChanges();
                    }

                    foreach (var eventId in eventIdsCreated)
                    {
                        foreach (var tagId in newTagIds)
                        {
                            var tagEvent = new TR_TagEvent
                            {
                                EventID = eventId,
                                TagID = tagId
                            };
                            _context.TR_TagEvents.Add(tagEvent);
                        }
                    }
                    _context.SaveChanges(); 

                    transaction.Commit();

                    response.ID = 1;
                    response.EventIDs = eventIdsCreated; // ✅ ส่งคืนรายการ Event ID ที่สร้าง
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
                    if (model.ProjectIds != null && model.ProjectIds.Any())
                    {
                        foreach (var projectId in model.ProjectIds)
                        {
                            if (model.DatesEvent != null && model.DatesEvent.Any())
                            {
                                foreach (var DateInsert in model.DatesEvent)
                                {
                                    if (newShopsIds != null && newShopsIds.Any())
                                    {
                                        foreach (var newShopId in newShopsIds)
                                        {
                                            var shop = model.ShopsItems.FirstOrDefault(x => x.ID == newShopId || x.Name?.Trim() == _context.tm_Shops.FirstOrDefault(s => s.ID == newShopId)?.Name);

                                            var targetDate = Commond.FormatExtension.ToDateFromddmmyyy(DateInsert);

                                            // ✅ ตรวจสอบก่อนว่า record มีอยู่หรือไม่
                                            var existing = _context.TR_ProjectShopEvents.FirstOrDefault(e =>
                                                e.ProjectID == projectId &&
                                                e.EventID == model.EventID &&
                                                e.EventDate == targetDate &&
                                                e.ShopID == newShopId &&
                                                e.FlagActive == true);

                                            if (existing != null)
                                            {
                                                // 🔄 UPDATE
                                                existing.UnitQuota = shop?.UnitQuota ?? 0;
                                                existing.ShopQuota = shop?.ShopQuota ?? 0;
                                                existing.IsUsed = shop?.IsUsed ?? false;
                                                existing.UpdateDate = DateTime.Now;

                                                _context.TR_ProjectShopEvents.Update(existing);
                                            }
                                            else
                                            {
                                                // ➕ INSERT
                                                var ProjectShopEvent = new TR_ProjectShopEvent
                                                {
                                                    ProjectID = projectId,
                                                    EventID = model.EventID,
                                                    EventDate = targetDate,
                                                    ShopID = newShopId,
                                                    UnitQuota = shop?.UnitQuota ?? 0,
                                                    ShopQuota = shop?.ShopQuota ?? 0,
                                                    IsUsed = shop?.IsUsed ?? false,
                                                    FlagActive = true,
                                                    CreateDate = DateTime.Now,
                                                    CreateBy = model.UserID,
                                                    UpdateDate = DateTime.Now,
                                                };
                                                _context.TR_ProjectShopEvents.Add(ProjectShopEvent);
                                            }

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
            var data = _context.TR_ProjectShopEvents
                .Where(e => e.EventID == filter.EventID
                         && e.EventDate == targetDate
                         && e.ProjectID == filter.ProjectID
                         && e.FlagActive == true
                         //&& e.IsUsed == true
                         )
                .ToList();

            var response = new GetDataCreateEvent_Shops
            {
                EventID = filter.EventID,
                EventDates = filter.EventDates,
                IsHaveData = data.Any(),

                Shops = data
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

        //public List<GetListShopAndEventCalendar.ListData> GetListShopAndEventSCalendar(GetListShopAndEventCalendar.FilterData filter)
        //{
        //    List<GetListShopAndEventCalendar.ListData> result = new List<GetListShopAndEventCalendar.ListData>();
        //    string connectionString = _context.Database.GetDbConnection().ConnectionString;

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();

        //        string sql = "";

        //        if (filter.L_ShowBy == 1) // Show by ProjectID
        //        {
        //            sql = @"
        //                    IF @L_Month = ''
        //                    BEGIN
        //                        SELECT 
        //                               T1.[ID] AS EventID
        //                              ,T2.[ProjectID]
        //                              ,T3.[ProjectName]
        //                              ,T3.[ProjectName_Eng]
        //                              ,T1.[Name] AS EventName
        //                        ,T4.[Name] AS EventType 
        //                              ,T4.[ColorCode] AS EventColor
        //                              ,T1.[Location]
        //                        ,T5.TagNames
        //                              ,T1.[StartDate]
        //                              ,T1.[EndDate]
        //                        FROM [tm_Event](NOLOCK) T1
        //                             LEFT JOIN [TR_ProjectEvent] (NOLOCK) T2 ON T1.ID = T2.[EventID]
        //                       LEFT JOIN [tm_Project] (NOLOCK) T3 ON T2.[ProjectID] = T3.ProjectID
        //                       LEFT JOIN [tm_EventType] (NOLOCK) T4 ON T1.[ID] = T4.EventID
        //                       LEFT JOIN (
        //                          SELECT 
        //                           T1.EventID,
        //                           STRING_AGG(T2.Name, ',') AS TagNames
        //                          FROM [TR_TagEvent] (NOLOCK) T1
        //                          LEFT JOIN [tm_Tag] (NOLOCK) T2 ON T1.TagID = T2.ID
        //                          GROUP BY T1.EventID
        //                                 ) T5 ON T5.EventID = T1.ID
        //                        WHERE 
        //                            T1.FlagActive = 1
        //                            AND T2.FlagActive = 1
        //                            AND (
        //                                @L_ProjectID = '' 
        //                                OR (',' + @L_ProjectID + ',' LIKE '%,' + T2.ProjectID + ',%')
        //                            )
        //                            AND (
        //                                YEAR(T1.StartDate) = @L_Year
        //                                OR YEAR(T1.EndDate) = @L_Year
        //                            )
        //                        ORDER BY T1.StartDate;
        //                    END
        //                    ELSE
        //                    BEGIN
        //                        SELECT       
        //                               T1.[ID] AS EventID
        //                              ,T2.[ProjectID]
        //                              ,T3.[ProjectName]
        //                              ,T3.[ProjectName_Eng]
        //                              ,T1.[Name] AS EventName
        //                        ,T4.[Name] AS EventType 
        //                              ,T4.[ColorCode] AS EventColor
        //                              ,T1.[Location]
        //                              ,T5.TagNames
        //                              ,T1.[StartDate]
        //                              ,T1.[EndDate]
        //                        FROM [tm_Event](NOLOCK) T1
        //                             LEFT JOIN [TR_ProjectEvent](NOLOCK) T2 ON T1.ID = T2.[EventID]
        //                       LEFT JOIN [tm_Project] (NOLOCK) T3 ON T2.[ProjectID] = T3.ProjectID
        //                       LEFT JOIN [tm_EventType] (NOLOCK) T4 ON T1.[ID] = T4.EventID
        //                       LEFT JOIN (
        //                          SELECT 
        //                           T1.EventID,
        //                           STRING_AGG(T2.Name, ',') AS TagNames
        //                          FROM [TR_TagEvent] (NOLOCK) T1
        //                          LEFT JOIN [tm_Tag] (NOLOCK) T2 ON T1.TagID = T2.ID
        //                          GROUP BY T1.EventID
        //                                 ) T5 ON T5.EventID = T1.ID
        //                        WHERE 
        //                            T1.FlagActive = 1
        //                            AND T2.FlagActive = 1
        //                            AND (
        //                                @L_ProjectID = '' 
        //                                OR (',' + @L_ProjectID + ',' LIKE '%,' + T2.ProjectID + ',%')
        //                            )
        //                            AND 
        //                      (
        //                                (
        //                                    YEAR(T1.StartDate) = @L_Year
        //                                    AND MONTH(T1.StartDate) = CAST(@L_Month AS INT)
        //                                )
        //                                OR (
        //                                    YEAR(T1.EndDate) = @L_Year
        //                                    AND MONTH(T1.EndDate) = CAST(@L_Month AS INT)
        //                                )
        //                                OR (
        //                                    T1.StartDate <= EOMONTH(CONVERT(DATE, CAST(@L_Year AS NVARCHAR(4)) + '-' + @L_Month + '-01'))
        //                                    AND T1.EndDate >= CONVERT(DATE, CAST(@L_Year AS NVARCHAR(4)) + '-' + @L_Month + '-01')
        //                                )
        //                            )
        //                        ORDER BY T1.StartDate;
        //                    END
        //                   "
        //            ;
        //        }
        //        else if (filter.L_ShowBy == 2) // Show by Event
        //        {
        //            sql = @"
        //                    IF @L_Month = ''
        //                    BEGIN
        //                        SELECT 
        //                               T1.[ID] AS EventID
        //                              ,T1.[Name] AS EventName
        //                        ,T4.[Name] AS EventType 
        //                              ,T4.[ColorCode] AS EventColor
        //                              ,T1.[Location]
        //                        ,T5.TagNames
        //                              ,T1.[StartDate]
        //                              ,T1.[EndDate]
        //                        FROM [tm_Event](NOLOCK) T1
        //                       LEFT JOIN [tm_EventType] (NOLOCK) T4 ON T1.[ID] = T4.EventID
        //                       LEFT JOIN (
        //                          SELECT 
        //                           T1.EventID,
        //                           STRING_AGG(T2.Name, ',') AS TagNames
        //                          FROM [TR_TagEvent] (NOLOCK) T1
        //                          LEFT JOIN [tm_Tag] (NOLOCK) T2 ON T1.TagID = T2.ID
        //                          GROUP BY T1.EventID
        //                                 ) T5 ON T5.EventID = T1.ID
        //                          INNER JOIN (
        //                          SELECT 
        //                             T1.EventID
        //                          FROM [TR_ProjectEvent] (NOLOCK) T1
        //                          WHERE T1.FlagActive = 1 
        //                           AND (
        //                            @L_ProjectID = '' 
        //                            OR (',' + @L_ProjectID + ',' LIKE '%,' + T1.ProjectID + ',%')
        //                           )
        //                          GROUP BY T1.EventID 
        //                          ) T3 ON T1.[ID] = T3.EventID
        //                        WHERE 
        //                            T1.FlagActive = 1
        //                            AND (
        //                                YEAR(T1.StartDate) = @L_Year
        //                                OR YEAR(T1.EndDate) = @L_Year
        //                            )
        //                        ORDER BY T1.StartDate;
        //                    END
        //                    ELSE
        //                    BEGIN
        //                        SELECT       
        //                               T1.[ID] AS EventID
        //                              ,T1.[Name] AS EventName
        //                        ,T4.[Name] AS EventType 
        //                              ,T4.[ColorCode] AS EventColor
        //                              ,T1.[Location]
        //                              ,T5.TagNames
        //                              ,T1.[StartDate]
        //                              ,T1.[EndDate]
        //                        FROM [tm_Event](NOLOCK) T1
        //                      LEFT JOIN [tm_EventType] (NOLOCK) T4 ON T1.[ID] = T4.EventID
        //                      LEFT JOIN (
        //                          SELECT 
        //                           T1.EventID,
        //                           STRING_AGG(T2.Name, ',') AS TagNames
        //                          FROM [TR_TagEvent] (NOLOCK) T1
        //                          LEFT JOIN [tm_Tag] (NOLOCK) T2 ON T1.TagID = T2.ID
        //                          GROUP BY T1.EventID
        //                                 ) T5 ON T5.EventID = T1.ID
        //                            INNER JOIN (
        //                                SELECT 
        //                                T1.EventID
        //                                FROM [TR_ProjectEvent] (NOLOCK) T1
        //                                WHERE T1.FlagActive = 1 
        //                                    AND (
        //                                        @L_ProjectID = '' 
        //                                        OR (',' + @L_ProjectID + ',' LIKE '%,' + T1.ProjectID + ',%')
        //                                    )
        //                                GROUP BY T1.EventID 
        //                            ) T3 ON T1.[ID] = T3.EventID
        //                        WHERE 
        //                            T1.FlagActive = 1
        //                            AND 
        //                      (
        //                                (
        //                                    YEAR(T1.StartDate) = @L_Year
        //                                    AND MONTH(T1.StartDate) = CAST(@L_Month AS INT)
        //                                )
        //                                OR (
        //                                    YEAR(T1.EndDate) = @L_Year
        //                                    AND MONTH(T1.EndDate) = CAST(@L_Month AS INT)
        //                                )
        //                                OR (
        //                                    T1.StartDate <= EOMONTH(CONVERT(DATE, CAST(@L_Year AS NVARCHAR(4)) + '-' + @L_Month + '-01'))
        //                                    AND T1.EndDate >= CONVERT(DATE, CAST(@L_Year AS NVARCHAR(4)) + '-' + @L_Month + '-01')
        //                                )
        //                            )
        //                        ORDER BY T1.StartDate;
        //                    END
        //                   "
        //            ;
        //        }

        //        using (SqlCommand cmd = new SqlCommand(sql, conn))
        //        {
        //            cmd.Parameters.Add(new SqlParameter("@L_ProjectID", filter.L_ProjectID ?? ""));
        //            cmd.Parameters.Add(new SqlParameter("@L_Month", filter.L_Month ?? ""));
        //            cmd.Parameters.Add(new SqlParameter("@L_Year", filter.L_Year ?? 0));

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (filter.L_ShowBy == 1)
        //                {
        //                    while (reader.Read())
        //                    {
        //                        result.Add(new GetListShopAndEventCalendar.ListData
        //                        {
        //                            title = "โครงการ: " + Commond.FormatExtension.NullToString(reader["ProjectName"]) + " " + "Event: " + Commond.FormatExtension.NullToString(reader["EventName"]) ,
        //                            start = Commond.FormatExtension.NullToString(reader["StartDate"]),
        //                            end = Commond.FormatExtension.NullToString(reader["EndDate"]),
        //                            color = Commond.FormatExtension.NullToString(reader["EventColor"]),
        //                            modaltype = 1, // 1 = Project

        //                            EventID = Commond.FormatExtension.Nulltoint(reader["EventID"]),
        //                            ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]),
        //                            Eventname = "โครงการ: " + Commond.FormatExtension.NullToString(reader["ProjectName"]) + " " + "Event: " + Commond.FormatExtension.NullToString(reader["EventName"]),
        //                            Eventlocation = Commond.FormatExtension.NullToString(reader["Location"]),
        //                            Eventtags = Commond.FormatExtension.NullToString(reader["TagNames"])
        //                        });
        //                    }
        //                }
        //                else
        //                {
        //                    while (reader.Read())
        //                    {
        //                        result.Add(new GetListShopAndEventCalendar.ListData
        //                        {
        //                            title = Commond.FormatExtension.NullToString(reader["EventName"]),
        //                            start = Commond.FormatExtension.NullToString(reader["StartDate"]),
        //                            end = Commond.FormatExtension.NullToString(reader["EndDate"]),
        //                            color = Commond.FormatExtension.NullToString(reader["EventColor"]),
        //                            modaltype = 2, // 2 = Event

        //                            EventID = Commond.FormatExtension.Nulltoint(reader["EventID"]),
        //                            Eventname = Commond.FormatExtension.NullToString(reader["EventName"]),
        //                            Eventlocation = Commond.FormatExtension.NullToString(reader["Location"]),
        //                            Eventtags = Commond.FormatExtension.NullToString(reader["TagNames"])
        //                        });
        //                    }
        //                }

        //            }
        //        }
        //    }


        //    return result;
        //}


        public List<GetListShopAndEventCalendar.ListData> GetListShopAndEventSCalendar(GetListShopAndEventCalendar.FilterData filter)
        {
            List<GetListShopAndEventCalendar.ListData> result = new List<GetListShopAndEventCalendar.ListData>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "";
                sql = @"
                            IF @L_Month = ''
                            BEGIN
	                            SELECT
			                        T1.[ID] AS EventID
		                            ,T1.ProjectID
                                    ,T2.[ProjectName]
                                    ,T2.[ProjectName_Eng]
                                    ,T1.[Name] AS EventName
                                    ,T4.[Name] AS EventType 
                                    ,T4.[ColorCode] AS EventColor
                                    ,T1.[Location]
                                    ,T5.TagNames
                                    ,T1.[StartDate]
                                    ,T1.[EndDate]
                                FROM [tm_Event](NOLOCK) T1
                                    LEFT JOIN [tm_Project] (NOLOCK) T2 ON T1.[ProjectID] = T2.ProjectID
			                        LEFT JOIN [TR_Event_EventType] (NOLOCK) T3 ON T1.ID = T3.EventID
                                    LEFT JOIN [tm_EventType] (NOLOCK) T4 ON T3.[EventTypeID] = T4.ID
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
                                        OR (',' + @L_ProjectID + ',' LIKE '%,' + T1.ProjectID + ',%')
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
                                       T1.[ID] AS EventID
                                      ,T1.[ProjectID]
                                      ,T2.[ProjectName]
                                      ,T2.[ProjectName_Eng]
                                      ,T1.[Name] AS EventName
                                      ,T4.[Name] AS EventType 
                                      ,T4.[ColorCode] AS EventColor
                                      ,T1.[Location]
                                      ,T5.TagNames
                                      ,T1.[StartDate]
                                      ,T1.[EndDate]
                                FROM [tm_Event](NOLOCK) T1
                                     LEFT JOIN [tm_Project] (NOLOCK) T2 ON T1.[ProjectID] = T2.ProjectID
                                     LEFT JOIN [TR_Event_EventType] (NOLOCK) T3 ON T1.ID = T3.EventID
                                     LEFT JOIN [tm_EventType] (NOLOCK) T4 ON T3.[EventTypeID] = T3.EventID
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
                                    AND (
                                        @L_ProjectID = '' 
                                        OR (',' + @L_ProjectID + ',' LIKE '%,' + T1.ProjectID + ',%')
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

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", filter.L_ProjectID ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Month", filter.L_Month ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Year", filter.L_Year ?? 0));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new GetListShopAndEventCalendar.ListData
                            {
                                title = "โครงการ: " + Commond.FormatExtension.NullToString(reader["ProjectName"]) + " " + "Event: " + Commond.FormatExtension.NullToString(reader["EventName"]),
                                start = Commond.FormatExtension.NullToString(reader["StartDate"]),
                                end = Commond.FormatExtension.NullToString(reader["EndDate"]),
                                color = Commond.FormatExtension.NullToString(reader["EventColor"]),
                                modaltype = 1, // 1 = Project

                                EventID = Commond.FormatExtension.Nulltoint(reader["EventID"]),
                                ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]),
                                Eventname = "โครงการ: " + Commond.FormatExtension.NullToString(reader["ProjectName"]) + " " + "Event: " + Commond.FormatExtension.NullToString(reader["EventName"]),
                                Eventlocation = Commond.FormatExtension.NullToString(reader["Location"]),
                                Eventtags = Commond.FormatExtension.NullToString(reader["TagNames"])
                            });
                        }
                    }
                }
            }


            return result;
        }

        //public GetDataEditEvents.EditEventInProjectModel GetDataEditEventInProject(GetDataEditEvents.GetEditEventInProjectFilterModel filter)
        //{
        //    // 1. ดึงข้อมูลหลักของ Event และ Project
        //    var result = (from e in _context.tm_Events
        //                  join pe in _context.TR_ProjectEvents on e.ID equals pe.EventID into peJoin
        //                  from pe in peJoin.DefaultIfEmpty()
        //                  join p in _context.tm_Projects on pe.ProjectID equals p.ProjectID into pJoin
        //                  from p in pJoin.DefaultIfEmpty()
        //                  join et in _context.tm_EventTypes on e.ID equals et.ID into etJoin
        //                  from et in etJoin.DefaultIfEmpty()
        //                  where e.ID == filter.EventID && pe.ProjectID == filter.ProjectID
        //                  select new GetDataEditEvents.EditEventInProjectModel
        //                  {
        //                      EventID = e.ID,
        //                      ProjectID = pe.ProjectID,
        //                      ProjectName = p.ProjectName,
        //                      EventName = e.Name,
        //                      EventLocation = e.Location,
        //                      EventType = et.Name,
        //                      EventColor = et.ColorCode
        //                  }).FirstOrDefault();

        //    // 2. ตรวจสอบและเพิ่ม DateEvents
        //    var eventItem = _context.tm_Events.FirstOrDefault(e => e.ID == filter.EventID);

        //    if (result != null && eventItem != null && eventItem.StartDate != null && eventItem.EndDate != null)
        //    {
        //        result.DateEvents = Enumerable.Range(0, (eventItem.EndDate.Value.Date - eventItem.StartDate.Value.Date).Days + 1)
        //            .Select(offset => eventItem.StartDate.Value.Date.AddDays(offset))
        //            .Select(date => new GetDataEditEvents.DateEventModel
        //            {
        //                Text = Commond.FormatExtension.FormatDateToThaiShortString(date),
        //                Value = date.ToString("yyyy-MM-dd")
        //            }).ToList();
        //    }

        //    // 3. ดึงข้อมูลร้านค้าที่เกี่ยวข้องกับ Shop (เฉพาะ FlagActive == true)
        //    result.ListShops = _context.tm_Shops.Where(s => s.FlagActive == true)
        //        .Select(s => new GetDataEditEvents.ListShopsModel
        //        {
        //            ID = s.ID,
        //            Name = s.Name
        //        }).ToList();

        //    return result;
        //}

        public GetDataEditEvents.EditEventInProjectModel GetDataEditEventInProject(GetDataEditEvents.GetEditEventInProjectFilterModel filter)
        {
            var result = (from e in _context.tm_Events
                          join p in _context.tm_Projects on e.ProjectID equals p.ProjectID into projectJoin
                          from p in projectJoin.DefaultIfEmpty()

                          join etype in _context.TR_Event_EventTypes on e.ID equals etype.EventID into etypeJoin
                          from etype in etypeJoin.DefaultIfEmpty()

                          join et in _context.tm_EventTypes on etype.EventTypeID equals et.ID into eventTypeJoin
                          from et in eventTypeJoin.DefaultIfEmpty()

                          where e.ID == filter.EventID && e.ProjectID == filter.ProjectID
                          select new GetDataEditEvents.EditEventInProjectModel
                          {
                              EventID = e.ID,
                              ProjectID = e.ProjectID,
                              ProjectName = p.ProjectName,
                              EventName = e.Name,
                              EventType = et.Name,
                              EventColor = et.ColorCode,
                              EventLocation = e.Location,
                              StartDate = e.StartDate.HasValue ? e.StartDate.Value.ToString("yyyy-MM-dd HH:mm") : "",
                              EndDate = e.EndDate.HasValue ? e.EndDate.Value.ToString("yyyy-MM-dd HH:mm") : ""

                          }).FirstOrDefault();

            var eventItem = _context.tm_Events.FirstOrDefault(e => e.ID == filter.EventID);

            if (result != null && eventItem != null && eventItem.StartDate != null && eventItem.EndDate != null)
            {
                result.DateEvents = Enumerable.Range(0, (eventItem.EndDate.Value.Date - eventItem.StartDate.Value.Date).Days + 1)
                    .Select(offset => eventItem.StartDate.Value.Date.AddDays(offset))
                    .Select(date => new GetDataEditEvents.DateEventModel
                    {
                        Text = Commond.FormatExtension.FormatDateToThaiShortString(date),
                        Value = date.ToString("yyyy-MM-dd")
                    }).ToList();
            }

            return result;
        }

        public List<GetDataEditEvents.ListEditShopsModel> GetDataTrEventsAndShopsinProject(int enventID, string projectID, string eventDate)
        {
            var targetDate = Commond.FormatExtension.ToDateFromddmmyyy(eventDate);

            var query = (from s in _context.tm_Shops
                         where s.FlagActive == true
                         join e in _context.TR_ProjectShopEvents
                             on new { ShopID = (int?)s.ID, EventID = (int?)enventID, ProjectID = projectID, EventDate = targetDate } equals new { ShopID = e.ShopID, e.EventID, e.ProjectID, e.EventDate }
                             into seJoin
                         from e in seJoin.Where(x => x.FlagActive == true).DefaultIfEmpty()
                         select new GetDataEditEvents.ListEditShopsModel
                         {
                             ID = s.ID,
                             Name = s.Name,
                             UnitQuota = e != null ? e.UnitQuota : 0,
                             ShopQuota = e != null ? e.ShopQuota : 0,
                             IsUsed = e != null && e.IsUsed == true
                         }).ToList();

            return query;
        }

        public bool UpdateDateTimeEvent(int EnventID, string ProjectID, string DateStart, string DateEnd, int UserID)
        {
            var response = false;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    var existing = _context.tm_Events.FirstOrDefault(e =>e.ProjectID == ProjectID && e.ID == EnventID && e.FlagActive == true);

                    if (existing != null)
                    {
                        // 🔄 UPDATE
                        existing.StartDate = Commond.FormatExtension.ToDateFromddmmyyy(DateStart);
                        existing.EndDate = Commond.FormatExtension.ToDateFromddmmyyy(DateEnd);
                        existing.UpdateDate = DateTime.Now;
                        existing.UpdateBy = UserID;

                        _context.tm_Events.Update(existing);
                    }

                    _context.SaveChanges();

 
                    transaction.Commit();

                    response = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var message = ex.InnerException != null
                        ? $"INNER ERROR: {ex.InnerException.Message}"
                        : $"ERROR: {ex.Message}";
                    response = false;
                }

            }

            return response;
        }

        public bool InActiveEvent(int EnventID, string ProjectID, int UserID)
        {
            var response = false;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    var existing = _context.tm_Events.FirstOrDefault(e => e.ProjectID == ProjectID && e.ID == EnventID && e.FlagActive == true);

                    if (existing != null)
                    {
                        // 🔄 UPDATE
                        existing.FlagActive = false;
                        existing.UpdateDate = DateTime.Now;
                        existing.UpdateBy = UserID;

                        _context.tm_Events.Update(existing);
                    }

                    _context.SaveChanges();


                    transaction.Commit();

                    response = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var message = ex.InnerException != null
                        ? $"INNER ERROR: {ex.InnerException.Message}"
                        : $"ERROR: {ex.Message}";
                    response = false;
                }

            }

            return response;
        }
    }
}
