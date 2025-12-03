namespace Project.CSS.Revise.Web.Models.Pages.QueueBankCounterView
{
    public class ListCounterDetailsModel
    {
        public class Filter
        {
            public string? ProjectID { get; set; } = string.Empty;
            public int? Counter { get; set; } = 0;
        }

        public class ListCounterItem
        {
            public string ID { get; set; } = string.Empty;
            public string ProjectID { get; set; } = string.Empty;
            public string UnitID { get; set; } = string.Empty;
            public string UnitCode { get; set; } = string.Empty;
            public string BankID { get; set; } = string.Empty;
            public string BankCode { get; set; } = string.Empty;
            public string BankName { get; set; } = string.Empty;
        }

    }
}
