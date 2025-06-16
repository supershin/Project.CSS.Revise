using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmFuniture
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CraeteDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<TrUnitFurnitureDetail> TrUnitFurnitureDetails { get; set; } = new List<TrUnitFurnitureDetail>();
}
