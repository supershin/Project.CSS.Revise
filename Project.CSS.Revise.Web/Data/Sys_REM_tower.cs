using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("Sys_REM_tower")]
public partial class Sys_REM_tower
{
    public int TowerID { get; set; }

    [StringLength(10)]
    public string? ProjectID { get; set; }

    [StringLength(50)]
    public string? TowerName { get; set; }

    [StringLength(50)]
    public string? TowerNameEng { get; set; }

    public string? TowerNameTransfer { get; set; }

    [StringLength(200)]
    public string? TitledeedNumber { get; set; }

    [StringLength(200)]
    public string? LandNumber { get; set; }

    [StringLength(200)]
    public string? LandSurveyArea { get; set; }

    [StringLength(200)]
    public string? LandPortionNumber { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? TitleDeedArea { get; set; }

    public int? LandBook { get; set; }

    [StringLength(50)]
    public string? LandBookPage { get; set; }

    [StringLength(50)]
    public string? TowerNumber { get; set; }

    [StringLength(50)]
    public string? TowerLicenseNo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FinishDate { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    public bool? isDelete { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(50)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifyDate { get; set; }

    [StringLength(50)]
    public string? ModifyBy { get; set; }

    public int? JuristicID { get; set; }

    [StringLength(50)]
    public string? JuristicName { get; set; }

    [StringLength(50)]
    public string? TowerTotalArea { get; set; }

    [StringLength(1)]
    public string? AccountTower { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RegisterBuildingDate { get; set; }
}
