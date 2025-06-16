using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrLog
{
    public Guid Id { get; set; }

    public string? UserName { get; set; }

    public string? Controller { get; set; }

    public string? Action { get; set; }

    public string? Url { get; set; }

    public string? Form { get; set; }

    public string? QueryString { get; set; }

    public DateTime? CreateDate { get; set; }
}
