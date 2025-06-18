using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("PR_CustomerCareer")]
public partial class PR_CustomerCareer
{
    [Key]
    public Guid ID { get; set; }

    public Guid? LoanID { get; set; }

    public Guid? CustomerID { get; set; }

    public int? CareerID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CreateBy { get; set; }

    [ForeignKey("CareerID")]
    [InverseProperty("PR_CustomerCareers")]
    public virtual tm_Ext? Career { get; set; }

    [ForeignKey("CustomerID")]
    [InverseProperty("PR_CustomerCareers")]
    public virtual PR_Customer? Customer { get; set; }

    [ForeignKey("LoanID")]
    [InverseProperty("PR_CustomerCareers")]
    public virtual PR_Loan? Loan { get; set; }
}
