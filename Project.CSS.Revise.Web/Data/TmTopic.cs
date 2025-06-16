using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmTopic
{
    public int Id { get; set; }

    public string? ProjectType { get; set; }

    public int? QctypeId { get; set; }

    public string? Name { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<TmSubject> TmSubjects { get; set; } = new List<TmSubject>();

    public virtual ICollection<TrQcCheckList> TrQcCheckLists { get; set; } = new List<TrQcCheckList>();
}
