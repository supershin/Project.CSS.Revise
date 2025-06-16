using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmDefectType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? LineOrder { get; set; }

    public int? Revise { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<TmDefectAreaTypeMapping> TmDefectAreaTypeMappings { get; set; } = new List<TmDefectAreaTypeMapping>();

    public virtual ICollection<TmDefectDescription> TmDefectDescriptions { get; set; } = new List<TmDefectDescription>();

    public virtual ICollection<TrDefectTypeVendorMapping> TrDefectTypeVendorMappings { get; set; } = new List<TrDefectTypeVendorMapping>();

    public virtual ICollection<TrQcDefect> TrQcDefects { get; set; } = new List<TrQcDefect>();
}
