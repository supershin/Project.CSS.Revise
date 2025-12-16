using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
public partial class vw_UnitMatrix
{
    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? UnitCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Build { get; set; }

    public int? Floor { get; set; }

    public int? Room { get; set; }

    [StringLength(500)]
    public string? CustomerName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QC5_FinishDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AppointDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastInspectDate { get; set; }

    public int? CntInspect { get; set; }

    [Unicode(false)]
    public string? InspectStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReceiveRoomDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransferDate { get; set; }

    public int? DefectZeroType { get; set; }

    public int? ExpectTransferByID { get; set; }

    public int? UnitStatus_CS { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ExpectTransfer { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SellingPrice { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TransferNetPrice { get; set; }

    public int MatrixTypeID { get; set; }

    [Unicode(false)]
    public string? BGColor { get; set; }

    [StringLength(200)]
    public string? MatrixTypeName { get; set; }

    [StringLength(67)]
    [Unicode(false)]
    public string? Prefix { get; set; }

    public int? MatrixTypeLineOrder { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string TagTransfer { get; set; } = null!;
}
