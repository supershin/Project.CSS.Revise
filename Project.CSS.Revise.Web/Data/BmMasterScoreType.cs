using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class BmMasterScoreType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Score { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<BmTrQuestionAnswerScore> BmTrQuestionAnswerScores { get; set; } = new List<BmTrQuestionAnswerScore>();

    public virtual ICollection<BmTrSetScore> BmTrSetScores { get; set; } = new List<BmTrSetScore>();
}
