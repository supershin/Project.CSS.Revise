using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrDefectTypeVendorMapping
{
    public int Id { get; set; }

    public int? DefectTypeId { get; set; }

    public int? VendorId { get; set; }

    public virtual TmDefectType? DefectType { get; set; }

    public virtual TmVendor? Vendor { get; set; }
}
