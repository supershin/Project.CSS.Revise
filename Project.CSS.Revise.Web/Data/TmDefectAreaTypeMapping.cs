using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmDefectAreaTypeMapping
{
    public int Id { get; set; }

    public int? DefectAreaId { get; set; }

    public int? DefectTypeId { get; set; }

    public virtual TmDefectArea? DefectArea { get; set; }

    public virtual TmDefectType? DefectType { get; set; }
}
