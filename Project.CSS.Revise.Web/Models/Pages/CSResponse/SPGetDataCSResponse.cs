namespace Project.CSS.Revise.Web.Models.Pages.CSResponse
{
    public class SPGetDataCSResponse
    {
        public class FilterData
        {
            public string Act { get; set; } = "";
            public string ProjectID { get; set; } = "";
            public string BUID { get; set; } = "";
            public string CsName { get; set; } = "";
            public int UserID { get; set; } = 0;
        }
        public class ListData
        {
            public List<ListCSSummary> CSSummary { get; set; } = new();
            public List<ListCountUnitStatus> CountUnitStatus { get; set; } = new();
        }
        public class ListCSSummary
        {
            public int index { get; set; } = 0;
            public int ID { get; set; } = 0;
            public string FullnameTH { get; set; } = "";
            public string FullnameEN { get; set; } = "";
            public string Email { get; set; } = "";
            public string Mobile { get; set; } = "";
            public int Cnt_Project { get; set; } = 0;
            public int Cnt_UnitCode { get; set; } = 0;
        }
        public class ListCountUnitStatus
        {
            public int index { get; set; } = 0;
            public string ProjectID { get; set; } = "";
            public string ProjectName { get; set; } = "";
            public int Cnt_UserUnits { get; set; } = 0;
            public int Cnt_Status1 { get; set; } = 0;
            public int Cnt_Status2 { get; set; } = 0;
            public int Cnt_Status3 { get; set; } = 0;
            public int Cnt_Status4 { get; set; } = 0;
        }
    }
}
