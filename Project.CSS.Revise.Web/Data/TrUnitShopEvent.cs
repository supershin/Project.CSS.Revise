using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrUnitShopEvent
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public string? UnitCode { get; set; }

    public string? ContractNumber { get; set; }

    public int? ShopId { get; set; }

    public DateTime? EventDate { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }
}
