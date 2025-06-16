using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class VwBiTransferTargetRolling
{
    public string? ProjectId { get; set; }

    public string? Yyyymm { get; set; }

    public int? Year { get; set; }

    public int? Month { get; set; }

    public decimal? TargetUnit { get; set; }

    public decimal? TargetValue { get; set; }

    public decimal? RollingUnit { get; set; }

    public decimal? RollingValue { get; set; }
}
