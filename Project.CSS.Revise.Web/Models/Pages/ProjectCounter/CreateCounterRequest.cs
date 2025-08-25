namespace Project.CSS.Revise.Web.Models.Pages.ProjectCounter
{
    public class CreateCounterRequest
    {
        public List<int> CounterTypeIds { get; set; } = new();   // e.g. [48] or [48,49]

        // Total quota (sum of all checked bank qty must be <= this)
        public int CounterQty { get; set; }

        // Project mappings (multi-select)
        public List<string> ProjectIds { get; set; } = new();    // e.g. ["102C028","102C029"]

        // Only needed if 48 (Bank) is selected
        public List<BankStaffItem> Banks { get; set; } = new();  // ordered by LineOrder from your fetch
        public int UserID { get; set; }

        public sealed class BankStaffItem
        {
            public int BankId { get; set; }
            public string Code { get; set; } = "";   // BankCode, e.g. "BBL"
            public bool Checked { get; set; }        // whether user enabled this bank
            public int Qty { get; set; }             // staff count for that bank (0..N)
        }

        public class Response
        {
            public int? ID { get; set; }
            public string? Message { get; set; }
        }

    }
}
