using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC_ContactLog")]
public partial class TR_QC_ContactLog
{
    [Key]
    public int ID { get; set; }

    public Guid? TempID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public Guid? QC_ID { get; set; }

    public int? QCTypeID { get; set; }

    public string? Detail { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_QC_ContactLogs")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("QC_ID")]
    [InverseProperty("TR_QC_ContactLogs")]
    public virtual TR_QC6? QC { get; set; }

    [ForeignKey("QCTypeID")]
    [InverseProperty("TR_QC_ContactLogs")]
    public virtual tm_Ext? QCType { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_QC_ContactLogs")]
    public virtual tm_Unit? Unit { get; set; }
}
