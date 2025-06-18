using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("BM_Master_Set")]
public partial class BM_Master_Set
{
    [Key]
    public int ID { get; set; }

    [StringLength(500)]
    public string? Name { get; set; }

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

    [InverseProperty("Set")]
    public virtual ICollection<BM_Master_Set_QuestionAnswer> BM_Master_Set_QuestionAnswers { get; set; } = new List<BM_Master_Set_QuestionAnswer>();

    [InverseProperty("Set")]
    public virtual ICollection<BM_TR_Set_Score> BM_TR_Set_Scores { get; set; } = new List<BM_TR_Set_Score>();

    [InverseProperty("Set")]
    public virtual ICollection<BM_TS_Matching_ScoreSet_Detail> BM_TS_Matching_ScoreSet_Details { get; set; } = new List<BM_TS_Matching_ScoreSet_Detail>();

    [InverseProperty("Set")]
    public virtual ICollection<BM_TS_Matching_ScoreSet> BM_TS_Matching_ScoreSets { get; set; } = new List<BM_TS_Matching_ScoreSet>();
}
