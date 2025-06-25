using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("BM_TR_QuestionAnswerScore")]
public partial class BM_TR_QuestionAnswerScore
{
    [Key]
    public int ID { get; set; }

    public int? BankID { get; set; }

    public int? ScoreTypeID { get; set; }

    public int? QuestionID { get; set; }

    public int? AnswerID { get; set; }

    public int? Score { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? UpdateBy { get; set; }

    [ForeignKey("AnswerID")]
    [InverseProperty("BM_TR_QuestionAnswerScores")]
    public virtual BM_Master_QuestionAnswer? Answer { get; set; }

    [ForeignKey("BankID")]
    [InverseProperty("BM_TR_QuestionAnswerScores")]
    public virtual tm_Bank? Bank { get; set; }

    [ForeignKey("QuestionID")]
    [InverseProperty("BM_TR_QuestionAnswerScores")]
    public virtual BM_Master_Question? Question { get; set; }

    [ForeignKey("ScoreTypeID")]
    [InverseProperty("BM_TR_QuestionAnswerScores")]
    public virtual BM_Master_ScoreType? ScoreType { get; set; }
}
