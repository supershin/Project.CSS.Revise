using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Ext")]
public partial class tm_Ext
{
    [Key]
    public int ID { get; set; }

    public int? ExtTypeID { get; set; }

    [StringLength(200)]
    public string? Name { get; set; }

    [StringLength(200)]
    public string? NameEng { get; set; }

    [Unicode(false)]
    public string? OtherValue { get; set; }

    [StringLength(50)]
    public string? OtherValue2 { get; set; }

    public int? ParentID { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("ExtTypeID")]
    [InverseProperty("tm_Exts")]
    public virtual tm_ExtType? ExtType { get; set; }

    [InverseProperty("AttachType")]
    public virtual ICollection<PR_AttachFile> PR_AttachFileAttachTypes { get; set; } = new List<PR_AttachFile>();

    [InverseProperty("UserType")]
    public virtual ICollection<PR_AttachFile> PR_AttachFileUserTypes { get; set; } = new List<PR_AttachFile>();

    [InverseProperty("Career")]
    public virtual ICollection<PR_CustomerCareer> PR_CustomerCareers { get; set; } = new List<PR_CustomerCareer>();

    [InverseProperty("Debt")]
    public virtual ICollection<PR_CustomerDebt> PR_CustomerDebts { get; set; } = new List<PR_CustomerDebt>();

    [InverseProperty("Income")]
    public virtual ICollection<PR_CustomerIncome> PR_CustomerIncomes { get; set; } = new List<PR_CustomerIncome>();

    [InverseProperty("UserType")]
    public virtual ICollection<PR_Loan> PR_Loans { get; set; } = new List<PR_Loan>();

    [InverseProperty("UserType")]
    public virtual ICollection<PR_User> PR_Users { get; set; } = new List<PR_User>();

    [InverseProperty("QCType")]
    public virtual ICollection<TR_AttachFileQC> TR_AttachFileQCs { get; set; } = new List<TR_AttachFileQC>();

    [InverseProperty("ContactReason")]
    public virtual ICollection<TR_ContactLog> TR_ContactLogContactReasons { get; set; } = new List<TR_ContactLog>();

    [InverseProperty("CustomerType")]
    public virtual ICollection<TR_ContactLog> TR_ContactLogCustomerTypes { get; set; } = new List<TR_ContactLog>();

    [InverseProperty("QCType")]
    public virtual ICollection<TR_ContactLog> TR_ContactLogQCTypes { get; set; } = new List<TR_ContactLog>();

    [InverseProperty("Team")]
    public virtual ICollection<TR_ContactLog> TR_ContactLogTeams { get; set; } = new List<TR_ContactLog>();

    [InverseProperty("QuestionType")]
    public virtual ICollection<TR_CustomerSatisfaction> TR_CustomerSatisfactions { get; set; } = new List<TR_CustomerSatisfaction>();

    [InverseProperty("ApproveStatus")]
    public virtual ICollection<TR_Letter> TR_LetterApproveStatuses { get; set; } = new List<TR_Letter>();

    [InverseProperty("LetterType")]
    public virtual ICollection<TR_Letter> TR_LetterLetterTypes { get; set; } = new List<TR_Letter>();

    [InverseProperty("SendStatus")]
    public virtual ICollection<TR_Letter> TR_LetterSendStatuses { get; set; } = new List<TR_Letter>();

    [InverseProperty("SendType")]
    public virtual ICollection<TR_Letter> TR_LetterSendTypes { get; set; } = new List<TR_Letter>();

    [InverseProperty("VerifyStatus")]
    public virtual ICollection<TR_Letter> TR_LetterVerifyStatuses { get; set; } = new List<TR_Letter>();

    [InverseProperty("Department")]
    public virtual ICollection<TR_MenuRolePermission> TR_MenuRolePermissionDepartments { get; set; } = new List<TR_MenuRolePermission>();

    [InverseProperty("QCType")]
    public virtual ICollection<TR_MenuRolePermission> TR_MenuRolePermissionQCTypes { get; set; } = new List<TR_MenuRolePermission>();

    [InverseProperty("Day")]
    public virtual ICollection<TR_ProjectAppointLimit_Mapping> TR_ProjectAppointLimit_MappingDays { get; set; } = new List<TR_ProjectAppointLimit_Mapping>();

    [InverseProperty("Time")]
    public virtual ICollection<TR_ProjectAppointLimit_Mapping> TR_ProjectAppointLimit_MappingTimes { get; set; } = new List<TR_ProjectAppointLimit_Mapping>();

    [InverseProperty("QueueType")]
    public virtual ICollection<TR_ProjectCounter_Mapping> TR_ProjectCounter_Mappings { get; set; } = new List<TR_ProjectCounter_Mapping>();

    [InverseProperty("Status")]
    public virtual ICollection<TR_ProjectStatus> TR_ProjectStatuses { get; set; } = new List<TR_ProjectStatus>();

    [InverseProperty("ProjectZone")]
    public virtual ICollection<TR_ProjectZone_Mapping> TR_ProjectZone_Mappings { get; set; } = new List<TR_ProjectZone_Mapping>();

    [InverseProperty("QC_Status")]
    public virtual ICollection<TR_QC1> TR_QC1s { get; set; } = new List<TR_QC1>();

    [InverseProperty("QC_Status")]
    public virtual ICollection<TR_QC2> TR_QC2s { get; set; } = new List<TR_QC2>();

    [InverseProperty("QC_Status")]
    public virtual ICollection<TR_QC3> TR_QC3s { get; set; } = new List<TR_QC3>();

    [InverseProperty("QC_Status")]
    public virtual ICollection<TR_QC4> TR_QC4s { get; set; } = new List<TR_QC4>();

    [InverseProperty("CustRelation")]
    public virtual ICollection<TR_QC5> TR_QC5CustRelations { get; set; } = new List<TR_QC5>();

    [InverseProperty("QC_Status")]
    public virtual ICollection<TR_QC5> TR_QC5QC_Statuses { get; set; } = new List<TR_QC5>();

    [InverseProperty("Answer")]
    public virtual ICollection<TR_QC5_CheckList_Detail> TR_QC5_CheckList_Details { get; set; } = new List<TR_QC5_CheckList_Detail>();

    [InverseProperty("QC_Status")]
    public virtual ICollection<TR_QC5_Open> TR_QC5_Opens { get; set; } = new List<TR_QC5_Open>();

    [InverseProperty("CustRelation")]
    public virtual ICollection<TR_QC6> TR_QC6CustRelations { get; set; } = new List<TR_QC6>();

    [InverseProperty("QC_Status")]
    public virtual ICollection<TR_QC6> TR_QC6QC_Statuses { get; set; } = new List<TR_QC6>();

    [InverseProperty("QCType")]
    public virtual ICollection<TR_QC_CheckList> TR_QC_CheckLists { get; set; } = new List<TR_QC_CheckList>();

    [InverseProperty("EstimateStatus")]
    public virtual ICollection<TR_QC_Defect_OverDueExpect> TR_QC_Defect_OverDueExpects { get; set; } = new List<TR_QC_Defect_OverDueExpect>();

    [InverseProperty("DefectStatus")]
    public virtual ICollection<TR_QC_Defect> TR_QC_Defects { get; set; } = new List<TR_QC_Defect>();

    [InverseProperty("QuestionType")]
    public virtual ICollection<TR_QuestionC> TR_QuestionCs { get; set; } = new List<TR_QuestionC>();

    [InverseProperty("QCType")]
    public virtual ICollection<TR_RegisterLog> TR_RegisterLogs { get; set; } = new List<TR_RegisterLog>();

    [InverseProperty("RemarkUnitStatusCS")]
    public virtual ICollection<TR_RemarkUnitStatus_Mapping> TR_RemarkUnitStatus_MappingRemarkUnitStatusCs { get; set; } = new List<TR_RemarkUnitStatus_Mapping>();

    [InverseProperty("UnitStatusCS")]
    public virtual ICollection<TR_RemarkUnitStatus_Mapping> TR_RemarkUnitStatus_MappingUnitStatusCs { get; set; } = new List<TR_RemarkUnitStatus_Mapping>();

    [InverseProperty("ApproveStatusID_2Navigation")]
    public virtual ICollection<TR_ReviseUnitPromotion> TR_ReviseUnitPromotionApproveStatusID_2Navigations { get; set; } = new List<TR_ReviseUnitPromotion>();

    [InverseProperty("ApproveStatus")]
    public virtual ICollection<TR_ReviseUnitPromotion> TR_ReviseUnitPromotionApproveStatuses { get; set; } = new List<TR_ReviseUnitPromotion>();

    [InverseProperty("ApproveRole")]
    public virtual ICollection<TR_ReviseUnitPromoton_Approver> TR_ReviseUnitPromoton_Approvers { get; set; } = new List<TR_ReviseUnitPromoton_Approver>();

    [InverseProperty("QCType")]
    public virtual ICollection<TR_Sync_QC> TR_Sync_QCs { get; set; } = new List<TR_Sync_QC>();

    [InverseProperty("PlanAmount")]
    public virtual ICollection<TR_TargetRollingPlan> TR_TargetRollingPlanPlanAmounts { get; set; } = new List<TR_TargetRollingPlan>();

    [InverseProperty("PlanType")]
    public virtual ICollection<TR_TargetRollingPlan> TR_TargetRollingPlanPlanTypes { get; set; } = new List<TR_TargetRollingPlan>();

    [InverseProperty("TerminateStatus")]
    public virtual ICollection<TR_TerminateTransferAppoint> TR_TerminateTransferAppoints { get; set; } = new List<TR_TerminateTransferAppoint>();

    [InverseProperty("DocumentType")]
    public virtual ICollection<TR_UnitDocument> TR_UnitDocumentDocumentTypes { get; set; } = new List<TR_UnitDocument>();

    [InverseProperty("DocumentType")]
    public virtual ICollection<TR_UnitDocumentNo> TR_UnitDocumentNoDocumentTypes { get; set; } = new List<TR_UnitDocumentNo>();

    [InverseProperty("QCType")]
    public virtual ICollection<TR_UnitDocumentNo> TR_UnitDocumentNoQCTypes { get; set; } = new List<TR_UnitDocumentNo>();

    [InverseProperty("QCType")]
    public virtual ICollection<TR_UnitDocument> TR_UnitDocumentQCTypes { get; set; } = new List<TR_UnitDocument>();

    [InverseProperty("CheckStatus")]
    public virtual ICollection<TR_UnitFurniture> TR_UnitFurnitures { get; set; } = new List<TR_UnitFurniture>();

    [InverseProperty("ProjectZone")]
    public virtual ICollection<tm_LineToken> tm_LineTokens { get; set; } = new List<tm_LineToken>();

    [InverseProperty("QCType")]
    public virtual ICollection<tm_Menu> tm_Menus { get; set; } = new List<tm_Menu>();

    [InverseProperty("GroupUser")]
    public virtual ICollection<tm_ProjectUser_Mapping> tm_ProjectUser_Mappings { get; set; } = new List<tm_ProjectUser_Mapping>();

    [InverseProperty("Partner")]
    public virtual ICollection<tm_Project> tm_Projects { get; set; } = new List<tm_Project>();

    [InverseProperty("QCType")]
    public virtual ICollection<tm_Responsible_Mapping> tm_Responsible_Mappings { get; set; } = new List<tm_Responsible_Mapping>();

    [InverseProperty("QCType")]
    public virtual ICollection<tm_Role> tm_Roles { get; set; } = new List<tm_Role>();

    [InverseProperty("MeterType")]
    public virtual ICollection<tm_Unit> tm_UnitMeterTypes { get; set; } = new List<tm_Unit>();

    [InverseProperty("UnitStatus_SaleNavigation")]
    public virtual ICollection<tm_Unit> tm_UnitUnitStatus_SaleNavigations { get; set; } = new List<tm_Unit>();

    [InverseProperty("QCType")]
    public virtual ICollection<tm_User> tm_Users { get; set; } = new List<tm_User>();
}
