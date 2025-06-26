using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Unit")]
[Index("ProjectID", "UnitCode", "ID", "Build", "Floor", "UnitStatus", "WIP_Matrix_ID", "QC6_WIP_Matrix_ID", "ExpectTransfer", "TransferDueDate_CS", "UnitStatus_CS", "LetterDueDate_CS", "BankSelectedID_CS", "ExpectTransferByID", Name = "NonClusteredIndex-20201108-164116")]
public partial class tm_Unit
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? UnitCode { get; set; }

    [StringLength(500)]
    public string? AddrNo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Build { get; set; }

    public int? Floor { get; set; }

    public int? Room { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? UnitType { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Position { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Area { get; set; }

    public int? UnitStatus { get; set; }

    public int? CustomerID { get; set; }

    [StringLength(500)]
    public string? CustomerName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? CustomerMobile { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? CustomerEmail { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BookDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ContractDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SellingPrice { get; set; }

    public int? WIP_Matrix_ID { get; set; }

    public int? QC6_WIP_Matrix_ID { get; set; }

    public Guid? QC1_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QC1_Date { get; set; }

    public int? QC1_StatusID { get; set; }

    public int? QC1 { get; set; }

    public Guid? QC2_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QC2_Date { get; set; }

    public int? QC2_StatusID { get; set; }

    public int? QC2 { get; set; }

    public Guid? QC3_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QC3_Date { get; set; }

    public int? QC3_StatusID { get; set; }

    public int? QC3 { get; set; }

    public Guid? QC4_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QC4_Date { get; set; }

    public int? QC4_StatusID { get; set; }

    public int? QC4 { get; set; }

    public Guid? QC5_Open_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QC5_Open_Date { get; set; }

    public int? QC5_Open_StatusID { get; set; }

    public int? QC5_Open { get; set; }

    public Guid? QC5_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QC5_Date { get; set; }

    public int? QC5_StatusID { get; set; }

    public int? QC5 { get; set; }

    public Guid? QC6_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QC6_Date { get; set; }

    public int? QC6_StatusID { get; set; }

    public int? QC6 { get; set; }

    public int? QC6_Matrix { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Appoint_Date { get; set; }

    public int? Inspect_Count { get; set; }

    public Guid? Inspect_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Inspect_Date { get; set; }

    public int? Defect { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QC5_FinishDate { get; set; }

    public int? Transfer { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ExpectTransfer { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransferDueDate_CS { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransferDate_CS { get; set; }

    public int? UnitStatus_CS { get; set; }

    public int? RemarkUnitStatus_CS { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LetterDueDate_CS { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDateUnitStatus_CS { get; set; }

    public int? BankSelectedID_CS { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransferDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReceiveRoomDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReceiveRoomAgreementDate { get; set; }

    public int? ExpectTransferByID { get; set; }

    public int? MeterTypeID { get; set; }

    [StringLength(500)]
    public string? SaleName { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public int? DeviateStatusID { get; set; }

    [StringLength(2000)]
    [Unicode(false)]
    public string? DeviateRemark { get; set; }

    public int? OverDuePeriod { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? OverDueAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? FreeAll { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TransferNetPrice { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CardExpireDate { get; set; }

    [StringLength(2000)]
    public string? MeterRemark { get; set; }

    public int? UnitStatus_Sale { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ExpectTransferDeviate { get; set; }

    [InverseProperty("Unit")]
    public virtual ICollection<Line_UserContract> Line_UserContracts { get; set; } = new List<Line_UserContract>();

    [ForeignKey("MeterTypeID")]
    [InverseProperty("tm_UnitMeterTypes")]
    public virtual tm_Ext? MeterType { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("tm_Units")]
    public virtual tm_Project? Project { get; set; }

    [InverseProperty("Unit")]
    public virtual ICollection<TR_Appointment> TR_Appointments { get; set; } = new List<TR_Appointment>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_AttachFileQC> TR_AttachFileQCs { get; set; } = new List<TR_AttachFileQC>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_ContactLog> TR_ContactLogs { get; set; } = new List<TR_ContactLog>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_Letter> TR_Letters { get; set; } = new List<TR_Letter>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_ProjectUnitFloorPlan> TR_ProjectUnitFloorPlans { get; set; } = new List<TR_ProjectUnitFloorPlan>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_QC1> TR_QC1s { get; set; } = new List<TR_QC1>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_QC2> TR_QC2s { get; set; } = new List<TR_QC2>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_QC3> TR_QC3s { get; set; } = new List<TR_QC3>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_QC4> TR_QC4s { get; set; } = new List<TR_QC4>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_QC5_CheckList> TR_QC5_CheckLists { get; set; } = new List<TR_QC5_CheckList>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_QC5_Open> TR_QC5_Opens { get; set; } = new List<TR_QC5_Open>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_QC5> TR_QC5s { get; set; } = new List<TR_QC5>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_QC6_Unsold> TR_QC6_Unsolds { get; set; } = new List<TR_QC6_Unsold>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_QC6> TR_QC6s { get; set; } = new List<TR_QC6>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_QC_CheckList> TR_QC_CheckLists { get; set; } = new List<TR_QC_CheckList>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_QC_Defect> TR_QC_Defects { get; set; } = new List<TR_QC_Defect>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_ReceiveRoomAgreementSign> TR_ReceiveRoomAgreementSigns { get; set; } = new List<TR_ReceiveRoomAgreementSign>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_ReceiveRoomSign> TR_ReceiveRoomSigns { get; set; } = new List<TR_ReceiveRoomSign>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_RegisterLog> TR_RegisterLogs { get; set; } = new List<TR_RegisterLog>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_ReviseUnitPromotion> TR_ReviseUnitPromotions { get; set; } = new List<TR_ReviseUnitPromotion>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_Sync_QC> TR_Sync_QCs { get; set; } = new List<TR_Sync_QC>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_Sync> TR_Syncs { get; set; } = new List<TR_Sync>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_TerminateTransferAppoint> TR_TerminateTransferAppoints { get; set; } = new List<TR_TerminateTransferAppoint>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_TransferDocument> TR_TransferDocuments { get; set; } = new List<TR_TransferDocument>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_UnitDocumentNo> TR_UnitDocumentNos { get; set; } = new List<TR_UnitDocumentNo>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_UnitDocument> TR_UnitDocuments { get; set; } = new List<TR_UnitDocument>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_UnitEquipment> TR_UnitEquipments { get; set; } = new List<TR_UnitEquipment>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_UnitEvent> TR_UnitEvents { get; set; } = new List<TR_UnitEvent>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_UnitFurniture> TR_UnitFurnitures { get; set; } = new List<TR_UnitFurniture>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_UnitPromotionSign> TR_UnitPromotionSigns { get; set; } = new List<TR_UnitPromotionSign>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_UnitPromotion> TR_UnitPromotions { get; set; } = new List<TR_UnitPromotion>();

    [InverseProperty("Unit")]
    public virtual ICollection<TR_Unsold_RoundUnit> TR_Unsold_RoundUnits { get; set; } = new List<TR_Unsold_RoundUnit>();

    [ForeignKey("UnitStatus")]
    [InverseProperty("tm_Units")]
    public virtual tm_UnitStatus? UnitStatusNavigation { get; set; }

    [ForeignKey("UnitStatus_Sale")]
    [InverseProperty("tm_UnitUnitStatus_SaleNavigations")]
    public virtual tm_Ext? UnitStatus_SaleNavigation { get; set; }

    [ForeignKey("WIP_Matrix_ID")]
    [InverseProperty("tm_Units")]
    public virtual tm_WIPMatrix? WIP_Matrix { get; set; }
}
