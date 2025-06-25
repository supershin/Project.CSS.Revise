using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_TerminateTransferAppoint")]
public partial class TR_TerminateTransferAppoint
{
    [Key]
    public Guid ID { get; set; }

    public Guid? UnitID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TerminateDate { get; set; }

    public int? TerminateStatusID { get; set; }

    public string? Detail { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpDateBy { get; set; }

    [InverseProperty("TerminateTransferAppoint")]
    public virtual ICollection<TR_TerminateTransferAppoint_Document> TR_TerminateTransferAppoint_Documents { get; set; } = new List<TR_TerminateTransferAppoint_Document>();

    [ForeignKey("TerminateStatusID")]
    [InverseProperty("TR_TerminateTransferAppoints")]
    public virtual tm_Ext? TerminateStatus { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_TerminateTransferAppoints")]
    public virtual tm_Unit? Unit { get; set; }
}
