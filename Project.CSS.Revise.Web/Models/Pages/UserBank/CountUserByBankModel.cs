namespace Project.CSS.Revise.Web.Models.Pages.UserBank
{
    public class CountUserByBankModel
    {

        public class ListData
        {
            public int? CntUserByBank { get; set; }
            public string? BankCode { get; set; }
            public string? BankName { get; set; }
            public int? BankID { get; set; }
            public decimal InterestRateAVG { get; set; }
        }
    }
}
