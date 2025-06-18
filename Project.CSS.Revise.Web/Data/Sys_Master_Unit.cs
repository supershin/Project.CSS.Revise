using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
public partial class Sys_Master_Unit
{
    [StringLength(50)]
    public string UnitID { get; set; } = null!;

    [StringLength(10)]
    public string? SBUID { get; set; }

    [StringLength(20)]
    public string? UnitNumber { get; set; }

    [StringLength(20)]
    public string? UnitNumber2 { get; set; }

    [StringLength(10)]
    public string? ProjectID { get; set; }

    [StringLength(20)]
    public string? ModelID { get; set; }

    public int? PhaseID { get; set; }

    public int? SubPhaseID { get; set; }

    [StringLength(50)]
    public string? Block { get; set; }

    public int? TowerID { get; set; }

    public int? FloorID { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? SellingArea { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? HouseArea { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? TitledeedArea { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? BuildingArea { get; set; }

    [StringLength(50)]
    public string? HouseNumber { get; set; }

    public int? HouseYear { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QCVendorDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QCCustomerDate { get; set; }

    public int? AssetType { get; set; }

    [StringLength(10)]
    public string? UnitStatus { get; set; }

    [StringLength(255)]
    public string? Location { get; set; }

    public bool? isDelete { get; set; }

    public string? Remark { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(50)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifyDate { get; set; }

    [StringLength(50)]
    public string? ModifyBy { get; set; }

    [StringLength(50)]
    public string? BillPaymentCode { get; set; }

    [StringLength(50)]
    public string? SAPWBSCode { get; set; }

    [StringLength(1)]
    public string? SAPStatusFlag { get; set; }

    public string? SAPRemarks { get; set; }

    [StringLength(4)]
    public string? SAPCurrStatus { get; set; }

    [StringLength(4)]
    public string? SAPOldStatus { get; set; }

    [StringLength(50)]
    public string? Zone { get; set; }

    [StringLength(50)]
    public string? AddressNo { get; set; }

    [StringLength(50)]
    public string? Moo { get; set; }

    [StringLength(50)]
    public string? Soi { get; set; }

    [StringLength(50)]
    public string? Village { get; set; }

    [StringLength(50)]
    public string? Road { get; set; }

    [StringLength(50)]
    public string? SubDistrict { get; set; }

    [StringLength(50)]
    public string? District { get; set; }

    [StringLength(50)]
    public string? Province { get; set; }

    [StringLength(50)]
    public string? ZipCode { get; set; }

    [StringLength(50)]
    public string? Country { get; set; }

    [StringLength(50)]
    public string? WorkPackage { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? WorkPackageDate { get; set; }

    public int? BOIID { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? WaterIns { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? WaterInstall { get; set; }

    [StringLength(50)]
    public string? WaterMeterSize { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? ElectricIns { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? ElectricInstall { get; set; }

    [StringLength(50)]
    public string? ElectricMeterSize { get; set; }

    public int? PJLandOfficeID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? WCustomerRequestDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? WEstimatedDateComplete { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ECustomerRequestDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EEstimatedDateComplete { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? ActualEstimatePrice { get; set; }

    [StringLength(20)]
    public string? UnitCombineFlag { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? PublicFee { get; set; }

    [StringLength(10)]
    public string? PublicFeeType { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Transferduedate { get; set; }

    public int? LockLevel { get; set; }

    [StringLength(4)]
    public string? AccountRoom { get; set; }

    public string? PublicFeeDescription { get; set; }

    public bool? IsWaranteeByHandOver { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? LoftArea { get; set; }

    [StringLength(50)]
    public string? AddressNoEN { get; set; }

    [StringLength(50)]
    public string? MooEN { get; set; }

    [StringLength(50)]
    public string? SoiEN { get; set; }

    [StringLength(50)]
    public string? VillageEN { get; set; }

    [StringLength(50)]
    public string? RoadEN { get; set; }

    [StringLength(50)]
    public string? SubDistrictEN { get; set; }

    [StringLength(50)]
    public string? DistrictEN { get; set; }

    [StringLength(50)]
    public string? ProvinceEN { get; set; }

    [StringLength(50)]
    public string? CountryEN { get; set; }

    [StringLength(5)]
    public string? ServitudeRateType { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? TrashRate { get; set; }

    [StringLength(5)]
    public string? TrashRateType { get; set; }

    public int? Parking { get; set; }

    public string? ParkingRemark { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? WaterCoolingRT { get; set; }

    public bool? SaleOnWeb { get; set; }

    [StringLength(50)]
    public string? HoldBy { get; set; }

    [StringLength(50)]
    public string? UnholdBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? HoldDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UnholdDate { get; set; }

    public bool? IsFlagBasePrice { get; set; }

    [StringLength(50)]
    public string? X { get; set; }

    [StringLength(50)]
    public string? Y { get; set; }

    [StringLength(50)]
    public string? Width { get; set; }

    [StringLength(50)]
    public string? Height { get; set; }

    [StringLength(50)]
    public string? UnitMapping { get; set; }
}
