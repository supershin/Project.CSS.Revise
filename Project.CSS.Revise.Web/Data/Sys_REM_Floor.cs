using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("Sys_REM_Floor")]
public partial class Sys_REM_Floor
{
    public int FloorID { get; set; }

    public int? TowerID { get; set; }

    [StringLength(10)]
    public string? ProjectID { get; set; }

    [StringLength(50)]
    public string? FloorName { get; set; }

    [StringLength(50)]
    public string? FloorNameEng { get; set; }

    public string? FloorNameTransfer { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [StringLength(500)]
    public string? FloorPlanPath { get; set; }

    public bool? isDelete { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(50)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifyDate { get; set; }

    [StringLength(50)]
    public string? ModifyBy { get; set; }

    public int? Sequence { get; set; }
}
