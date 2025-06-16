using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrQcContactLog
{
    public int Id { get; set; }

    public Guid? TempId { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public Guid? QcId { get; set; }

    public int? QctypeId { get; set; }

    public string? Detail { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TrQc6? Qc { get; set; }

    public virtual TmExt? Qctype { get; set; }

    public virtual TmUnit? Unit { get; set; }
}
