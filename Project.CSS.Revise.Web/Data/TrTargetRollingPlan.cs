using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrTargetRollingPlan
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public int? PlanTypeId { get; set; }

    public int? PlanAmountId { get; set; }

    public DateTime? MonthlyDate { get; set; }

    public decimal? Amount { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmExt? PlanAmount { get; set; }

    public virtual TmExt? PlanType { get; set; }

    public virtual TmProject? Project { get; set; }
}
