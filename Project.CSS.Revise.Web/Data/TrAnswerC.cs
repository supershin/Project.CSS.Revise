using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrAnswerC
{
    public int Id { get; set; }

    public int? QuestionId { get; set; }

    public string? AnswerName { get; set; }

    public string? ImageUrl { get; set; }

    public bool? FlagOtherText { get; set; }

    public int? Revision { get; set; }

    public int? Score { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TrQuestionC? Question { get; set; }

    public virtual ICollection<TrCustomerSatisfactionDetail> TrCustomerSatisfactionDetails { get; set; } = new List<TrCustomerSatisfactionDetail>();
}
