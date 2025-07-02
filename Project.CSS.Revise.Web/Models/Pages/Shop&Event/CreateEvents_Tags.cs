namespace Project.CSS.Revise.Web.Models.Pages.Shop_Event
{
    public class CreateEvents_Tags
    {
        public string? EventName { get; set; }
        public string? EventLocation { get; set; }
        public List<TagModel>? TagItems { get; set; } // ใช้รับ tag แบบ value/label
        public List<string>? ProjectIds { get; set; } // multiple select
        public string? StartDateTime { get; set; }
        public string? EndDateTime { get; set; }
        public bool IsActive { get; set; }
        public int? UserID { get; set; }
    }
    public class TagModel
    {
        public string? Value { get; set; }
        public string? Label { get; set; }
    }
    public class CreateEventsTagsResponse
    {
        public int? ID { get; set; }
        public string? Message { get; set; }
    }

}
