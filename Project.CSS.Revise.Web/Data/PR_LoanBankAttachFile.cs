using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("PR_LoanBankAttachFile")]
public partial class PR_LoanBankAttachFile
{
    [Key]
    public Guid ID { get; set; }

    public Guid? LoanID { get; set; }

    public Guid? LoanBankID { get; set; }

    [StringLength(500)]
    public string? FileName { get; set; }

    [StringLength(500)]
    public string? FilePath { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? MimeType { get; set; }

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

    [ForeignKey("LoanID")]
    [InverseProperty("PR_LoanBankAttachFiles")]
    public virtual PR_Loan? Loan { get; set; }

    [ForeignKey("LoanBankID")]
    [InverseProperty("PR_LoanBankAttachFiles")]
    public virtual PR_LoanBank? LoanBank { get; set; }
}
