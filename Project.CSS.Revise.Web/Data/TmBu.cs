using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmBu
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<TmBuprojectMapping> TmBuprojectMappings { get; set; } = new List<TmBuprojectMapping>();
}
