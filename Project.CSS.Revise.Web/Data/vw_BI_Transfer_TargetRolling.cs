using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
public partial class vw_BI_Transfer_TargetRolling
{
    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    [StringLength(6)]
    [Unicode(false)]
    public string? YYYYMM { get; set; }

    public int? Year { get; set; }

    public int? Month { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? Target_Unit { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? Target_Value { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? Rolling_Unit { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? Rolling_Value { get; set; }
}
