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

                    // ✅ 2. Loop เพิ่ม Event ตาม ProjectID
                    if (model.ProjectIds != null && model.ProjectIds.Any())
                    {
                        foreach (var projectId in model.ProjectIds)
                        {
                            var newEvent = new tm_Event
                            {
                                ProjectID = projectId,
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
                            _context.SaveChanges(); // ต้อง Save เพื่อให้ newEvent.ID มีค่า

                            eventIdsCreated.Add(newEvent.ID);

                            // ✅ 3. เพิ่มความสัมพันธ์ใน TR_TagEvent
                            foreach (var tagId in newTagIds)
                            {
                                var tagEvent = new TR_TagEvent
                                {
                                    EventID = newEvent.ID,
                                    TagID = tagId
                                };
                                _context.TR_TagEvents.Add(tagEvent);
                            }

                            _context.SaveChanges(); // Save TR_TagEvent หลัง loop แต่ละ event
                        }
                    }

                    transaction.Commit();

                    response.ID = eventIdsCreated.FirstOrDefault();
                    response.Message = "สร้าง Event + Tag และเชื่อมโยง Tag สำเร็จแล้ว";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    response.Message = $"เกิดข้อผิดพลาด: {ex.Message}";
                }
            }

            return response;
        }


    }
}
