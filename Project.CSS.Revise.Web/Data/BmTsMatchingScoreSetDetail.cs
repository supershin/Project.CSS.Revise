using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class BmTsMatchingScoreSetDetail
{
    public int Id { get; set; }

    public Guid? MatchingId { get; set; }

    public int? SetId { get; set; }

    public int? QuestionId { get; set; }

    public int? AnswerId { get; set; }

    public virtual BmMasterQuestionAnswer? Answer { get; set; }

    public virtual BmTsMatching? Matching { get; set; }

    public virtual BmMasterQuestion? Question { get; set; }

    public virtual BmMasterSet? Set { get; set; }
}
