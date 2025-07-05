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
    }
}
