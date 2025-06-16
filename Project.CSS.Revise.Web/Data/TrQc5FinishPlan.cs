using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrQc5FinishPlan
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public string? Build { get; set; }

    public int? FloorId { get; set; }

    public DateTime? PlanDate { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }
}
