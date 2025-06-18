using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC_Defect")]
[Index("ProjectID", "UnitID", "QC_ID", "QCTypeID", "DefectStatusID", "DefectAreaID", "DefectTypeID", Name = "NonClusteredIndex-20201109-102830")]
public partial class TR_QC_Defect
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public Guid? QC_ID { get; set; }

    public int? QCTypeID { get; set; }

    public int? DefectStatusID { get; set; }

    public int? DefectAreaID { get; set; }

    public int? DefectTypeID { get; set; }

    public int? DefectDescriptionID { get; set; }

    public string? DefectDetail { get; set; }

    public string? Note { get; set; }

    public Guid? FloorPlanID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PointX { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PointY { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ClearDate { get; set; }

    public int? ClearBy { get; set; }

    public bool? IsMajor { get; set; }

    [ForeignKey("DefectAreaID")]
    [InverseProperty("TR_QC_Defects")]
    public virtual tm_DefectArea? DefectArea { get; set; }

    [ForeignKey("DefectDescriptionID")]
    [InverseProperty("TR_QC_Defects")]
    public virtual tm_DefectDescription? DefectDescription { get; set; }

    [ForeignKey("DefectStatusID")]
    [InverseProperty("TR_QC_Defects")]
    public virtual tm_Ext? DefectStatus { get; set; }

    [ForeignKey("DefectTypeID")]
    [InverseProperty("TR_QC_Defects")]
    public virtual tm_DefectType? DefectType { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_QC_Defects")]
    public virtual tm_Project? Project { get; set; }

    [InverseProperty("Defect")]
    public virtual ICollection<TR_QC_DefectResource> TR_QC_DefectResources { get; set; } = new List<TR_QC_DefectResource>();

    [InverseProperty("Defect")]
    public virtual ICollection<TR_QC_Defect_OverDueExpect> TR_QC_Defect_OverDueExpects { get; set; } = new List<TR_QC_Defect_OverDueExpect>();

    [ForeignKey("UnitID")]
    [InverseProperty("TR_QC_Defects")]
    public virtual tm_Unit? Unit { get; set; }
}
