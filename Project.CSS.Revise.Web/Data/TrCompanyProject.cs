using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrCompanyProject
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public string? ProjectId { get; set; }

    public virtual TmCompany? Company { get; set; }

    public virtual TmProject? Project { get; set; }
}
