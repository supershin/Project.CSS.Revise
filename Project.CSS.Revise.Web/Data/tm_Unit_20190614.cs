using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("tm_Unit_20190614")]
public partial class tm_Unit_20190614
{
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

    [Column(TypeName = "datetime")]
    public DateTime? TransferDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReceiveRoomDate { get; set; }

    public int? ExpectTransferByID { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }
}
