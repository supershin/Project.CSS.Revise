using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrCustomerSatisfactionDetail
{
    public int Id { get; set; }

    public int? CustomerSatisfactionId { get; set; }

    public int? QuestionId { get; set; }

    public int? AnswerId { get; set; }

    public string? OtherText { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TrAnswerC? Answer { get; set; }

    public virtual TrCustomerSatisfaction? CustomerSatisfaction { get; set; }

    public virtual TrQuestionC? Question { get; set; }
}
