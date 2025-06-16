using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class LineUserContract
{
    public Guid Id { get; set; }

    public string? LineUserId { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public string? ContractNumber { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreatBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual LineUser? LineUser { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TmUnit? Unit { get; set; }
}
