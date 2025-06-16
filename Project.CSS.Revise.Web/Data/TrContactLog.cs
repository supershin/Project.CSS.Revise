using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrContactLog
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public Guid? QcId { get; set; }

    public int? QctypeId { get; set; }

    public int? TeamId { get; set; }

    public int? BankId { get; set; }

    public DateTime? ContactDate { get; set; }

    public string? ContactTime { get; set; }

    public string? ContactName { get; set; }

    public int? CustomerTypeId { get; set; }

    public int? ContactReasonId { get; set; }

    public DateTime? PaymentDueDate { get; set; }

    public string? Detail { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmBank? Bank { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TrQc6? Qc { get; set; }

    public virtual TmExt? Qctype { get; set; }

    public virtual TmExt? Team { get; set; }

    public virtual ICollection<TrQcContactLogResource> TrQcContactLogResources { get; set; } = new List<TrQcContactLogResource>();

    public virtual TmUnit? Unit { get; set; }
}
