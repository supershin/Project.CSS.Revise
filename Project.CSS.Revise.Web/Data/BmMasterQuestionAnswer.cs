using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class BmMasterQuestionAnswer
{
    public int Id { get; set; }

    public int? QuestionId { get; set; }

    public string? Name { get; set; }

    public decimal? CompareFrom { get; set; }

    public decimal? CompareTo { get; set; }

    public string? Color { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<BmMasterSetQuestionAnswer> BmMasterSetQuestionAnswers { get; set; } = new List<BmMasterSetQuestionAnswer>();

    public virtual ICollection<BmTrQuestionAnswerScore> BmTrQuestionAnswerScores { get; set; } = new List<BmTrQuestionAnswerScore>();

    public virtual ICollection<BmTsMatchingQuestionAnswer> BmTsMatchingQuestionAnswers { get; set; } = new List<BmTsMatchingQuestionAnswer>();

    public virtual ICollection<BmTsMatchingScoreSetDetail> BmTsMatchingScoreSetDetails { get; set; } = new List<BmTsMatchingScoreSetDetail>();

    public virtual BmMasterQuestion? Question { get; set; }
}
