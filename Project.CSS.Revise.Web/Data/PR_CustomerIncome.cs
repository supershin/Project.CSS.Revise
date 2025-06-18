using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("PR_CustomerIncome")]
public partial class PR_CustomerIncome
{
    [Key]
    public Guid ID { get; set; }

    public Guid? LoanID { get; set; }

    public Guid? CustomerID { get; set; }

    public int? IncomeID { get; set; }

    public string? OtherText { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? OtherValue { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CreateBy { get; set; }

    [ForeignKey("CustomerID")]
    [InverseProperty("PR_CustomerIncomes")]
    public virtual PR_Customer? Customer { get; set; }

    [ForeignKey("IncomeID")]
    [InverseProperty("PR_CustomerIncomes")]
    public virtual tm_Ext? Income { get; set; }

    [ForeignKey("LoanID")]
    [InverseProperty("PR_CustomerIncomes")]
    public virtual PR_Loan? Loan { get; set; }
}
