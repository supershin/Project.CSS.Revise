using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
public partial class vw_BI_Backlog
{
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

    public int? NonActive_Unit { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? NonActive_Value { get; set; }

    public int? Active_Unit { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? Active_Value { get; set; }

    public int? Total_Unit { get; set; }

    public int? Total_Value { get; set; }

    public int? TotalNonAvailable_Unit { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? TotalNonAvailable_Value { get; set; }

    public int? TotalTransfer_Unit { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? TotalTransfer_Value { get; set; }
}
