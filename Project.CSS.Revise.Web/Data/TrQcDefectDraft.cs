using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrQcDefectDraft
{
    public Guid DefectId { get; set; }

    public Guid? UnitId { get; set; }

    public int? DefectStatusId { get; set; }

    public string? Remark { get; set; }
}
