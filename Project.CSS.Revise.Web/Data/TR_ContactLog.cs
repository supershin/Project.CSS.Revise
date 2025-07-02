using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_ContactLog")]
public partial class TR_ContactLog
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public Guid? QC_ID { get; set; }

    public int? QCTypeID { get; set; }

    public int? TeamID { get; set; }

    public int? BankID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ContactDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ContactTime { get; set; }

    public string? ContactName { get; set; }

    public int? CustomerTypeID { get; set; }

    public int? ContactReasonID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PaymentDueDate { get; set; }

    public string? Detail { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("BankID")]
    [InverseProperty("TR_ContactLogs")]
    public virtual tm_Bank? Bank { get; set; }

    [ForeignKey("ContactReasonID")]
    [InverseProperty("TR_ContactLogContactReasons")]
    public virtual tm_Ext? ContactReason { get; set; }

    [ForeignKey("CustomerTypeID")]
    [InverseProperty("TR_ContactLogCustomerTypes")]
    public virtual tm_Ext? CustomerType { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_ContactLogs")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("QC_ID")]
    [InverseProperty("TR_ContactLogs")]
    public virtual TR_QC6? QC { get; set; }

    [ForeignKey("QCTypeID")]
    [InverseProperty("TR_ContactLogQCTypes")]
    public virtual tm_Ext? QCType { get; set; }

    [InverseProperty("QCContactLog")]
    public virtual ICollection<TR_QC_ContactLogResource> TR_QC_ContactLogResources { get; set; } = new List<TR_QC_ContactLogResource>();

    [ForeignKey("TeamID")]
    [InverseProperty("TR_ContactLogTeams")]
    public virtual tm_Ext? Team { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_ContactLogs")]
    public virtual tm_Unit? Unit { get; set; }
}
