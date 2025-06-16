using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrQc5CheckListDetail
{
    public int Id { get; set; }

    public Guid? Qc5checkListId { get; set; }

    public int? QuestionId { get; set; }

    public int? AnswerId { get; set; }

    public string? Remark { get; set; }

    public decimal? Score { get; set; }

    public DateTime? CreateDtae { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmExt? Answer { get; set; }

    public virtual TrQc5CheckList? Qc5checkList { get; set; }

    public virtual TmQc5CheckList? Question { get; set; }
}
