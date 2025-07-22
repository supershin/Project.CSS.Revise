namespace Project.CSS.Revise.Web.Models.Pages.Shop_Event
{
    public class CreateEvent_Shops
    {
        public int EventID { get; set; }
        public int UserID { get; set; }
        public List<ShopsModel>? ShopsItems { get; set; }
        public List<string>? ProjectIds { get; set; }
        public List<string>? DatesEvent { get; set; }
    }
    public class ShopsModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int? UnitQuota { get; set; }
        public int? ShopQuota { get; set; }
        public bool? IsUsed { get; set; }
    }
    public class CreateEventsShopsResponse
    {
        public int? ID { get; set; }
        public string? Message { get; set; }
    }
}
