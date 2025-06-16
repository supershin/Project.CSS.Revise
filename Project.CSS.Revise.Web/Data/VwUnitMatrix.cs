using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class VwUnitMatrix
{
    public string? ProjectId { get; set; }

    public Guid Id { get; set; }

    public string? UnitCode { get; set; }

    public string? Build { get; set; }

    public int? Floor { get; set; }

    public int? Room { get; set; }

    public string? CustomerName { get; set; }

    public DateTime? Qc5FinishDate { get; set; }

    public DateTime? AppointDate { get; set; }

    public DateTime? LastInspectDate { get; set; }

    public int? CntInspect { get; set; }

    public string? InspectStatus { get; set; }

    public DateTime? ReceiveRoomDate { get; set; }

    public DateTime? TransferDate { get; set; }

    public int? DefectZeroType { get; set; }

    public int? ExpectTransferById { get; set; }

    public int? UnitStatusCs { get; set; }

    public string? ExpectTransfer { get; set; }

    public decimal? SellingPrice { get; set; }

    public int MatrixTypeId { get; set; }

    public string? Bgcolor { get; set; }

    public string? MatrixTypeName { get; set; }

    public string? Prefix { get; set; }

    public int? MatrixTypeLineOrder { get; set; }

    public string TagTransfer { get; set; } = null!;
}
