using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmUnit
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public string? UnitCode { get; set; }

    public string? AddrNo { get; set; }

    public string? Build { get; set; }

    public int? Floor { get; set; }

    public int? Room { get; set; }

    public string? UnitType { get; set; }

    public string? Position { get; set; }

    public decimal? Area { get; set; }

    public int? UnitStatus { get; set; }

    public int? CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerMobile { get; set; }

    public string? CustomerEmail { get; set; }

    public DateTime? BookDate { get; set; }

    public DateTime? ContractDate { get; set; }

    public decimal? SellingPrice { get; set; }

    public int? WipMatrixId { get; set; }

    public int? Qc6WipMatrixId { get; set; }

    public Guid? Qc1Id { get; set; }

    public DateTime? Qc1Date { get; set; }

    public int? Qc1StatusId { get; set; }

    public int? Qc1 { get; set; }

    public Guid? Qc2Id { get; set; }

    public DateTime? Qc2Date { get; set; }

    public int? Qc2StatusId { get; set; }

    public int? Qc2 { get; set; }

    public Guid? Qc3Id { get; set; }

    public DateTime? Qc3Date { get; set; }

    public int? Qc3StatusId { get; set; }

    public int? Qc3 { get; set; }

    public Guid? Qc4Id { get; set; }

    public DateTime? Qc4Date { get; set; }

    public int? Qc4StatusId { get; set; }

    public int? Qc4 { get; set; }

    public Guid? Qc5OpenId { get; set; }

    public DateTime? Qc5OpenDate { get; set; }

    public int? Qc5OpenStatusId { get; set; }

    public int? Qc5Open { get; set; }

    public Guid? Qc5Id { get; set; }

    public DateTime? Qc5Date { get; set; }

    public int? Qc5StatusId { get; set; }

    public int? Qc5 { get; set; }

    public Guid? Qc6Id { get; set; }

    public DateTime? Qc6Date { get; set; }

    public int? Qc6StatusId { get; set; }

    public int? Qc6 { get; set; }

    public int? Qc6Matrix { get; set; }

    public DateTime? AppointDate { get; set; }

    public int? InspectCount { get; set; }

    public Guid? InspectId { get; set; }

    public DateTime? InspectDate { get; set; }

    public int? Defect { get; set; }

    public DateTime? Qc5FinishDate { get; set; }

    public int? Transfer { get; set; }

    public string? ExpectTransfer { get; set; }

    public DateTime? TransferDueDateCs { get; set; }

    public DateTime? TransferDateCs { get; set; }

    public int? UnitStatusCs { get; set; }

    public int? RemarkUnitStatusCs { get; set; }

    public DateTime? LetterDueDateCs { get; set; }

    public DateTime? UpdateDateUnitStatusCs { get; set; }

    public int? BankSelectedIdCs { get; set; }

    public DateTime? TransferDate { get; set; }

    public DateTime? ReceiveRoomDate { get; set; }

    public DateTime? ReceiveRoomAgreementDate { get; set; }

    public int? ExpectTransferById { get; set; }

    public int? MeterTypeId { get; set; }

    public string? SaleName { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public int? DeviateStatusId { get; set; }

    public string? DeviateRemark { get; set; }

    public int? OverDuePeriod { get; set; }

    public decimal? OverDueAmount { get; set; }

    public decimal? FreeAll { get; set; }

    public decimal? TransferNetPrice { get; set; }

    public DateTime? CardExpireDate { get; set; }

    public string? MeterRemark { get; set; }

    public int? UnitStatusSale { get; set; }

    public string? ExpectTransferDeviate { get; set; }

    public virtual ICollection<LineUserContract> LineUserContracts { get; set; } = new List<LineUserContract>();

    public virtual TmExt? MeterType { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual ICollection<TrAppointment> TrAppointments { get; set; } = new List<TrAppointment>();

    public virtual ICollection<TrAttachFileQc> TrAttachFileQcs { get; set; } = new List<TrAttachFileQc>();

    public virtual ICollection<TrContactLog> TrContactLogs { get; set; } = new List<TrContactLog>();

    public virtual ICollection<TrLetter> TrLetters { get; set; } = new List<TrLetter>();

    public virtual ICollection<TrProjectUnitFloorPlan> TrProjectUnitFloorPlans { get; set; } = new List<TrProjectUnitFloorPlan>();

    public virtual ICollection<TrQc1> TrQc1s { get; set; } = new List<TrQc1>();

    public virtual ICollection<TrQc2> TrQc2s { get; set; } = new List<TrQc2>();

    public virtual ICollection<TrQc3> TrQc3s { get; set; } = new List<TrQc3>();

    public virtual ICollection<TrQc4> TrQc4s { get; set; } = new List<TrQc4>();

    public virtual ICollection<TrQc5CheckList> TrQc5CheckLists { get; set; } = new List<TrQc5CheckList>();

    public virtual ICollection<TrQc5Open> TrQc5Opens { get; set; } = new List<TrQc5Open>();

    public virtual ICollection<TrQc5> TrQc5s { get; set; } = new List<TrQc5>();

    public virtual ICollection<TrQc6Unsold> TrQc6Unsolds { get; set; } = new List<TrQc6Unsold>();

    public virtual ICollection<TrQc6> TrQc6s { get; set; } = new List<TrQc6>();

    public virtual ICollection<TrQcCheckList> TrQcCheckLists { get; set; } = new List<TrQcCheckList>();

    public virtual ICollection<TrQcContactLog> TrQcContactLogs { get; set; } = new List<TrQcContactLog>();

    public virtual ICollection<TrQcDefect> TrQcDefects { get; set; } = new List<TrQcDefect>();

    public virtual ICollection<TrReceiveRoomAgreementSign> TrReceiveRoomAgreementSigns { get; set; } = new List<TrReceiveRoomAgreementSign>();

    public virtual ICollection<TrReceiveRoomSign> TrReceiveRoomSigns { get; set; } = new List<TrReceiveRoomSign>();

    public virtual ICollection<TrRegisterLog> TrRegisterLogs { get; set; } = new List<TrRegisterLog>();

    public virtual ICollection<TrReviseUnitPromotion> TrReviseUnitPromotions { get; set; } = new List<TrReviseUnitPromotion>();

    public virtual ICollection<TrSyncQc> TrSyncQcs { get; set; } = new List<TrSyncQc>();

    public virtual ICollection<TrSync> TrSyncs { get; set; } = new List<TrSync>();

    public virtual ICollection<TrTerminateTransferAppoint> TrTerminateTransferAppoints { get; set; } = new List<TrTerminateTransferAppoint>();

    public virtual ICollection<TrTransferDocument> TrTransferDocuments { get; set; } = new List<TrTransferDocument>();

    public virtual ICollection<TrUnitDocumentNo> TrUnitDocumentNos { get; set; } = new List<TrUnitDocumentNo>();

    public virtual ICollection<TrUnitDocument> TrUnitDocuments { get; set; } = new List<TrUnitDocument>();

    public virtual ICollection<TrUnitEquipment> TrUnitEquipments { get; set; } = new List<TrUnitEquipment>();

    public virtual ICollection<TrUnitEvent> TrUnitEvents { get; set; } = new List<TrUnitEvent>();

    public virtual ICollection<TrUnitFurniture> TrUnitFurnitures { get; set; } = new List<TrUnitFurniture>();

    public virtual ICollection<TrUnitPromotionSign> TrUnitPromotionSigns { get; set; } = new List<TrUnitPromotionSign>();

    public virtual ICollection<TrUnitPromotion> TrUnitPromotions { get; set; } = new List<TrUnitPromotion>();

    public virtual ICollection<TrUnsoldRoundUnit> TrUnsoldRoundUnits { get; set; } = new List<TrUnsoldRoundUnit>();

    public virtual TmUnitStatus? UnitStatusNavigation { get; set; }

    public virtual TmExt? UnitStatusSaleNavigation { get; set; }

    public virtual TmWipmatrix? WipMatrix { get; set; }
}
