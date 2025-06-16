using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrSync
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public string? Module { get; set; }

    public string? Detail { get; set; }

    public DateTime? SyncDate { get; set; }

    public string? SyncStatus { get; set; }

    public string? Message { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual ICollection<TrSyncLoanBank> TrSyncLoanBanks { get; set; } = new List<TrSyncLoanBank>();

    public virtual ICollection<TrSyncQc> TrSyncQcs { get; set; } = new List<TrSyncQc>();

    public virtual TmUnit? Unit { get; set; }
}
