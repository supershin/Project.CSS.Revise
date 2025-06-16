using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmQuestion
{
    public int Id { get; set; }

    public string? QuestionName { get; set; }

    public bool? FlagMulti { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<TmAnswer> TmAnswers { get; set; } = new List<TmAnswer>();

    public virtual ICollection<TrQuestionAnswer> TrQuestionAnswers { get; set; } = new List<TrQuestionAnswer>();
}
