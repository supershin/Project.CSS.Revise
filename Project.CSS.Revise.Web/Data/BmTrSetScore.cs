using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class BmTrSetScore
{
    public int Id { get; set; }

    public int? SetId { get; set; }

    public int? BankId { get; set; }

    public int? ScoreTypeId { get; set; }

    public int? Score { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual TmBank? Bank { get; set; }

    public virtual BmMasterScoreType? ScoreType { get; set; }

    public virtual BmMasterSet? Set { get; set; }
}
