using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Sync_QC")]
public partial class TR_Sync_QC
{
    [Key]
    public Guid ID { get; set; }

    public Guid? SyncID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UnitCode { get; set; }

    public int? QCTypeID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QCAppointDate { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? QCAppointTimeFrom { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? QCAppointTimeTo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? QCResponseUserID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QCResponseDate { get; set; }

    public string? QCRemark { get; set; }

    public int? SyncTypeID { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? SyncTypeDetail { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_Sync_QCs")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("QCTypeID")]
    [InverseProperty("TR_Sync_QCs")]
    public virtual tm_Ext? QCType { get; set; }

    [ForeignKey("SyncID")]
    [InverseProperty("TR_Sync_QCs")]
    public virtual TR_Sync? Sync { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_Sync_QCs")]
    public virtual tm_Unit? Unit { get; set; }
}
