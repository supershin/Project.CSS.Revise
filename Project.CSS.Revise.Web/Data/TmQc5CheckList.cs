using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmQc5CheckList
{
    public int Id { get; set; }

    public string? ProjectType { get; set; }

    public int? ParentId { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<TrQc5CheckListDetail> TrQc5CheckListDetails { get; set; } = new List<TrQc5CheckListDetail>();
}
