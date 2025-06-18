using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("PR_AttachFile")]
public partial class PR_AttachFile
{
    [Key]
    public Guid ID { get; set; }

    public int? UserTypeID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? IDCardNo { get; set; }

    public int? AttachTypeID { get; set; }

    public int? Seq { get; set; }

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

    [ForeignKey("AttachTypeID")]
    [InverseProperty("PR_AttachFileAttachTypes")]
    public virtual tm_Ext? AttachType { get; set; }

    [InverseProperty("AttachFile")]
    public virtual ICollection<PR_LoanCustomerAttach> PR_LoanCustomerAttaches { get; set; } = new List<PR_LoanCustomerAttach>();

    [ForeignKey("UserTypeID")]
    [InverseProperty("PR_AttachFileUserTypes")]
    public virtual tm_Ext? UserType { get; set; }
}
