using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Unit_Budget_Defect")]
public partial class TR_Unit_Budget_Defect
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public Guid? QC_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? DocumentNo { get; set; }

    public int? BudgetTypeID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? PRNO { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("BudgetTypeID")]
    [InverseProperty("TR_Unit_Budget_Defects")]
    public virtual tm_Ext? BudgetType { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_Unit_Budget_Defects")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("QC_ID")]
    [InverseProperty("TR_Unit_Budget_Defects")]
    public virtual TR_QC6? QC { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_Unit_Budget_Defects")]
    public virtual tm_Unit? Unit { get; set; }
}
