using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_DefectDescription")]
public partial class tm_DefectDescription
{
    [Key]
    public int ID { get; set; }

    public int? DefectTypeID { get; set; }

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

    [ForeignKey("DefectTypeID")]
    [InverseProperty("tm_DefectDescriptions")]
    public virtual tm_DefectType? DefectType { get; set; }

    [InverseProperty("DefectDescription")]
    public virtual ICollection<TR_QC_Defect> TR_QC_Defects { get; set; } = new List<TR_QC_Defect>();
}
