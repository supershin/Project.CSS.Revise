namespace Project.CSS.Revise.Web.Models.Pages.QueueBank
{
    public class GetQueueBankModel
    {
        // ===== DataTables params =====
        public int draw { get; set; } = 0;
        public int start { get; set; } = 0;
        public int length { get; set; } = 10;
        public string SearchTerm { get; set; } = string.Empty;

        // ===== QueueBank filters =====
        public string L_Act { get; set; } = string.Empty;
        public string L_ProjectID { get; set; } = string.Empty;
        public string L_RegisterDateStart { get; set; } = string.Empty;
        public string L_RegisterDateEnd { get; set; } = string.Empty;
        public string L_UnitID { get; set; } = string.Empty;
        public string L_CSResponse { get; set; } = string.Empty;
        public string L_UnitCS { get; set; } = string.Empty;
        public string L_ExpectTransfer { get; set; } = string.Empty;
    }
}
