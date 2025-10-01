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
            public int Total { get; set; } = 0;
            public int ID_62 { get; set; } = 0;
            public int ID_63 { get; set; } = 0;
            public int ID_64 { get; set; } = 0;
            public int ID_65 { get; set; } = 0;
            public int ID_67 { get; set; } = 0;
            public int ID_68 { get; set; } = 0;
            public int ID_69 { get; set; } = 0;
            public int ID_70 { get; set; } = 0;

            public int ID_71 { get; set; } = 0;
            public int ID_373 { get; set; } = 0;
            public int ID_408 { get; set; } = 0;

            public int ID_435 { get; set; } = 0;
            public int ID_436 { get; set; } = 0;
            public int ID_438 { get; set; } = 0;
        }
    }
}
