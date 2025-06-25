using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("BM_Master_ScoreType")]
public partial class BM_Master_ScoreType
{
    [Key]
    public int ID { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public int? Score { get; set; }

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

    [InverseProperty("ScoreType")]
    public virtual ICollection<BM_TR_QuestionAnswerScore> BM_TR_QuestionAnswerScores { get; set; } = new List<BM_TR_QuestionAnswerScore>();

    [InverseProperty("ScoreType")]
    public virtual ICollection<BM_TR_Set_Score> BM_TR_Set_Scores { get; set; } = new List<BM_TR_Set_Score>();
}
