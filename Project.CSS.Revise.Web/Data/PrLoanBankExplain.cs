using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrLoanBankExplain
{
    public Guid LoanBankId { get; set; }

    public decimal? ExSalary { get; set; }

    public decimal? ExCommission { get; set; }

    public decimal? ExOvertime { get; set; }

    public decimal? ExIncentive { get; set; }

    public decimal? ExBonus { get; set; }

    public string? ExOtherIncome { get; set; }

    public decimal? ExOtherIncomeValue { get; set; }

    public decimal? ExHousingLoan { get; set; }

    public decimal? ExFinanceLoan { get; set; }

    public decimal? ExPersonalLoan { get; set; }

    public decimal? ExCreditCard { get; set; }

    public decimal? ExCooperativeLoan { get; set; }

    public string? ExOtherLoan { get; set; }

    public decimal? ExOtherLoanValue { get; set; }

    public virtual PrLoanBank LoanBank { get; set; } = null!;
}
