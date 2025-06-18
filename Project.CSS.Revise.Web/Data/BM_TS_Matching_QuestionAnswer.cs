using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("BM_TS_Matching_QuestionAnswer")]
public partial class BM_TS_Matching_QuestionAnswer
{
    [Key]
    public long ID { get; set; }

    public Guid? MatchingID { get; set; }

    public int? QuestionID { get; set; }

    public int? AnswerID { get; set; }

    public int? BankID { get; set; }

    public int? HDP { get; set; }

    public int? Credit { get; set; }

    public int? Debt { get; set; }

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
    [InverseProperty("BM_TS_Matching_QuestionAnswers")]
    public virtual BM_Master_QuestionAnswer? Answer { get; set; }

    [ForeignKey("BankID")]
    [InverseProperty("BM_TS_Matching_QuestionAnswers")]
    public virtual tm_Bank? Bank { get; set; }

    [ForeignKey("MatchingID")]
    [InverseProperty("BM_TS_Matching_QuestionAnswers")]
    public virtual BM_TS_Matching? Matching { get; set; }

    [ForeignKey("QuestionID")]
    [InverseProperty("BM_TS_Matching_QuestionAnswers")]
    public virtual BM_Master_Question? Question { get; set; }
}
