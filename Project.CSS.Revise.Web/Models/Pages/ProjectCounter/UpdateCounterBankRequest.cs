namespace Project.CSS.Revise.Web.Models.Pages.ProjectCounter
{
    public class UpdateCounterBankRequest
    {
        public int ID { get; set; }                 // TR_ProjectCounter_Mapping.ID
        public int CounterQty { get; set; }         // EndCounter (StartCounter is always 1)
        public int UserID { get; set; }             // for audit fields

        public List<BankEditItem> Banks { get; set; } = new();

        public sealed class BankEditItem
        {
            public int BankId { get; set; }         // tm_Bank.ID
            public string? Code { get; set; }       // BankCode (optional, for reference)
            public bool Checked { get; set; }       // enabled or not
            public int Qty { get; set; }            // staff count (0..N)
        }

        public class Response
        {
            public int? ID { get; set; }
            public string? Message { get; set; }
        }
    }
}
