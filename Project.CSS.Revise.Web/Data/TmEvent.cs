using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmEvent
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CraeteDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual ICollection<TrLetterC> TrLetterCs { get; set; } = new List<TrLetterC>();

    public virtual ICollection<TrUnitEvent> TrUnitEvents { get; set; } = new List<TrUnitEvent>();
}
