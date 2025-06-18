using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("PR_LoanBank_Explain")]
public partial class PR_LoanBank_Explain
{
    [Key]
    public Guid LoanBankID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExSalary { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExCommission { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExOvertime { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExIncentive { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExBonus { get; set; }

    [StringLength(2000)]
    public string? ExOtherIncome { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExOtherIncomeValue { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExHousingLoan { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExFinanceLoan { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExPersonalLoan { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExCreditCard { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExCooperativeLoan { get; set; }

    [StringLength(2000)]
    public string? ExOtherLoan { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExOtherLoanValue { get; set; }

    [ForeignKey("LoanBankID")]
    [InverseProperty("PR_LoanBank_Explain")]
    public virtual PR_LoanBank LoanBank { get; set; } = null!;
}
