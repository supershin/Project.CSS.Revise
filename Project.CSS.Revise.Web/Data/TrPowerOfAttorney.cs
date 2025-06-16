using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrPowerOfAttorney
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public DateTime? SignDate { get; set; }

    public Guid? SignResourceId { get; set; }

    public Guid? SignResourceId2 { get; set; }

    public Guid? IdcardResourceId { get; set; }

    public Guid? IdcardResourceId2 { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }
}
