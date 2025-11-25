using Project.CSS.Revise.Web.Data;

namespace Project.CSS.Revise.Web.Models.Pages.QueueBank
{
    public class RegisterLog : TR_RegisterLog
    {
        public int RowNum { get; set; }
        public string UnitCode { get; set; }
        public string BankIDs { get; set; }
        public bool? FlagRegister { get; set; }
        public bool? FlagWait { get; set; }
        public bool? FlagInprocess { get; set; }
        public bool? FlagFastFix { get; set; }
        public bool? FlagFastFixFinish { get; set; }
        public bool? FlagFinish { get; set; }

        public string CustomerName { get; set; }
        public string ReasonName { get; set; }
        public string ResponsibleName { get; set; }

        public string UpdateByName { get; set; }
        public string CreateByName { get; set; }
        public string RedirectHousingLoan { get; set; }
        public string ContractNumber { get; set; }

        public LoanModel Loan { get; set; } = new LoanModel();

        public bool Isfound { get; set; }

        public List<LoanBankModel> LoanBankList { get; set; } = new List<LoanBankModel>();
    }
}
