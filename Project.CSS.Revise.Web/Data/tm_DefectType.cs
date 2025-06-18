using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_DefectType")]
public partial class tm_DefectType
{
    [Key]
    public int ID { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public int? LineOrder { get; set; }

    public int? Revise { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("DefectType")]
    public virtual ICollection<TR_DefectTypeVendor_Mapping> TR_DefectTypeVendor_Mappings { get; set; } = new List<TR_DefectTypeVendor_Mapping>();

    [InverseProperty("DefectType")]
    public virtual ICollection<TR_QC_Defect> TR_QC_Defects { get; set; } = new List<TR_QC_Defect>();

    [InverseProperty("DefectType")]
    public virtual ICollection<tm_DefectAreaType_Mapping> tm_DefectAreaType_Mappings { get; set; } = new List<tm_DefectAreaType_Mapping>();

    [InverseProperty("DefectType")]
    public virtual ICollection<tm_DefectDescription> tm_DefectDescriptions { get; set; } = new List<tm_DefectDescription>();
}
