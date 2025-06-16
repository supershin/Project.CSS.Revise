using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrApiLog
{
    public Guid Id { get; set; }

    public string? Module { get; set; }

    public string? Inbound { get; set; }

    public string? OutBound { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }
}
