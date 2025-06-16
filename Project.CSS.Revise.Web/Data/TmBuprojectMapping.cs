using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmBuprojectMapping
{
    public int Id { get; set; }

    public int? Buid { get; set; }

    public string? ProjectId { get; set; }

    public virtual TmBu? Bu { get; set; }

    public virtual TmProject? Project { get; set; }
}
