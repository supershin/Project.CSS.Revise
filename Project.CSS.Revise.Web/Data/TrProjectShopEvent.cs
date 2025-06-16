using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrProjectShopEvent
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public int? ShopId { get; set; }

    public DateTime? EventDate { get; set; }

    public int? UnitQuota { get; set; }

    public int? ShopQuota { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TmShop? Shop { get; set; }
}
