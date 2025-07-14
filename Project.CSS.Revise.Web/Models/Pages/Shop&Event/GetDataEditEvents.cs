namespace Project.CSS.Revise.Web.Models.Pages.Shop_Event
{
    public class GetDataEditEvents
    {
        public class GetEditEventInProjectFilterModel
        {
            public int? EventID { get; set; }
            public string? ProjectID { get; set; }
        }
        public class EditEventInProjectModel
        {
            public int? EventID { get; set; }
            public string? ProjectID { get; set; }

            public string? EventName { get; set; }
            public string? EventType { get; set; }
            public string? EventColor { get; set; }
            public string? EventLocation { get; set; }
            public string? ProjectName { get; set; }

            public List<DateEventModel>? DateEvents { get; set; }
            public List<ListShopsModel>? ListShops { get; set; }
            public List<ListEditShopsModel>? ListEditShops { get; set; }
        }
        public class DateEventModel
        {
            public string? Text { get; set; }
            public string? Value { get; set; }
        }
        public class ListShopsModel
        {
            public int? ID { get; set; }
            public string? Name { get; set; }
        }
        public class ListEditShopsModel
        {
            public int? ID { get; set; }
            public string? Name { get; set; }
            public int? UnitQuota { get; set; }
            public int? ShopQuota { get; set; }
            public bool? IsUsed { get; set; }
        }

    }
}
