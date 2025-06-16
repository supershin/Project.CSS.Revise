using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class BmTsMatchingDetail
{
    public int Id { get; set; }

    public Guid? MatchingId { get; set; }

    public int? BankId { get; set; }

    public int? MaxLoanYear { get; set; }

    public decimal? MonthlyInstallment { get; set; }

    public decimal? BankRate { get; set; }

    public decimal? BankAverageRate { get; set; }

    public int? AssessmentScore { get; set; }

    public decimal? AssessmentScorePerCent { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual TmBank? Bank { get; set; }

    public virtual BmTsMatching? Matching { get; set; }
}
