using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrReviseUnitPromotonApprover
{
    public int Id { get; set; }

    public int? ApproveRoleId { get; set; }

    public int? UserId { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmExt? ApproveRole { get; set; }

    public virtual TmUser? User { get; set; }
}
