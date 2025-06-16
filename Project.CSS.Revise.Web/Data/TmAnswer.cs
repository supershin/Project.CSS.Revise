using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmAnswer
{
    public int Id { get; set; }

    public int? QuestionId { get; set; }

    public string? AnswerName { get; set; }

    public bool? FlagOtherText { get; set; }

    public string? OtherText { get; set; }

    public string? OtherTextAfter { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmQuestion? Question { get; set; }

    public virtual ICollection<TrQuestionAnswer> TrQuestionAnswers { get; set; } = new List<TrQuestionAnswer>();
}
