using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrProjectZoneMapping
{
    public int ProjectZoneId { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual TmExt ProjectZone { get; set; } = null!;
}
