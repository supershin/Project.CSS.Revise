using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrQc5ProjectSendMail
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public string? SendToType { get; set; }

    public string? Email { get; set; }

    public virtual TmProject? Project { get; set; }
}
