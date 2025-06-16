using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrCustomerSatisfaction
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public int? UserId { get; set; }

    public int? QuestionTypeId { get; set; }

    public DateTime? QuestionDate { get; set; }

    public string? UnitCode { get; set; }

    public string? Remark { get; set; }

    public string? ClientIp { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TmExt? QuestionType { get; set; }

    public virtual ICollection<TrCustomerSatisfactionDetail> TrCustomerSatisfactionDetails { get; set; } = new List<TrCustomerSatisfactionDetail>();

    public virtual TmUser? User { get; set; }
}
