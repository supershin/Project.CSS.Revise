using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class SysRemFloor
{
    public int FloorId { get; set; }

    public int? TowerId { get; set; }

    public string? ProjectId { get; set; }

    public string? FloorName { get; set; }

    public string? FloorNameEng { get; set; }

    public string? FloorNameTransfer { get; set; }

    public string? Description { get; set; }

    public string? FloorPlanPath { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyBy { get; set; }

    public int? Sequence { get; set; }
}
