using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.Shop_Event;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IShopAndEventRepo
    {
        public CreateEventsTagsResponse CreateEventsAndTags(CreateEvents_Tags model);
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


    }
}
