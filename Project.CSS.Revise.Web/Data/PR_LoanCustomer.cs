using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("PR_LoanCustomer")]
public partial class PR_LoanCustomer
{
    [Key]
    public Guid ID { get; set; }

    public Guid? LoanID { get; set; }

    public Guid? CustomerID { get; set; }

    public int? Seq { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? UpdateBy { get; set; }

    [ForeignKey("CustomerID")]
    [InverseProperty("PR_LoanCustomers")]
    public virtual PR_Customer? Customer { get; set; }

    [ForeignKey("LoanID")]
    [InverseProperty("PR_LoanCustomers")]
    public virtual PR_Loan? Loan { get; set; }
}
