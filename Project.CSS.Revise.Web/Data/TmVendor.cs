using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmVendor
{
    public int Id { get; set; }

    public int? VendorTypeId { get; set; }

    public string? Name { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<TrDefectTypeVendorMapping> TrDefectTypeVendorMappings { get; set; } = new List<TrDefectTypeVendorMapping>();
}
