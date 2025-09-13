﻿using Microsoft.Data.SqlClient;
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

        //public CreateEventsShopsResponse CreateEventsAndShops(CreateEvent_Shops model)
        //{
        //    var response = new CreateEventsShopsResponse();

        //    using (var transaction = _context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var newShopsIds = new List<int>();

        //            // ✅ 1. ตรวจสอบและเพิ่ม Shops ใหม่
        //            if (model.ShopsItems != null)
        //            {
        //                foreach (var tag in model.ShopsItems)
        //                {
        //                    if (tag.ID == -1) // ✅ INSERT ใหม่
        //                    {
        //                        var newShop = new tm_Shop
        //                        {
        //                            Name = tag.Name?.Trim(),
        //                            FlagActive = true,
        //                            CreateDate = DateTime.Now,
        //                            CreateBy = model.UserID,
        //                            UpdateDate = DateTime.Now,
        //                            UpdateBy = model.UserID
        //                        };
        //                        _context.tm_Shops.Add(newShop);
        //                        _context.SaveChanges();
        //                        newShopsIds.Add(newShop.ID);
        //                    }
        //                    else // ✅ UPDATE ชื่อร้าน
        //                    {
        //                        var existingShop = _context.tm_Shops.FirstOrDefault(s => s.ID == tag.ID && s.FlagActive == true);
        //                        if (existingShop != null)
        //                        {
        //                            existingShop.Name = tag.Name?.Trim();
        //                            existingShop.UpdateDate = DateTime.Now;
        //                            existingShop.UpdateBy = model.UserID;

        //                            _context.tm_Shops.Update(existingShop);
        //                            _context.SaveChanges();
        //                            newShopsIds.Add(existingShop.ID);
        //                        }
        //                    }
        //                }
        //            }

        //            // ✅ 2. สร้าง ProjectShopEvent
        //            if (model.ProjectIds != null && model.ProjectIds.Any())
        //            {
        //                foreach (var projectId in model.ProjectIds)
        //                {
        //                    if (model.DatesEvent != null && model.DatesEvent.Any())
        //                    {
        //                        foreach (var DateInsert in model.DatesEvent)
        //                        {
        //                            if (newShopsIds != null && newShopsIds.Any())
        //                            {
        //                                foreach (var newShopId in newShopsIds)
        //                                {
        //                                    var shop = model.ShopsItems.FirstOrDefault(x => x.ID == newShopId || x.Name?.Trim() == _context.tm_Shops.FirstOrDefault(s => s.ID == newShopId)?.Name);

        //                                    var targetDate = Commond.FormatExtension.ToDateFromddmmyyy(DateInsert);

        //                                    // ✅ ตรวจสอบก่อนว่า record มีอยู่หรือไม่
        //                                    var existing = _context.TR_ProjectShopEvents.FirstOrDefault(e =>
        //                                        e.ProjectID == projectId &&
        //                                        e.EventID == model.EventID &&
        //                                        e.EventDate == targetDate &&
        //                                        e.ShopID == newShopId &&
        //                                        e.FlagActive == true);

        //                                    if (existing != null)
        //                                    {
        //                                        // 🔄 UPDATE
        //                                        existing.UnitQuota = shop?.UnitQuota ?? 0;
        //                                        existing.ShopQuota = shop?.ShopQuota ?? 0;
        //                                        existing.IsUsed = shop?.IsUsed ?? false;
        //                                        existing.UpdateDate = DateTime.Now;

        //                                        _context.TR_ProjectShopEvents.Update(existing);
        //                                    }
        //                                    else
        //                                    {
        //                                        // ➕ INSERT
        //                                        var ProjectShopEvent = new TR_ProjectShopEvent
        //                                        {
        //                                            ProjectID = projectId,
        //                                            EventID = model.EventID,
        //                                            EventDate = targetDate,
        //                                            ShopID = newShopId,
        //                                            UnitQuota = shop?.UnitQuota ?? 0,
        //                                            ShopQuota = shop?.ShopQuota ?? 0,
        //                                            IsUsed = shop?.IsUsed ?? false,
        //                                            FlagActive = true,
        //                                            CreateDate = DateTime.Now,
        //                                            CreateBy = model.UserID,
        //                                            UpdateDate = DateTime.Now,
        //                                        };
        //                                        _context.TR_ProjectShopEvents.Add(ProjectShopEvent);
        //                                    }

        //                                    _context.SaveChanges();
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }


        //            // ✅ 3. บันทึกการเปลี่ยนแปลงทั้งหมด
        //            transaction.Commit();

        //            // ✅ 4. ส่งผลลัพธ์กลับ
        //            response.ID = 1;
        //            response.Message = "Shops created successfully";
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            var message = ex.InnerException != null
        //                ? $"INNER ERROR: {ex.InnerException.Message}"
        //                : $"ERROR: {ex.Message}";

        //            response.Message = $"An error occurred: {message}";
        //        }

        //    }

        //    return response;
        //}
        public CreateEventsShopsResponse CreateEventsAndShops(CreateEvent_Shops model)
        {
            var response = new CreateEventsShopsResponse();

            // ---- 0) เตรียม helper ----
            static string Normalize(string? s) => (s ?? "").Trim().ToUpperInvariant();

            try
            {
                var newShopIds = new List<int>();
                var dupErrors = new List<string>();

                // รวบรวมรายการจาก payload (เฉพาะรายการที่มีชื่อ)
                var items = (model.ShopsItems ?? new List<ShopsModel>())
                    .Where(x => !string.IsNullOrWhiteSpace(x.Name))
                    .Select(x => new
                    {
                        x.ID,
                        RawName = x.Name!,
                        NormName = Normalize(x.Name),
                    })
                    .ToList();

                // ---- 1) เช็คซ้ำใน payload เอง ----
                var dupInPayload = items
                    .GroupBy(x => x.NormName)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.First().RawName)  // โชว์ชื่อดิบอย่างน้อย 1 รายการ
                    .Distinct()
                    .ToList();

                if (dupInPayload.Any())
                    dupErrors.Add($"ชื่อร้านซ้ำกันในรายการที่ส่งมา: {string.Join(", ", dupInPayload)}");

                // ---- 2) โหลดชื่อร้านที่มีอยู่แล้วใน DB (Active) ----
                var existing = _context.tm_Shops
                    .Where(s => s.FlagActive == true)
                    .Select(s => new { s.ID, s.Name })
                    .AsNoTracking()
                    .ToList();

                var existingByNorm = existing
                    .GroupBy(e => Normalize(e.Name))
                    .ToDictionary(g => g.Key, g => g.Select(v => v.ID).ToList());

                // ---- 3) เช็คชนกับ DB ----
                foreach (var it in items)
                {
                    if (!existingByNorm.TryGetValue(it.NormName, out var ids)) continue;

                    // INSERT (ID == -1): ชื่อชนเมื่อมีชื่อเดียวกันอยู่แล้ว
                    if (it.ID == -1)
                    {
                        dupErrors.Add($"พบชื่อร้านซ้ำกับข้อมูลเดิม: {it.RawName}");
                    }
                    else
                    {
                        // UPDATE: ชื่อชนถ้ามีเจ้าของชื่อคนอื่น (ID อื่น)
                        if (ids.Any(dbId => dbId != it.ID))
                            dupErrors.Add($"บันทึกชื่อ '{it.RawName}' ไม่ได้ เพราะมีร้านอื่นใช้ชื่อนี้อยู่แล้ว");
                    }
                }

                // ---- 4) ถ้ามี error เรื่องซ้ำ -> ยกเลิกทันที ----
                if (dupErrors.Any())
                {
                    response.ID = 0;
                    response.Message = string.Join(" | ", dupErrors.Distinct());
                    return response;
                }

                // ---- 5) ผ่านการตรวจแล้ว ค่อยเริ่มทรานแซคชันเพื่อเขียนข้อมูล ----
                using var tran = _context.Database.BeginTransaction();

                // 5.1 INSERT/UPDATE shops
                if (model.ShopsItems != null)
                {
                    foreach (var tag in model.ShopsItems)
                    {
                        var normalized = Normalize(tag.Name);
                        if (string.IsNullOrWhiteSpace(normalized)) continue;

                        if (tag.ID == -1) // INSERT
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
                            _context.SaveChanges(); // ต้องการ ID เพื่อใช้ต่อ
                            newShopIds.Add(newShop.ID);
                        }
                        else // UPDATE
                        {
                            var existingShop = _context.tm_Shops
                                .FirstOrDefault(s => s.ID == tag.ID && s.FlagActive == true);
                            if (existingShop != null)
                            {
                                existingShop.Name = tag.Name?.Trim();
                                existingShop.UpdateDate = DateTime.Now;
                                existingShop.UpdateBy = model.UserID;

                                _context.tm_Shops.Update(existingShop);
                                _context.SaveChanges(); // ต้องการยืนยันว่าอัปเดตผ่าน
                                newShopIds.Add(existingShop.ID);
                            }
                        }
                    }
                }

                // 5.2 สร้าง/อัปเดต TR_ProjectShopEvent
                if (model.ProjectIds != null && model.ProjectIds.Any()
                    && model.DatesEvent != null && model.DatesEvent.Any()
                    && newShopIds.Any())
                {
                    foreach (var projectId in model.ProjectIds)
                    {
                        foreach (var dateText in model.DatesEvent)
                        {
                            var targetDate = Commond.FormatExtension.ToDateFromddmmyyy(dateText);

                            foreach (var shopId in newShopIds)
                            {
                                var shopPayload = model.ShopsItems!.FirstOrDefault(x => x.ID == shopId)
                                                  ?? model.ShopsItems!.FirstOrDefault(x =>
                                                      Normalize(x.Name) == Normalize(
                                                          _context.tm_Shops.Where(s => s.ID == shopId)
                                                                            .Select(s => s.Name)
                                                                            .FirstOrDefault()
                                                      )
                                                  );

                                var existingEvent = _context.TR_ProjectShopEvents.FirstOrDefault(e =>
                                    e.ProjectID == projectId &&
                                    e.EventID == model.EventID &&
                                    e.EventDate == targetDate &&
                                    e.ShopID == shopId &&
                                    e.FlagActive == true);

                                if (existingEvent != null)
                                {
                                    // UPDATE
                                    existingEvent.UnitQuota = shopPayload?.UnitQuota ?? 0;
                                    existingEvent.ShopQuota = shopPayload?.ShopQuota ?? 0;
                                    existingEvent.IsUsed = shopPayload?.IsUsed ?? false;
                                    existingEvent.UpdateDate = DateTime.Now;

                                    _context.TR_ProjectShopEvents.Update(existingEvent);
                                }
                                else
                                {
                                    // INSERT
                                    var pse = new TR_ProjectShopEvent
                                    {
                                        ProjectID = projectId,
                                        EventID = model.EventID,
                                        EventDate = targetDate,
                                        ShopID = shopId,
                                        UnitQuota = shopPayload?.UnitQuota ?? 0,
                                        ShopQuota = shopPayload?.ShopQuota ?? 0,
                                        IsUsed = shopPayload?.IsUsed ?? false,
                                        FlagActive = true,
                                        CreateDate = DateTime.Now,
                                        CreateBy = model.UserID,
                                        UpdateDate = DateTime.Now,
                                    };
                                    _context.TR_ProjectShopEvents.Add(pse);
                                }
                            }
                        }
                    }
                    _context.SaveChanges();
                }

                // 5.3 commit
                tran.Commit();

                response.ID = 1;
                response.Message = "Shops created/updated successfully";
                return response;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null
                    ? $"INNER ERROR: {ex.InnerException.Message}"
                    : $"ERROR: {ex.Message}";
                return new CreateEventsShopsResponse
                {
                    ID = 0,
                    Message = $"An error occurred: {msg}"
                };
            }
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
                                    LEFT JOIN TR_ProjectStatus PST (NOLOCK) ON T1.ProjectID = PST.ProjectID
                                    LEFT JOIN (
                                                    SELECT 
	                                                    T1.EventID,
	                                                    STRING_AGG(T2.Name, ',') AS TagNames
                                                    FROM [TR_TagEvent] (NOLOCK) T1
                                                    LEFT JOIN [tm_Tag] (NOLOCK) T2 ON T1.TagID = T2.ID
                                                    GROUP BY T1.EventID
                                            ) T5 ON T5.EventID = T1.ID
                                    LEFT JOIN [tm_BUProject_Mapping] (NOLOCK) T6 ON T6.[ProjectID] = T2.ProjectID
                                WHERE 
                                    T1.FlagActive = 1
                                    AND T2.FlagActive = 1
                                    AND (
                                        @L_Bu = '' 
                                        OR (',' + @L_Bu + ',' LIKE '%,' + CONVERT(varchar, T6.BUID) + ',%')
                                    )
                                    AND (
                                        @L_ProjectID = '' 
                                        OR (',' + @L_ProjectID + ',' LIKE '%,' + T1.ProjectID + ',%')
                                    )
                                   AND (
                                        @L_ProjectStatus = ''
                                        OR (',' + @L_ProjectStatus + ',' LIKE '%,' + CONVERT(VARCHAR, PST.StatusID) + ',%')
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
                                     LEFT JOIN TR_ProjectStatus PST (NOLOCK) ON T1.ProjectID = PST.ProjectID
                                     LEFT JOIN (
                                                    SELECT 
                                                        T1.EventID,
                                                        STRING_AGG(T2.Name, ',') AS TagNames
                                                    FROM [TR_TagEvent] (NOLOCK) T1
                                                    LEFT JOIN [tm_Tag] (NOLOCK) T2 ON T1.TagID = T2.ID
                                                    GROUP BY T1.EventID
                                               ) T5 ON T5.EventID = T1.ID
                                    LEFT JOIN [tm_BUProject_Mapping] (NOLOCK) T6 ON T6.[ProjectID] = T2.ProjectID
                                WHERE 
                                    T1.FlagActive = 1
                                    AND (
                                        @L_ProjectID = '' 
                                        OR (',' + @L_ProjectID + ',' LIKE '%,' + T1.ProjectID + ',%')
                                    )
                                    AND (
                                        @L_Bu = '' 
                                        OR (',' + @L_Bu + ',' LIKE '%,' + CONVERT(varchar, T6.BUID) + ',%')
                                    )
                                   AND (
                                        @L_ProjectStatus = ''
                                        OR (',' + @L_ProjectStatus + ',' LIKE '%,' + CONVERT(VARCHAR, PST.StatusID) + ',%')
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
                    cmd.Parameters.Add(new SqlParameter("@L_Bu", filter.L_Bu ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", filter.L_ProjectID ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectStatus", filter.L_ProjectStatus ?? ""));
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
