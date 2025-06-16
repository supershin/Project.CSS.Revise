using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrTransferAppointHistory
{
    public int Id { get; set; }

    public string? Bu { get; set; }

    public string? ProjectId { get; set; }

    public int? TransferUnit { get; set; }

    public decimal? TransferValue { get; set; }

    public int? TransferAppointUnit { get; set; }

    public decimal? TransferAppointValue { get; set; }

    public DateTime CreateDate { get; set; }
}
