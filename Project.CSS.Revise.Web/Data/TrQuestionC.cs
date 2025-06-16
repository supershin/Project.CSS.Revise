using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrQuestionC
{
    public int Id { get; set; }

    public int? QuestionTypeId { get; set; }

    public string? QuestionName { get; set; }

    public bool? FlagMultiChoice { get; set; }

    public bool? FlagCorporate { get; set; }

    public bool? FlagRating { get; set; }

    public int? Revision { get; set; }

    public int? Col { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmExt? QuestionType { get; set; }

    public virtual ICollection<TrAnswerC> TrAnswerCs { get; set; } = new List<TrAnswerC>();

    public virtual ICollection<TrCustomerSatisfactionDetail> TrCustomerSatisfactionDetails { get; set; } = new List<TrCustomerSatisfactionDetail>();
}
