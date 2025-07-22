namespace Project.CSS.Revise.Web.Models.Pages.Shop_Event
{
    public class CreateEvents_Tags
    {
        public string? EventName { get; set; }
        public int? EventType { get; set; }
        //public string? EventColor { get; set; }
        public string? EventLocation { get; set; }
        public List<TagModel>? TagItems { get; set; } // ใช้รับ tag แบบ value/label
        public List<string>? ProjectIds { get; set; } // multiple select
        public string? StartDateTime { get; set; }
        public string? EndDateTime { get; set; }
        public bool IsActive { get; set; }
        public int UserID { get; set; }
    }
    public class TagModel
    {
        public string? Value { get; set; }
        public string? Label { get; set; }
    }
    public class CreateEventsTagsResponse
    {
        public int? ID { get; set; }
        public List<int>? EventIDs { get; set; }  // 🔁 เปลี่ยนจาก int? ID เป็น List<int>
        public string? Message { get; set; }
    }

}
