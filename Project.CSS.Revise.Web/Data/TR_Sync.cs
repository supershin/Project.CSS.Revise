using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Sync")]
public partial class TR_Sync
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Module { get; set; }

    public string? Detail { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SyncDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SyncStatus { get; set; }

    public string? Message { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_Syncs")]
    public virtual tm_Project? Project { get; set; }

    [InverseProperty("Sync")]
    public virtual ICollection<TR_Sync_LoanBank> TR_Sync_LoanBanks { get; set; } = new List<TR_Sync_LoanBank>();

    [InverseProperty("Sync")]
    public virtual ICollection<TR_Sync_QC> TR_Sync_QCs { get; set; } = new List<TR_Sync_QC>();

    [ForeignKey("UnitID")]
    [InverseProperty("TR_Syncs")]
    public virtual tm_Unit? Unit { get; set; }
}
