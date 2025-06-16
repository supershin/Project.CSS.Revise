using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class VwDefect
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public int? QctypeId { get; set; }

    public int? DefectAreaId { get; set; }

    public string? AreaName { get; set; }

    public int? DefectTypeId { get; set; }

    public string? TypeName { get; set; }

    public int? DefectDescriptionId { get; set; }

    public string? DescName { get; set; }

    public DateTime? CreateDate { get; set; }
}
