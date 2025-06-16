using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class VwBiProjectInitialMonth
{
    public string ProjectId { get; set; } = null!;

    public string? ProjectName { get; set; }

    public string? ProjectStatus { get; set; }

    public string? Md { get; set; }

    public string? Yyyymm { get; set; }

    public int? Year { get; set; }

    public int? Month { get; set; }

    public string? LastMonth { get; set; }
}
