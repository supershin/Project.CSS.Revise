using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrUnitEvent
{
    public int Id { get; set; }

    public int? EventId { get; set; }

    public Guid? UnitId { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CraeteDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmEvent? Event { get; set; }

    public virtual TmUnit? Unit { get; set; }
}
