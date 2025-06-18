using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("BM_TS_Matching")]
public partial class BM_TS_Matching
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(500)]
    public string? FirstName { get; set; }

    [StringLength(1000)]
    public string? LastName { get; set; }

    public int? Age { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? MatchingDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? LoanAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? IncomeTotal { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DebtTotal { get; set; }

    public bool? FlagAccept { get; set; }

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

    [InverseProperty("Matching")]
    public virtual ICollection<BM_TS_Matching_Detail> BM_TS_Matching_Details { get; set; } = new List<BM_TS_Matching_Detail>();

    [InverseProperty("Matching")]
    public virtual ICollection<BM_TS_Matching_QuestionAnswer> BM_TS_Matching_QuestionAnswers { get; set; } = new List<BM_TS_Matching_QuestionAnswer>();

    [InverseProperty("Matching")]
    public virtual ICollection<BM_TS_Matching_ScoreSet_Detail> BM_TS_Matching_ScoreSet_Details { get; set; } = new List<BM_TS_Matching_ScoreSet_Detail>();

    [InverseProperty("Matching")]
    public virtual ICollection<BM_TS_Matching_ScoreSet> BM_TS_Matching_ScoreSets { get; set; } = new List<BM_TS_Matching_ScoreSet>();
}
