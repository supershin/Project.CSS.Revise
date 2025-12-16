namespace Project.CSS.Revise.Web.Models.Pages.QueueBank
{
    public class ListSummeryRegister
    {
        public sealed class ListSummeryRegisterType
        {
            public string Topic { get; set; } = "";
            public string Unit { get; set; } = "";
            public string Value { get; set; } = "";
            public string Percent { get; set; } = "";
        }
        public sealed class ListSummeryRegisterLoanType
        {
            public string Topic { get; set; } = "";
            public string Unit { get; set; } = "";
            public string Value { get; set; } = "";
            public string Percent { get; set; } = "";
        }
        public sealed class ListSummeryRegisterCareerType
        {
            public string Topic { get; set; } = "";
            public string Unit { get; set; } = "";
            public string Value { get; set; } = "";
            public string Percent { get; set; } = "";
        }
        public sealed class ListSummeryRegisterBank
        {
            public string BankCode { get; set; } = "";
            public string BankName { get; set; } = "";
            public string InterestRateAVG { get; set; } = "";
            public string Unit { get; set; } = "";
            public string Value { get; set; } = "";
            public string Percent { get; set; } = "";
        }
        public sealed class ListSummeryRegisterBankNonSubmissionReason
        {
            public int ID { get; set; } = 0;
            public string Name { get; set; } = "";
            public int Count { get; set; } = 0;
            public string Percent { get; set; } = "";
        }
    }
}
