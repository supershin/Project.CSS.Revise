using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmPromotion
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public virtual ICollection<TrUnitPromotion> TrUnitPromotions { get; set; } = new List<TrUnitPromotion>();
}
