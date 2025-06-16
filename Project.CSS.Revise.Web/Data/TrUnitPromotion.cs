using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrUnitPromotion
{
    public long Id { get; set; }

    public Guid? UnitId { get; set; }

    public int? PromotionId { get; set; }

    public bool? IsUsed { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmPromotion? Promotion { get; set; }

    public virtual TmUnit? Unit { get; set; }
}
