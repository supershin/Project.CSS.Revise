using System.Drawing;

namespace Project.CSS.Revise.Web.Models.Pages.Shop_Event
{
    public class GetListShopAndEventCalendar
    {
        public class FilterData
        {
            public string? L_ProjectID { get; set; }
            public int? L_Year { get; set; }
            public string? L_Month { get; set; }
            public int? L_ShowBy { get; set; }
        }
        public class ListData
        {
            public string? title { get; set; }
            public string? start { get; set; }
            public string? end { get; set; }
            public string? color { get; set; }
            public int? modaltype { get; set; }

            public int? EventID { get; set; }
            public string? ProjectID { get; set; }
            public string? Eventname { get; set; }
            public string? Eventlocation { get; set; }
            public string? Eventtags { get; set; }
        }
    }
}
