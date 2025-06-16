using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrQcDefectResource
{
    public Guid Id { get; set; }

    public Guid? DefectId { get; set; }

    public Guid? ResourceId { get; set; }

    public virtual TrQcDefect? Defect { get; set; }

    public virtual TrResource? Resource { get; set; }
}
