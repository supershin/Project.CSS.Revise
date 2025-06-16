using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrQc6Unsold
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public DateTime? QcDate { get; set; }

    public string? QcTime { get; set; }

    public Guid? SignResourceId { get; set; }

    public DateTime? CloseCaseDate { get; set; }

    public int? CloseCaseBy { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? ClearDate { get; set; }

    public int? ClearBy { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TrSignResource? SignResource { get; set; }

    public virtual TmUnit? Unit { get; set; }
}
