using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_RegisterLog")]
public partial class TR_RegisterLog
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public int? QCTypeID { get; set; }

    public int? QueueTypeID { get; set; }

    public int? ResponsibleID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RegisterDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? WaitDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InprocessDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FastFixDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FastFixFinishDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FinishDate { get; set; }

    public int? CareerTypeID { get; set; }

    public int? TransferTypeID { get; set; }

    public int? ReasonID { get; set; }

    public int? ReasonRemarkID { get; set; }

    public int? FixedDuration { get; set; }

    public int? Counter { get; set; }

    public Guid? LoanID { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_RegisterLogs")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("QCTypeID")]
    [InverseProperty("TR_RegisterLogQCTypes")]
    public virtual tm_Ext? QCType { get; set; }

    [ForeignKey("ReasonRemarkID")]
    [InverseProperty("TR_RegisterLogReasonRemarks")]
    public virtual tm_Ext? ReasonRemark { get; set; }

    [ForeignKey("ResponsibleID")]
    [InverseProperty("TR_RegisterLogs")]
    public virtual tm_User? Responsible { get; set; }

    [InverseProperty("RegisterLog")]
    public virtual ICollection<TR_RegisterBank> TR_RegisterBanks { get; set; } = new List<TR_RegisterBank>();

    [InverseProperty("RegisterLog")]
    public virtual ICollection<TR_Register_BankCounter> TR_Register_BankCounters { get; set; } = new List<TR_Register_BankCounter>();

    [InverseProperty("RegisterLog")]
    public virtual ICollection<TR_Register_CallStaffCounter> TR_Register_CallStaffCounters { get; set; } = new List<TR_Register_CallStaffCounter>();

    [ForeignKey("UnitID")]
    [InverseProperty("TR_RegisterLogs")]
    public virtual tm_Unit? Unit { get; set; }
}
