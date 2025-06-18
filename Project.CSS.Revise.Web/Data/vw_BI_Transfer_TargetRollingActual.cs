using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
public partial class vw_BI_Transfer_TargetRollingActual
{
    public int? Year { get; set; }

    public int? Month { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string ProjectID { get; set; } = null!;

    [StringLength(500)]
    public string? ProjectName { get; set; }

    [StringLength(200)]
    public string? ProjectStatus { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? MD { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal Target_Unit { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal Target_Value { get; set; }

    public int Actual_Unit { get; set; }

    [Column(TypeName = "numeric(38, 2)")]
    public decimal Actual_Value { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? AccumTarget_Unit { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? AccumTarget_Value { get; set; }

    public int? AccumActual_Unit { get; set; }

    [Column(TypeName = "numeric(38, 2)")]
    public decimal? AccumActual_Value { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? AccumRollingBase_Unit { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? AccumRollingBase_Value { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal Rolling_Unit { get; set; }

    [Column(TypeName = "numeric(38, 2)")]
    public decimal Rolling_Value { get; set; }
}
