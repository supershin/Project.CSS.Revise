using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class UnitFixdone
{
    public string? ProjectId { get; set; }

    public Guid Id { get; set; }

    public string? UnitCode { get; set; }

    public string? CustomerName { get; set; }

    public int? CntInspect { get; set; }

    public string? InspectStatus { get; set; }

    public DateTime? ReceiveRoomDate { get; set; }

    public DateTime? TransferDate { get; set; }
}
