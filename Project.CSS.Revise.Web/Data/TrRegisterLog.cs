using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrRegisterLog
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public int? QctypeId { get; set; }

    public int? QueueTypeId { get; set; }

    public int? ResponsibleId { get; set; }

    public DateTime? RegisterDate { get; set; }

    public DateTime? WaitDate { get; set; }

    public DateTime? InprocessDate { get; set; }

    public DateTime? FastFixDate { get; set; }

    public DateTime? FastFixFinishDate { get; set; }

    public DateTime? FinishDate { get; set; }

    public int? CareerTypeId { get; set; }

    public int? TransferTypeId { get; set; }

    public int? ReasonId { get; set; }

    public int? FixedDuration { get; set; }

    public int? Counter { get; set; }

    public Guid? LoanId { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TmExt? Qctype { get; set; }

    public virtual TmUser? Responsible { get; set; }

    public virtual ICollection<TrRegisterBankCounter> TrRegisterBankCounters { get; set; } = new List<TrRegisterBankCounter>();

    public virtual ICollection<TrRegisterBank> TrRegisterBanks { get; set; } = new List<TrRegisterBank>();

    public virtual ICollection<TrRegisterCallStaffCounter> TrRegisterCallStaffCounters { get; set; } = new List<TrRegisterCallStaffCounter>();

    public virtual TmUnit? Unit { get; set; }
}
