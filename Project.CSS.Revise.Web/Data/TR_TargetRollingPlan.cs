using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_TargetRollingPlan")]
public partial class TR_TargetRollingPlan
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public int? PlanTypeID { get; set; }

    public int? PlanAmountID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? MonthlyDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("PlanAmountID")]
    [InverseProperty("TR_TargetRollingPlanPlanAmounts")]
    public virtual tm_Ext? PlanAmount { get; set; }

    [ForeignKey("PlanTypeID")]
    [InverseProperty("TR_TargetRollingPlanPlanTypes")]
    public virtual tm_Ext? PlanType { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_TargetRollingPlans")]
    public virtual tm_Project? Project { get; set; }
}
