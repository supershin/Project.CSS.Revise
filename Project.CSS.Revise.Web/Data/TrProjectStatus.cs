using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrProjectStatus
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public int? StatusId { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TmExt? Status { get; set; }
}
