using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmCloseProject
{
    public string? ProjectId { get; set; }

    public DateTime? CloseDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }
}
