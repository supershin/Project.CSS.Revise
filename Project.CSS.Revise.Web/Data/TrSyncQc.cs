using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrSyncQc
{
    public Guid Id { get; set; }

    public Guid? SyncId { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public string? UnitCode { get; set; }

    public int? QctypeId { get; set; }

    public DateTime? QcappointDate { get; set; }

    public string? QcappointTimeFrom { get; set; }

    public string? QcappointTimeTo { get; set; }

    public string? QcresponseUserId { get; set; }

    public DateTime? QcresponseDate { get; set; }

    public string? Qcremark { get; set; }

    public int? SyncTypeId { get; set; }

    public string? SyncTypeDetail { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TmExt? Qctype { get; set; }

    public virtual TrSync? Sync { get; set; }

    public virtual TmUnit? Unit { get; set; }
}
