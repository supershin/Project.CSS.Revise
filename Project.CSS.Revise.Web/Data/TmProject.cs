using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmProject
{
    public string ProjectId { get; set; } = null!;

    public int? PartnerId { get; set; }

    public string? ProjectName { get; set; }

    public string? ProjectNameEng { get; set; }

    public string? ProjectType { get; set; }

    public string? InterestRateUrl { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<LineUserContract> LineUserContracts { get; set; } = new List<LineUserContract>();

    public virtual TmExt? Partner { get; set; }

    public virtual ICollection<PrLoan> PrLoans { get; set; } = new List<PrLoan>();

    public virtual ICollection<TmBuprojectMapping> TmBuprojectMappings { get; set; } = new List<TmBuprojectMapping>();

    public virtual ICollection<TmEvent> TmEvents { get; set; } = new List<TmEvent>();

    public virtual ICollection<TmProjectUserMapping> TmProjectUserMappings { get; set; } = new List<TmProjectUserMapping>();

    public virtual ICollection<TmResponsibleMapping> TmResponsibleMappings { get; set; } = new List<TmResponsibleMapping>();

    public virtual ICollection<TmUnit> TmUnits { get; set; } = new List<TmUnit>();

    public virtual ICollection<TrAppointment> TrAppointments { get; set; } = new List<TrAppointment>();

    public virtual ICollection<TrAttachFileQc> TrAttachFileQcs { get; set; } = new List<TrAttachFileQc>();

    public virtual ICollection<TrCompanyProject> TrCompanyProjects { get; set; } = new List<TrCompanyProject>();

    public virtual ICollection<TrContactLog> TrContactLogs { get; set; } = new List<TrContactLog>();

    public virtual ICollection<TrCustomerSatisfaction> TrCustomerSatisfactions { get; set; } = new List<TrCustomerSatisfaction>();

    public virtual ICollection<TrLetter> TrLetters { get; set; } = new List<TrLetter>();

    public virtual ICollection<TrProjectAppointLimitMapping> TrProjectAppointLimitMappings { get; set; } = new List<TrProjectAppointLimitMapping>();

    public virtual ICollection<TrProjectCounterMapping> TrProjectCounterMappings { get; set; } = new List<TrProjectCounterMapping>();

    public virtual ICollection<TrProjectEmailMapping> TrProjectEmailMappings { get; set; } = new List<TrProjectEmailMapping>();

    public virtual ICollection<TrProjectFloorPlan> TrProjectFloorPlans { get; set; } = new List<TrProjectFloorPlan>();

    public virtual ICollection<TrProjectLandOffice> TrProjectLandOffices { get; set; } = new List<TrProjectLandOffice>();

    public virtual ICollection<TrProjectShopEvent> TrProjectShopEvents { get; set; } = new List<TrProjectShopEvent>();

    public virtual ICollection<TrProjectStatus> TrProjectStatuses { get; set; } = new List<TrProjectStatus>();

    public virtual ICollection<TrProjectUnitFloorPlan> TrProjectUnitFloorPlans { get; set; } = new List<TrProjectUnitFloorPlan>();

    public virtual ICollection<TrProjectUserSign> TrProjectUserSigns { get; set; } = new List<TrProjectUserSign>();

    public virtual ICollection<TrQc1> TrQc1s { get; set; } = new List<TrQc1>();

    public virtual ICollection<TrQc2> TrQc2s { get; set; } = new List<TrQc2>();

    public virtual ICollection<TrQc3> TrQc3s { get; set; } = new List<TrQc3>();

    public virtual ICollection<TrQc4> TrQc4s { get; set; } = new List<TrQc4>();

    public virtual ICollection<TrQc5CheckList> TrQc5CheckLists { get; set; } = new List<TrQc5CheckList>();

    public virtual ICollection<TrQc5Open> TrQc5Opens { get; set; } = new List<TrQc5Open>();

    public virtual ICollection<TrQc5ProjectSendMail> TrQc5ProjectSendMails { get; set; } = new List<TrQc5ProjectSendMail>();

    public virtual ICollection<TrQc5> TrQc5s { get; set; } = new List<TrQc5>();

    public virtual ICollection<TrQc6UnsoldSendMail> TrQc6UnsoldSendMails { get; set; } = new List<TrQc6UnsoldSendMail>();

    public virtual ICollection<TrQc6Unsold> TrQc6Unsolds { get; set; } = new List<TrQc6Unsold>();

    public virtual ICollection<TrQc6> TrQc6s { get; set; } = new List<TrQc6>();

    public virtual ICollection<TrQcCheckList> TrQcCheckLists { get; set; } = new List<TrQcCheckList>();

    public virtual ICollection<TrQcContactLog> TrQcContactLogs { get; set; } = new List<TrQcContactLog>();

    public virtual ICollection<TrQcDefect> TrQcDefects { get; set; } = new List<TrQcDefect>();

    public virtual ICollection<TrQuestionAnswer> TrQuestionAnswers { get; set; } = new List<TrQuestionAnswer>();

    public virtual ICollection<TrReceiveRoomAgreementSign> TrReceiveRoomAgreementSigns { get; set; } = new List<TrReceiveRoomAgreementSign>();

    public virtual ICollection<TrReceiveRoomSign> TrReceiveRoomSigns { get; set; } = new List<TrReceiveRoomSign>();

    public virtual ICollection<TrRegisterLog> TrRegisterLogs { get; set; } = new List<TrRegisterLog>();

    public virtual ICollection<TrRegisterProjectBankStaff> TrRegisterProjectBankStaffs { get; set; } = new List<TrRegisterProjectBankStaff>();

    public virtual ICollection<TrReviseUnitPromotion> TrReviseUnitPromotions { get; set; } = new List<TrReviseUnitPromotion>();

    public virtual ICollection<TrSyncLog> TrSyncLogs { get; set; } = new List<TrSyncLog>();

    public virtual ICollection<TrSyncQc> TrSyncQcs { get; set; } = new List<TrSyncQc>();

    public virtual ICollection<TrSync> TrSyncs { get; set; } = new List<TrSync>();

    public virtual ICollection<TrTargetRollingPlan> TrTargetRollingPlans { get; set; } = new List<TrTargetRollingPlan>();

    public virtual ICollection<TrTransferDocument> TrTransferDocuments { get; set; } = new List<TrTransferDocument>();

    public virtual ICollection<TrUnitDocumentNo> TrUnitDocumentNos { get; set; } = new List<TrUnitDocumentNo>();

    public virtual ICollection<TrUnitDocument> TrUnitDocuments { get; set; } = new List<TrUnitDocument>();

    public virtual ICollection<TrUnitEquipment> TrUnitEquipments { get; set; } = new List<TrUnitEquipment>();

    public virtual ICollection<TrUnitFurniture> TrUnitFurnitures { get; set; } = new List<TrUnitFurniture>();

    public virtual ICollection<TrUnsoldRoundUnit> TrUnsoldRoundUnits { get; set; } = new List<TrUnsoldRoundUnit>();

    public virtual ICollection<TrUnsoldRound> TrUnsoldRounds { get; set; } = new List<TrUnsoldRound>();
}
