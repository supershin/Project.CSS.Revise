using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC_Defect_OverDueExpect")]
public partial class TR_QC_Defect_OverDueExpect
{
    [Key]
    public long ID { get; set; }

    public Guid? DefectID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? OpenDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InprocessDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpectDate { get; set; }

    public int? EstimateStatusID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EstimateDate { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("DefectID")]
    [InverseProperty("TR_QC_Defect_OverDueExpects")]
    public virtual TR_QC_Defect? Defect { get; set; }

    [ForeignKey("EstimateStatusID")]
    [InverseProperty("TR_QC_Defect_OverDueExpects")]
    public virtual tm_Ext? EstimateStatus { get; set; }
}
