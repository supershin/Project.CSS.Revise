using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("TR_QC_Defect_20240518")]
public partial class TR_QC_Defect_20240518
{
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
}
