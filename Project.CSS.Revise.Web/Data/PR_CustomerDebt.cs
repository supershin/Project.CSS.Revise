using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("PR_CustomerDebt")]
public partial class PR_CustomerDebt
{
    [Key]
    public Guid ID { get; set; }

    public Guid? LoanID { get; set; }

    public Guid? CustomerID { get; set; }

    public int? DebtID { get; set; }

    public string? OtherText { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? OtherValue { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CreateBy { get; set; }

    [ForeignKey("CustomerID")]
    [InverseProperty("PR_CustomerDebts")]
    public virtual PR_Customer? Customer { get; set; }

    [ForeignKey("DebtID")]
    [InverseProperty("PR_CustomerDebts")]
    public virtual tm_Ext? Debt { get; set; }

    [ForeignKey("LoanID")]
    [InverseProperty("PR_CustomerDebts")]
    public virtual PR_Loan? Loan { get; set; }
}
