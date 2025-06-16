using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmExt
{
    public int Id { get; set; }

    public int? ExtTypeId { get; set; }

    public string? Name { get; set; }

    public string? NameEng { get; set; }

    public string? OtherValue { get; set; }

    public string? OtherValue2 { get; set; }

    public int? ParentId { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmExtType? ExtType { get; set; }

    public virtual ICollection<PrAttachFile> PrAttachFileAttachTypes { get; set; } = new List<PrAttachFile>();

    public virtual ICollection<PrAttachFile> PrAttachFileUserTypes { get; set; } = new List<PrAttachFile>();

    public virtual ICollection<PrCustomerCareer> PrCustomerCareers { get; set; } = new List<PrCustomerCareer>();

    public virtual ICollection<PrCustomerDebt> PrCustomerDebts { get; set; } = new List<PrCustomerDebt>();

    public virtual ICollection<PrCustomerIncome> PrCustomerIncomes { get; set; } = new List<PrCustomerIncome>();

    public virtual ICollection<PrLoan> PrLoans { get; set; } = new List<PrLoan>();

    public virtual ICollection<PrUser> PrUsers { get; set; } = new List<PrUser>();

    public virtual ICollection<TmLineToken> TmLineTokens { get; set; } = new List<TmLineToken>();

    public virtual ICollection<TmMenu> TmMenus { get; set; } = new List<TmMenu>();

    public virtual ICollection<TmProjectUserMapping> TmProjectUserMappings { get; set; } = new List<TmProjectUserMapping>();

    public virtual ICollection<TmProject> TmProjects { get; set; } = new List<TmProject>();

    public virtual ICollection<TmResponsibleMapping> TmResponsibleMappings { get; set; } = new List<TmResponsibleMapping>();

    public virtual ICollection<TmRole> TmRoles { get; set; } = new List<TmRole>();

    public virtual ICollection<TmUnit> TmUnitMeterTypes { get; set; } = new List<TmUnit>();

    public virtual ICollection<TmUnit> TmUnitUnitStatusSaleNavigations { get; set; } = new List<TmUnit>();

    public virtual ICollection<TmUser> TmUsers { get; set; } = new List<TmUser>();

    public virtual ICollection<TrAttachFileQc> TrAttachFileQcs { get; set; } = new List<TrAttachFileQc>();

    public virtual ICollection<TrContactLog> TrContactLogQctypes { get; set; } = new List<TrContactLog>();

    public virtual ICollection<TrContactLog> TrContactLogTeams { get; set; } = new List<TrContactLog>();

    public virtual ICollection<TrCustomerSatisfaction> TrCustomerSatisfactions { get; set; } = new List<TrCustomerSatisfaction>();

    public virtual ICollection<TrLetter> TrLetterApproveStatuses { get; set; } = new List<TrLetter>();

    public virtual ICollection<TrLetter> TrLetterLetterTypes { get; set; } = new List<TrLetter>();

    public virtual ICollection<TrLetter> TrLetterSendStatuses { get; set; } = new List<TrLetter>();

    public virtual ICollection<TrLetter> TrLetterSendTypes { get; set; } = new List<TrLetter>();

    public virtual ICollection<TrLetter> TrLetterVerifyStatuses { get; set; } = new List<TrLetter>();

    public virtual ICollection<TrMenuRolePermission> TrMenuRolePermissionDepartments { get; set; } = new List<TrMenuRolePermission>();

    public virtual ICollection<TrMenuRolePermission> TrMenuRolePermissionQctypes { get; set; } = new List<TrMenuRolePermission>();

    public virtual ICollection<TrProjectAppointLimitMapping> TrProjectAppointLimitMappingDays { get; set; } = new List<TrProjectAppointLimitMapping>();

    public virtual ICollection<TrProjectAppointLimitMapping> TrProjectAppointLimitMappingTimes { get; set; } = new List<TrProjectAppointLimitMapping>();

    public virtual ICollection<TrProjectCounterMapping> TrProjectCounterMappings { get; set; } = new List<TrProjectCounterMapping>();

    public virtual ICollection<TrProjectStatus> TrProjectStatuses { get; set; } = new List<TrProjectStatus>();

    public virtual ICollection<TrProjectZoneMapping> TrProjectZoneMappings { get; set; } = new List<TrProjectZoneMapping>();

    public virtual ICollection<TrQc1> TrQc1s { get; set; } = new List<TrQc1>();

    public virtual ICollection<TrQc2> TrQc2s { get; set; } = new List<TrQc2>();

    public virtual ICollection<TrQc3> TrQc3s { get; set; } = new List<TrQc3>();

    public virtual ICollection<TrQc4> TrQc4s { get; set; } = new List<TrQc4>();

    public virtual ICollection<TrQc5CheckListDetail> TrQc5CheckListDetails { get; set; } = new List<TrQc5CheckListDetail>();

    public virtual ICollection<TrQc5> TrQc5CustRelations { get; set; } = new List<TrQc5>();

    public virtual ICollection<TrQc5Open> TrQc5Opens { get; set; } = new List<TrQc5Open>();

    public virtual ICollection<TrQc5> TrQc5QcStatuses { get; set; } = new List<TrQc5>();

    public virtual ICollection<TrQc6> TrQc6CustRelations { get; set; } = new List<TrQc6>();

    public virtual ICollection<TrQc6> TrQc6QcStatuses { get; set; } = new List<TrQc6>();

    public virtual ICollection<TrQcCheckList> TrQcCheckLists { get; set; } = new List<TrQcCheckList>();

    public virtual ICollection<TrQcContactLog> TrQcContactLogs { get; set; } = new List<TrQcContactLog>();

    public virtual ICollection<TrQcDefectOverDueExpect> TrQcDefectOverDueExpects { get; set; } = new List<TrQcDefectOverDueExpect>();

    public virtual ICollection<TrQcDefect> TrQcDefects { get; set; } = new List<TrQcDefect>();

    public virtual ICollection<TrQuestionC> TrQuestionCs { get; set; } = new List<TrQuestionC>();

    public virtual ICollection<TrRegisterLog> TrRegisterLogs { get; set; } = new List<TrRegisterLog>();

    public virtual ICollection<TrRemarkUnitStatusMapping> TrRemarkUnitStatusMappingRemarkUnitStatusCs { get; set; } = new List<TrRemarkUnitStatusMapping>();

    public virtual ICollection<TrRemarkUnitStatusMapping> TrRemarkUnitStatusMappingUnitStatusCs { get; set; } = new List<TrRemarkUnitStatusMapping>();

    public virtual ICollection<TrReviseUnitPromotion> TrReviseUnitPromotionApproveStatusId2Navigations { get; set; } = new List<TrReviseUnitPromotion>();

    public virtual ICollection<TrReviseUnitPromotion> TrReviseUnitPromotionApproveStatuses { get; set; } = new List<TrReviseUnitPromotion>();

    public virtual ICollection<TrReviseUnitPromotonApprover> TrReviseUnitPromotonApprovers { get; set; } = new List<TrReviseUnitPromotonApprover>();

    public virtual ICollection<TrSyncQc> TrSyncQcs { get; set; } = new List<TrSyncQc>();

    public virtual ICollection<TrTargetRollingPlan> TrTargetRollingPlanPlanAmounts { get; set; } = new List<TrTargetRollingPlan>();

    public virtual ICollection<TrTargetRollingPlan> TrTargetRollingPlanPlanTypes { get; set; } = new List<TrTargetRollingPlan>();

    public virtual ICollection<TrTerminateTransferAppoint> TrTerminateTransferAppoints { get; set; } = new List<TrTerminateTransferAppoint>();

    public virtual ICollection<TrUnitDocument> TrUnitDocumentDocumentTypes { get; set; } = new List<TrUnitDocument>();

    public virtual ICollection<TrUnitDocumentNo> TrUnitDocumentNoDocumentTypes { get; set; } = new List<TrUnitDocumentNo>();

    public virtual ICollection<TrUnitDocumentNo> TrUnitDocumentNoQctypes { get; set; } = new List<TrUnitDocumentNo>();

    public virtual ICollection<TrUnitDocument> TrUnitDocumentQctypes { get; set; } = new List<TrUnitDocument>();

    public virtual ICollection<TrUnitFurniture> TrUnitFurnitures { get; set; } = new List<TrUnitFurniture>();
}
