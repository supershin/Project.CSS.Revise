using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrReviseUnitPromotion
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public Guid? UnitDocumentId { get; set; }

    public string? ContractNumber { get; set; }

    public string? ContractCustomerName { get; set; }

    public string? ContractCustomerSureName { get; set; }

    public string? ContractAddress { get; set; }

    public string? ContractMoo { get; set; }

    public string? ContractSoi { get; set; }

    public string? ContractRoad { get; set; }

    public string? ContractSubdistrict { get; set; }

    public string? ContractDistrict { get; set; }

    public string? ContractProvince { get; set; }

    public string? ContractPostalCode { get; set; }

    public string? ContractMobile { get; set; }

    public int? ApproveStatusId { get; set; }

    public string? ApproveRemark { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? ApproveBy { get; set; }

    public int? ApproveStatusId2 { get; set; }

    public string? ApproveRemark2 { get; set; }

    public DateTime? ApproveDate2 { get; set; }

    public int? ApproveBy2 { get; set; }

    public Guid? CustomerSignId { get; set; }

    public DateTime? CustomerSignDate { get; set; }

    public Guid? CustomerResourceId { get; set; }

    public int? CustomerBankId { get; set; }

    public string? CustomerBookBank { get; set; }

    public DateTime? PrintDate { get; set; }

    public int? PrintBy { get; set; }

    public DateTime? SendMailDate { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? ClearDate { get; set; }

    public int? ClearBy { get; set; }

    public virtual TmExt? ApproveStatus { get; set; }

    public virtual TmExt? ApproveStatusId2Navigation { get; set; }

    public virtual TmBank? CustomerBank { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual ICollection<TrReviseUnitPromotionDetail> TrReviseUnitPromotionDetails { get; set; } = new List<TrReviseUnitPromotionDetail>();

    public virtual TmUnit? Unit { get; set; }
}
