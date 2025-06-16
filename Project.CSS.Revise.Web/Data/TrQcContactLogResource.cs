using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrQcContactLogResource
{
    public int Id { get; set; }

    public Guid? QccontactLogId { get; set; }

    public Guid? ResourceId { get; set; }

    public virtual TrContactLog? QccontactLog { get; set; }

    public virtual TrResource? Resource { get; set; }
}
