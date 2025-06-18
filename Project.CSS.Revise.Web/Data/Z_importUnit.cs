using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("Z_importUnit")]
public partial class Z_importUnit
{
    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string? Build { get; set; }

    public int? Floor { get; set; }

    public int? Room { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? UnitCode { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? UnitType { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Area { get; set; }

    [StringLength(500)]
    public string? CustomerName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? CustomerMobile { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? CustomerEmail { get; set; }
}
