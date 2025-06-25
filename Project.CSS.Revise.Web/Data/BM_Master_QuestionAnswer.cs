using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("BM_Master_QuestionAnswer")]
public partial class BM_Master_QuestionAnswer
{
    [Key]
    public int ID { get; set; }

    public int? QuestionID { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CompareFrom { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CompareTo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Color { get; set; }

    public int? LineOrder { get; set; }

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

    [InverseProperty("Answer")]
    public virtual ICollection<BM_Master_Set_QuestionAnswer> BM_Master_Set_QuestionAnswers { get; set; } = new List<BM_Master_Set_QuestionAnswer>();

    [InverseProperty("Answer")]
    public virtual ICollection<BM_TR_QuestionAnswerScore> BM_TR_QuestionAnswerScores { get; set; } = new List<BM_TR_QuestionAnswerScore>();

    [InverseProperty("Answer")]
    public virtual ICollection<BM_TS_Matching_QuestionAnswer> BM_TS_Matching_QuestionAnswers { get; set; } = new List<BM_TS_Matching_QuestionAnswer>();

    [InverseProperty("Answer")]
    public virtual ICollection<BM_TS_Matching_ScoreSet_Detail> BM_TS_Matching_ScoreSet_Details { get; set; } = new List<BM_TS_Matching_ScoreSet_Detail>();

    [ForeignKey("QuestionID")]
    [InverseProperty("BM_Master_QuestionAnswers")]
    public virtual BM_Master_Question? Question { get; set; }
}
