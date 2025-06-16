using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class BmTsMatchingQuestionAnswer
{
    public long Id { get; set; }

    public Guid? MatchingId { get; set; }

    public int? QuestionId { get; set; }

    public int? AnswerId { get; set; }

    public int? BankId { get; set; }

    public int? Hdp { get; set; }

    public int? Credit { get; set; }

    public int? Debt { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual BmMasterQuestionAnswer? Answer { get; set; }

    public virtual TmBank? Bank { get; set; }

    public virtual BmTsMatching? Matching { get; set; }

    public virtual BmMasterQuestion? Question { get; set; }
}
