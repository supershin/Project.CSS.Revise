namespace Project.CSS.Revise.Web.Models.Pages.Shop_Event
{
    public class GetDataCreateEvent_Shops
    {
        public int EventID { get; set; } // use where 
        public string EventDates { get; set; } // use where 
        public string ProjectID { get; set; } // use where 
        public bool IsHaveData { get; set; } // if have data in EventDates then true else false
        public List<ListProjects>? Projects { get; set; }
        public List<ListShops>? Shops { get; set; }
    }
    public class ListProjects
    {
        public string? ProjectID { get; set; }
        public string? ProjectName { get; set; }
        public bool? IsUsed { get; set; }
    }
    public class ListShops
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public int? UnitQuota { get; set; }
        public int? ShopQuota { get; set; }
        public bool? IsUsed { get; set; }
    }
}
