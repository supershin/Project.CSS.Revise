using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrQuestionAnswer
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public int? QuestionId { get; set; }

    public int? AnswerId { get; set; }

    public string? OtherText { get; set; }

    public DateTime? QuestionDate { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? ClearDate { get; set; }

    public int? ClearBy { get; set; }

    public virtual TmAnswer? Answer { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TmQuestion? Question { get; set; }
}
