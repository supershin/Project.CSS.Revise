using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Project")]
public partial class tm_Project
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string ProjectID { get; set; } = null!;

    public int? PartnerID { get; set; }

    [StringLength(500)]
    public string? ProjectName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? ProjectName_Eng { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? ProjectType { get; set; }

    [StringLength(1000)]
    public string? InterestRateUrl { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<Line_UserContract> Line_UserContracts { get; set; } = new List<Line_UserContract>();

    [InverseProperty("Project")]
    public virtual ICollection<PR_Loan> PR_Loans { get; set; } = new List<PR_Loan>();

    [ForeignKey("PartnerID")]
    [InverseProperty("tm_Projects")]
    public virtual tm_Ext? Partner { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<TR_Appointment> TR_Appointments { get; set; } = new List<TR_Appointment>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_AttachFileQC> TR_AttachFileQCs { get; set; } = new List<TR_AttachFileQC>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_CompanyProject> TR_CompanyProjects { get; set; } = new List<TR_CompanyProject>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_ContactLog> TR_ContactLogs { get; set; } = new List<TR_ContactLog>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_CustomerSatisfaction> TR_CustomerSatisfactions { get; set; } = new List<TR_CustomerSatisfaction>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_Letter> TR_Letters { get; set; } = new List<TR_Letter>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_ProjectAppointLimit_Mapping> TR_ProjectAppointLimit_Mappings { get; set; } = new List<TR_ProjectAppointLimit_Mapping>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_ProjectCounter_Mapping> TR_ProjectCounter_Mappings { get; set; } = new List<TR_ProjectCounter_Mapping>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_ProjectEmail_Mapping> TR_ProjectEmail_Mappings { get; set; } = new List<TR_ProjectEmail_Mapping>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_ProjectFloorPlan> TR_ProjectFloorPlans { get; set; } = new List<TR_ProjectFloorPlan>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_ProjectLandOffice> TR_ProjectLandOffices { get; set; } = new List<TR_ProjectLandOffice>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_ProjectShopEvent> TR_ProjectShopEvents { get; set; } = new List<TR_ProjectShopEvent>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_ProjectStatus> TR_ProjectStatuses { get; set; } = new List<TR_ProjectStatus>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_ProjectUnitFloorPlan> TR_ProjectUnitFloorPlans { get; set; } = new List<TR_ProjectUnitFloorPlan>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_ProjectUserSign> TR_ProjectUserSigns { get; set; } = new List<TR_ProjectUserSign>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QC1> TR_QC1s { get; set; } = new List<TR_QC1>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QC2> TR_QC2s { get; set; } = new List<TR_QC2>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QC3> TR_QC3s { get; set; } = new List<TR_QC3>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QC4> TR_QC4s { get; set; } = new List<TR_QC4>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QC5_CheckList> TR_QC5_CheckLists { get; set; } = new List<TR_QC5_CheckList>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QC5_Open> TR_QC5_Opens { get; set; } = new List<TR_QC5_Open>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QC5_ProjectSendMail> TR_QC5_ProjectSendMails { get; set; } = new List<TR_QC5_ProjectSendMail>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QC5> TR_QC5s { get; set; } = new List<TR_QC5>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QC6_Unsold_SendMail> TR_QC6_Unsold_SendMails { get; set; } = new List<TR_QC6_Unsold_SendMail>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QC6_Unsold> TR_QC6_Unsolds { get; set; } = new List<TR_QC6_Unsold>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QC6> TR_QC6s { get; set; } = new List<TR_QC6>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QC_CheckList> TR_QC_CheckLists { get; set; } = new List<TR_QC_CheckList>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QC_ContactLog> TR_QC_ContactLogs { get; set; } = new List<TR_QC_ContactLog>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QC_Defect> TR_QC_Defects { get; set; } = new List<TR_QC_Defect>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_QuestionAnswer> TR_QuestionAnswers { get; set; } = new List<TR_QuestionAnswer>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_ReceiveRoomAgreementSign> TR_ReceiveRoomAgreementSigns { get; set; } = new List<TR_ReceiveRoomAgreementSign>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_ReceiveRoomSign> TR_ReceiveRoomSigns { get; set; } = new List<TR_ReceiveRoomSign>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_RegisterLog> TR_RegisterLogs { get; set; } = new List<TR_RegisterLog>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_Register_ProjectBankStaff> TR_Register_ProjectBankStaffs { get; set; } = new List<TR_Register_ProjectBankStaff>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_ReviseUnitPromotion> TR_ReviseUnitPromotions { get; set; } = new List<TR_ReviseUnitPromotion>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_Sync_Log> TR_Sync_Logs { get; set; } = new List<TR_Sync_Log>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_Sync_QC> TR_Sync_QCs { get; set; } = new List<TR_Sync_QC>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_Sync> TR_Syncs { get; set; } = new List<TR_Sync>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_TargetRollingPlan> TR_TargetRollingPlans { get; set; } = new List<TR_TargetRollingPlan>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_TransferDocument> TR_TransferDocuments { get; set; } = new List<TR_TransferDocument>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_UnitDocumentNo> TR_UnitDocumentNos { get; set; } = new List<TR_UnitDocumentNo>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_UnitDocument> TR_UnitDocuments { get; set; } = new List<TR_UnitDocument>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_UnitEquipment> TR_UnitEquipments { get; set; } = new List<TR_UnitEquipment>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_UnitFurniture> TR_UnitFurnitures { get; set; } = new List<TR_UnitFurniture>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_Unsold_RoundUnit> TR_Unsold_RoundUnits { get; set; } = new List<TR_Unsold_RoundUnit>();

    [InverseProperty("Project")]
    public virtual ICollection<TR_Unsold_Round> TR_Unsold_Rounds { get; set; } = new List<TR_Unsold_Round>();

    [InverseProperty("Project")]
    public virtual ICollection<tm_BUProject_Mapping> tm_BUProject_Mappings { get; set; } = new List<tm_BUProject_Mapping>();

    [InverseProperty("Project")]
    public virtual ICollection<tm_Event> tm_Events { get; set; } = new List<tm_Event>();

    [InverseProperty("Project")]
    public virtual ICollection<tm_ProjectUser_Mapping> tm_ProjectUser_Mappings { get; set; } = new List<tm_ProjectUser_Mapping>();

    [InverseProperty("Project")]
    public virtual ICollection<tm_Responsible_Mapping> tm_Responsible_Mappings { get; set; } = new List<tm_Responsible_Mapping>();

    [InverseProperty("Project")]
    public virtual ICollection<tm_Unit> tm_Units { get; set; } = new List<tm_Unit>();
}
