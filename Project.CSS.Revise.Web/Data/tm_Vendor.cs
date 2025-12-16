using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Vendor")]
public partial class tm_Vendor
{
    [Key]
    public int ID { get; set; }

    public int? VendorTypeID { get; set; }

    [StringLength(500)]
    public string? Name { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("Vendor")]
    public virtual ICollection<TR_DefectTypeVendor_Mapping> TR_DefectTypeVendor_Mappings { get; set; } = new List<TR_DefectTypeVendor_Mapping>();
}
