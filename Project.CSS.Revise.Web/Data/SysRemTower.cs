using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class SysRemTower
{
    public int TowerId { get; set; }

    public string? ProjectId { get; set; }

    public string? TowerName { get; set; }

    public string? TowerNameEng { get; set; }

    public string? TowerNameTransfer { get; set; }

    public string? TitledeedNumber { get; set; }

    public string? LandNumber { get; set; }

    public string? LandSurveyArea { get; set; }

    public string? LandPortionNumber { get; set; }

    public decimal? TitleDeedArea { get; set; }

    public int? LandBook { get; set; }

    public string? LandBookPage { get; set; }

    public string? TowerNumber { get; set; }

    public string? TowerLicenseNo { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? FinishDate { get; set; }

    public string? Description { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyBy { get; set; }

    public int? JuristicId { get; set; }

    public string? JuristicName { get; set; }

    public string? TowerTotalArea { get; set; }

    public string? AccountTower { get; set; }

    public DateTime? RegisterBuildingDate { get; set; }
}
