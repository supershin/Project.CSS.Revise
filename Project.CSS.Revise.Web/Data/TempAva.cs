using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TempAva
{
    public string ProjectId { get; set; } = null!;

    public string? ProjectName { get; set; }

    public string UnitCode { get; set; } = null!;

    public string? Build { get; set; }

    public int? Floor { get; set; }
}
