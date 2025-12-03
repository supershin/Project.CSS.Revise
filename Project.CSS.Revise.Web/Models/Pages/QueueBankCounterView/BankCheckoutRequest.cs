namespace Project.CSS.Revise.Web.Models.Pages.QueueBankCounterView
{
    public class BankCheckoutRequest
    {
        public int? RegisterLogID { get; set; }
        public int BankID { get; set; }
        public string? ContactDetail { get; set; }  // optional
    }
}
