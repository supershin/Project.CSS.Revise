using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.Shop_Event;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IShopAndEventRepo
    {

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
                                _context.SaveChanges(); // ต้อง Save เพื่อให้ได้ ID
                                newTagIds.Add(newTag.ID);
                            }
                        }
                    }

                    // 👇 สมมุติคุณมีตาราง tm_Event
                    var newEvent = new tm_Event
                    {
                        Name = model.EventName?.Trim(),
                        Location = model.EventLocation?.Trim(),
                        StartDate = DateTime.Parse(model.StartDateTime!),
                        EndDate = DateTime.Parse(model.EndDateTime!),
                        FlagActive = model.IsActive,
                        //CreateDate = DateTime.Now,
                        //CreateBy = "System" // หรือดึงจาก session
                    };
                    _context.tm_Events.Add(newEvent);
                    _context.SaveChanges();

                    // เพิ่ม logic บันทึก Project หรือความสัมพันธ์ Tag → Event ได้ที่นี่

                    transaction.Commit();

                    response.ID = newEvent.ID;
                    response.Message = "สร้าง Event และ Tag สำเร็จแล้ว";
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
