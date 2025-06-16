using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrProjectLandOffice
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public int? LandOfficeId { get; set; }

    public virtual TmLandOffice? LandOffice { get; set; }

    public virtual TmProject? Project { get; set; }
}
