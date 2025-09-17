namespace Project.CSS.Revise.Web.Models.Pages.CSResponse
{
    public class GetlistUnitCSResponseModel
    {
        public class FilterData
        {
            public string L_UserID { get; set; } = "";
            public string L_ProjectID { get; set; } = "";
            public string L_UnitStatus{ get; set; } = "";
            public string L_Build { get; set; } = "";
            public string L_Floor { get; set; } = "";
            public string L_Room { get; set; } = "";
            public string L_TypeUserShow { get; set; } = "-1";
        }
        public class ListData
        {
            public string ProjectID { get; set; } = "";
            public string ProjectName { get; set; } = "";
            public string ProjectName_Eng { get; set; } = "";
            public string ProjectType { get; set; } = "";

            public string UnitCode { get; set; } = "";
            public string AddrNo { get; set; } = "";
            public string Build { get; set; } = "";
            public string Floor { get; set; } = "";
            public string Room { get; set; } = "";
            public string UnitType { get; set; } = "";
            public string Area { get; set; } = "";
            public string UnitStatus { get; set; } = "";

            public string CSUserID { get; set; } = "";
            public string CSFullNameThai { get; set; } = "";
            public string CSFullNameEng { get; set; } = "";
            public string UpdateBy { get; set; } = "";
            public string UpdateDate { get; set; } = "";

            public int IsCheck { get; set; } = 0;
        }
    }
}
