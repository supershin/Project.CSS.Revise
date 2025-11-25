namespace Project.CSS.Revise.Web.Models.Pages.QueueBank
{
    public class RegisterModel
    {
        public string ProjectID { get; set; }
        public Guid UnitID { get; set; }
        public int RegisterLogID { get; set; }
        public string ProjectName { get; set; }
        public string InterestRateUrl { get; set; }
        public int QueueTypeID { get; set; }
        public int Counter { get; set; }
        public int BankID { get; set; }
        public int ReasonID { get; set; }
        public string BankCounterStatus { get; set; }

        public bool IsCancelCheckout { get; set; }

        public Guid? LoanID { get; set; }

        private List<RegisterProjectBank_Result> _ProjectBankList;
        public List<RegisterProjectBank_Result> ProjectBankList
        {
            get
            {
                return (_ProjectBankList) ?? new List<RegisterProjectBank_Result>();
            }
            set { _ProjectBankList = value; }
        }

        private List<RegisterBankCounter_Result> _BankCounterList;
        public List<RegisterBankCounter_Result> BankCounterList
        {
            get
            {
                return (_BankCounterList) ?? new List<RegisterBankCounter_Result>();
            }
            set { _BankCounterList = value; }
        }

        public List<GerRegisterCounter_Result> UnitList { get; set; }
        public string ContactDetail { get; set; }
        public bool IsBankCheckout { get; set; }
    }
}
