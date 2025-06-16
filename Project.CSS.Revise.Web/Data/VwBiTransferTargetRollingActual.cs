using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class VwBiTransferTargetRollingActual
{
    public int? Year { get; set; }

    public int? Month { get; set; }

    public string ProjectId { get; set; } = null!;

    public string? ProjectName { get; set; }

    public string? ProjectStatus { get; set; }

    public string? Md { get; set; }

    public decimal TargetUnit { get; set; }

    public decimal TargetValue { get; set; }

    public int ActualUnit { get; set; }

    public decimal ActualValue { get; set; }

    public decimal? AccumTargetUnit { get; set; }

    public decimal? AccumTargetValue { get; set; }

    public int? AccumActualUnit { get; set; }

    public decimal? AccumActualValue { get; set; }

    public decimal? AccumRollingBaseUnit { get; set; }

    public decimal? AccumRollingBaseValue { get; set; }

    public decimal RollingUnit { get; set; }

    public decimal RollingValue { get; set; }
}
