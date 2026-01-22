namespace Project.CSS.Revise.Web.Models.Pages.QueueBank
{
    public class ListCreateRegisterTableModel
    {
        public string RegisterLogID { get; set; } = "";
        public string ProjectID { get; set; } = "";
        public string UnitID { get; set; } = "";

        public string UnitCode { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string ResponsibleName { get; set; } = "";
        public string CSResponseName { get; set; } = "";

        public string RegisterDate { get; set; } = "";
        public string InprocessDate { get; set; } = "";
        public string Done { get; set; } = "";
        public string Status { get; set; } = "";


        public string ReasonName { get; set; } = "";
        public string ReasonRemarkName { get; set; } = "";
        public string Counter { get; set; } = "";

        public string CreateBy { get; set; } = "";
        public string UpdateDate { get; set; } = "";
        public string UpdateBy { get; set; } = "";


        // 👇 ใช้สำหรับ DataTables server-side
        public int TotalRecords { get; set; } = 0;
        public int FilteredRecords { get; set; } = 0;
    }

}
