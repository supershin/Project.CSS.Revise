using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
public partial class Sys_Master_Project
{
    [StringLength(15)]
    public string ProjectID { get; set; } = null!;

    public string? ProjectName { get; set; }

    public string? ProjectNameEng { get; set; }

    public string? ProjectNameTitleDeed { get; set; }

    [StringLength(1)]
    public string? ProjectType { get; set; }

    public int? RealEstateType { get; set; }

    [StringLength(50)]
    public string? BUID { get; set; }

    public int? SubBUID { get; set; }

    [StringLength(15)]
    public string? BrandID { get; set; }

    [StringLength(15)]
    public string? CompanyID { get; set; }

    public int? TotalUnit { get; set; }

    public int? TotalTitleDeed { get; set; }

    [StringLength(15)]
    public string? ProjectStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ProjectOpen { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ProjectClose { get; set; }

    [StringLength(50)]
    public string? ProjectOwner { get; set; }

    [StringLength(20)]
    public string? ProjectTel { get; set; }

    [StringLength(20)]
    public string? ProjectFax { get; set; }

    [StringLength(100)]
    public string? ProjectEmail { get; set; }

    [StringLength(100)]
    public string? ProjectWebsite { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BuildCompleteDate { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? ProjectValues { get; set; }

    public int? AreaRai { get; set; }

    public int? Areangan { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? AreaSquareWah { get; set; }

    public string? Remark { get; set; }

    public int? JuristicID { get; set; }

    [StringLength(50)]
    public string? JuristicName { get; set; }

    [StringLength(50)]
    public string? JuristicNameEng { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? JuristicDate { get; set; }

    [StringLength(50)]
    public string? ImgPath { get; set; }

    public double? BudgetAlertPerc { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? BudgetAlertAmt { get; set; }

    public bool? isRenovate { get; set; }

    public bool? isDelete { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(50)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifyDate { get; set; }

    [StringLength(50)]
    public string? ModifyBy { get; set; }

    [StringLength(50)]
    public string? BOQID { get; set; }

    [StringLength(255)]
    public string? MoFinanceNameTH { get; set; }

    [StringLength(255)]
    public string? MoFinanceNameEN { get; set; }

    [StringLength(255)]
    public string? Port { get; set; }

    [StringLength(255)]
    public string? AbProjectName { get; set; }

    [StringLength(510)]
    public string? ProjectImagePath { get; set; }

    [StringLength(20)]
    public string? SAPWBSCode { get; set; }

    [StringLength(20)]
    public string? ACCWBSCode { get; set; }

    [StringLength(20)]
    public string? COMWBSCode { get; set; }

    [StringLength(20)]
    public string? PlantCode { get; set; }

    [StringLength(50)]
    public string? SAPProfitCenter { get; set; }

    [StringLength(50)]
    public string? SAPProfixCenter { get; set; }

    [StringLength(50)]
    public string? SAPPostCenter { get; set; }

    [StringLength(50)]
    public string? SAPCostCenter { get; set; }

    public bool? AllowSendSAP { get; set; }

    [StringLength(50)]
    public string? SAPCostCenter2 { get; set; }

    [StringLength(50)]
    public string? SAPBandCode { get; set; }

    [StringLength(50)]
    public string? SAPPlantCode { get; set; }

    [StringLength(50)]
    public string? SAPPlantCode2 { get; set; }

    [StringLength(50)]
    public string? SAPWBSCode47 { get; set; }

    [StringLength(50)]
    public string? SAPCostCenter47 { get; set; }

    [StringLength(50)]
    public string? SAPCostCenter472 { get; set; }

    public string? AccountStaffName { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? ProjectValues2 { get; set; }

    [StringLength(20)]
    public string? ContractType { get; set; }

    [StringLength(50)]
    public string? AccountProject { get; set; }

    [Column(TypeName = "text")]
    public string? Base64Image { get; set; }

    public bool? isReserve { get; set; }

    [StringLength(50)]
    public string? AllocateLand { get; set; }

    [StringLength(5)]
    public string? JuristicStatus { get; set; }

    public int? GradeID { get; set; }

    public string? ProjectRemark { get; set; }

    [StringLength(255)]
    public string? LineOfficial { get; set; }
}
