namespace Project.CSS.Revise.Web.Models.Pages.CSResponse
{
    public class UpdateInsertCsmapping
    {
        public string ProjectID { get; set; } = "";
        public int CSUserID { get; set; } = 0;
        public List<string> ListUnitCode { get; set; } = new List<string>();
        public int UpdateBy { get; set; } = 0;
        public bool Issuccess { get; set; } = false;
        public string Message { get; set; } = "";

    }
}
