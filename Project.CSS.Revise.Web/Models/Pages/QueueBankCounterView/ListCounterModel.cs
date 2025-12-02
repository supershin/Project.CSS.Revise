namespace Project.CSS.Revise.Web.Models.Pages.QueueBankCounterView
{
    public class ListCounterModel
    {
        public class Filter
        {
            public string? ProjectID { get; set; } = string.Empty;
        }

        public class ListCounterItem
        {
            public string ProjectCounterMappingID { get; set; } = string.Empty;
            public string ProjectID { get; set; } = string.Empty;
            public string QueueTypeID { get; set; } = string.Empty;
            public string Counter { get; set; } = string.Empty;

            // RegisterLog fields
            public string RegisterLogID { get; set; } = string.Empty;
            public string UnitID { get; set; } = string.Empty;
            public string UnitCode { get; set; } = string.Empty;
            public string ResponsibleID { get; set; } = string.Empty;
            public string FastFixDate { get; set; } = string.Empty;
            public string FinishDate { get; set; } = string.Empty;
            public string CareerTypeID { get; set; } = string.Empty;
            public string TransferTypeID { get; set; } = string.Empty;
            public string ReasonID { get; set; } = string.Empty;
            public string FixedDuration { get; set; } = string.Empty;

            // Bank Counter fields
            public string BankID { get; set; } = string.Empty;
            public string BankCounterStatus { get; set; } = string.Empty;
            public string CheckInDate { get; set; } = string.Empty;
            public string InProcessDate { get; set; } = string.Empty;
            public string CheckOutDate { get; set; } = string.Empty;

            // Bank master data
            public string BankCode { get; set; } = string.Empty;
            public string BankName { get; set; } = string.Empty;
        }



    }
}
