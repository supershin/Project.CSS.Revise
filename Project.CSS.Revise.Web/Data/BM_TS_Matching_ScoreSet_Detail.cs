using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("BM_TS_Matching_ScoreSet_Detail")]
public partial class BM_TS_Matching_ScoreSet_Detail
{
    [Key]
    public int ID { get; set; }

    public Guid? MatchingID { get; set; }

    public int? SetID { get; set; }

    public int? QuestionID { get; set; }

    public int? AnswerID { get; set; }

    [ForeignKey("AnswerID")]
    [InverseProperty("BM_TS_Matching_ScoreSet_Details")]
    public virtual BM_Master_QuestionAnswer? Answer { get; set; }

    [ForeignKey("MatchingID")]
    [InverseProperty("BM_TS_Matching_ScoreSet_Details")]
    public virtual BM_TS_Matching? Matching { get; set; }

    [ForeignKey("QuestionID")]
    [InverseProperty("BM_TS_Matching_ScoreSet_Details")]
    public virtual BM_Master_Question? Question { get; set; }

    [ForeignKey("SetID")]
    [InverseProperty("BM_TS_Matching_ScoreSet_Details")]
    public virtual BM_Master_Set? Set { get; set; }
}
