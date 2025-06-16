using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmPosition
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? NameEng { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<TrUserPosition> TrUserPositions { get; set; } = new List<TrUserPosition>();
}
