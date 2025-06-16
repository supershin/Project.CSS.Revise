using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrSyncLog
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public string? Detail { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public virtual TmProject? Project { get; set; }
}
