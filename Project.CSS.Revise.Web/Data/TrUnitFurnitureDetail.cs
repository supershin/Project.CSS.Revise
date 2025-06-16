using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrUnitFurnitureDetail
{
    public long Id { get; set; }

    public Guid? UnitFurnitureId { get; set; }

    public int? FurnitureId { get; set; }

    public int? Amount { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmFuniture? Furniture { get; set; }

    public virtual TrUnitFurniture? UnitFurniture { get; set; }
}
