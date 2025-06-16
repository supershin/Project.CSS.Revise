using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class BmTsMatching
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? Age { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public DateTime? MatchingDate { get; set; }

    public decimal? LoanAmount { get; set; }

    public decimal? IncomeTotal { get; set; }

    public decimal? DebtTotal { get; set; }

    public bool? FlagAccept { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<BmTsMatchingDetail> BmTsMatchingDetails { get; set; } = new List<BmTsMatchingDetail>();

    public virtual ICollection<BmTsMatchingQuestionAnswer> BmTsMatchingQuestionAnswers { get; set; } = new List<BmTsMatchingQuestionAnswer>();

    public virtual ICollection<BmTsMatchingScoreSetDetail> BmTsMatchingScoreSetDetails { get; set; } = new List<BmTsMatchingScoreSetDetail>();

    public virtual ICollection<BmTsMatchingScoreSet> BmTsMatchingScoreSets { get; set; } = new List<BmTsMatchingScoreSet>();
}
