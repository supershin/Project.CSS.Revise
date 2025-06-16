using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrQc5CheckList
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public DateTime? CheckDate { get; set; }

    public decimal? ScoreResult { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual ICollection<TrQc5CheckListDetail> TrQc5CheckListDetails { get; set; } = new List<TrQc5CheckListDetail>();

    public virtual TmUnit? Unit { get; set; }
}
