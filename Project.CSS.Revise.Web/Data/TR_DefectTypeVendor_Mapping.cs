using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_DefectTypeVendor_Mapping")]
public partial class TR_DefectTypeVendor_Mapping
{
    [Key]
    public int ID { get; set; }

    public int? DefectTypeID { get; set; }

    public int? VendorID { get; set; }

    [ForeignKey("DefectTypeID")]
    [InverseProperty("TR_DefectTypeVendor_Mappings")]
    public virtual tm_DefectType? DefectType { get; set; }

    [ForeignKey("VendorID")]
    [InverseProperty("TR_DefectTypeVendor_Mappings")]
    public virtual tm_Vendor? Vendor { get; set; }
}
