using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrLetterC
{
    public Guid Id { get; set; }

    public int? EventId { get; set; }

    public string? EventLocation { get; set; }

    public DateTime? EventStartDate { get; set; }

    public DateTime? EventEndDate { get; set; }

    public string? CodeVerify { get; set; }

    public decimal? PercentProgress { get; set; }

    public string? ExpectTransfer { get; set; }

    public DateTime? FinplusStartDate { get; set; }

    public DateTime? FinplusEndDate { get; set; }

    public string? Promotion { get; set; }

    public decimal? ElectricMeter { get; set; }

    public decimal? WaterMaintainCost { get; set; }

    public decimal? CentralValue { get; set; }

    public DateTime? InspectLastDate { get; set; }

    public DateTime? ExpectLastDate { get; set; }

    public int? SignUserId { get; set; }

    public string? SignUserPosition { get; set; }

    public virtual TmEvent? Event { get; set; }

    public virtual TmUser? SignUser { get; set; }
}
