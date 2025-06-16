using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmCompany
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? NameEng { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<TrCompanyProject> TrCompanyProjects { get; set; } = new List<TrCompanyProject>();
}
