using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("BM_TS_Matching_Detail")]
public partial class BM_TS_Matching_Detail
{
    [Key]
    public int ID { get; set; }

    public Guid? MatchingID { get; set; }

    public int? BankID { get; set; }

    public int? MaxLoanYear { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? MonthlyInstallment { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? BankRate { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? BankAverageRate { get; set; }

    public int? AssessmentScore { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? AssessmentScorePerCent { get; set; }

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

    [ForeignKey("BankID")]
    [InverseProperty("BM_TS_Matching_Details")]
    public virtual tm_Bank? Bank { get; set; }

    [ForeignKey("MatchingID")]
    [InverseProperty("BM_TS_Matching_Details")]
    public virtual BM_TS_Matching? Matching { get; set; }
}
