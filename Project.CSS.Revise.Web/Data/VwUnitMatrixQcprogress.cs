using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class VwUnitMatrixQcprogress
{
    public string? ProjectId { get; set; }

    public Guid Id { get; set; }

    public string? UnitCode { get; set; }

    public string? Build { get; set; }

    public int? Floor { get; set; }

    public int? Room { get; set; }

    public string? CustomerName { get; set; }

    public DateTime? Qc5FinishDate { get; set; }

    public DateTime? LastQc5date { get; set; }

    public DateTime? LastQc6date { get; set; }

    public int? UnitStatusCs { get; set; }

    public DateTime? ReceiveRoomDate { get; set; }

    public DateTime? TransferDate { get; set; }

    public int VerifyQc5CheckLis { get; set; }

    public int MatrixTypeId { get; set; }

    public string? Bgcolor { get; set; }

    public string? MatrixTypeName { get; set; }

    public int? MatrixTypeLineOrder { get; set; }
}
