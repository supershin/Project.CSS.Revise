using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class VwBiBacklog
{
    public string ProjectId { get; set; } = null!;

    public string? ProjectName { get; set; }

    public string? ProjectStatus { get; set; }

    public string? Md { get; set; }

    public int? NonActiveUnit { get; set; }

    public decimal? NonActiveValue { get; set; }

    public int? ActiveUnit { get; set; }

    public decimal? ActiveValue { get; set; }

    public int? TotalUnit { get; set; }

    public int? TotalValue { get; set; }

    public int? TotalNonAvailableUnit { get; set; }

    public decimal? TotalNonAvailableValue { get; set; }

    public int? TotalTransferUnit { get; set; }

    public decimal? TotalTransferValue { get; set; }
}
