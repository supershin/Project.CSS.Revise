using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class VwUnit
{
    public string ProjectId { get; set; } = null!;

    public string? ProjectName { get; set; }

    public string? UnitCode { get; set; }

    public string? Build { get; set; }

    public string? Floor { get; set; }
}
