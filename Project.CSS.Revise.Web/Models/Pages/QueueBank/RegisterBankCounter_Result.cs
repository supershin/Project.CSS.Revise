namespace Project.CSS.Revise.Web.Models.Pages.QueueBank
{
    public class RegisterBankCounter_Result
    {
        public Nullable<int> QueueTypeID { get; set; }
        public string ProjectID { get; set; }
        public string UnitCode { get; set; }
        public Nullable<int> Counter { get; set; }
        public Nullable<int> BankID { get; set; }
        public string BankCounterStatus { get; set; }
        public int RegisterLogID { get; set; }
        public string BankCode { get; set; }
        public Nullable<System.DateTime> CheckInDate { get; set; }
        public Nullable<System.DateTime> InProcessDate { get; set; }
        public Nullable<System.DateTime> FinishDate { get; set; }
    }
}
