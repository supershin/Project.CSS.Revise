using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrProjectEmailMapping
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public string? Email { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual TmProject? Project { get; set; }
}
