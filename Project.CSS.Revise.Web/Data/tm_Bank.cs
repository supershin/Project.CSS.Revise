using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Bank")]
public partial class tm_Bank
{
    [Key]
    public int ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BankCode { get; set; }

    public string? BankName { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [InverseProperty("Bank")]
    public virtual ICollection<BM_Master_Bank> BM_Master_Banks { get; set; } = new List<BM_Master_Bank>();

    [InverseProperty("Bank")]
    public virtual ICollection<BM_TR_LoanAgeRate_Bank> BM_TR_LoanAgeRate_Banks { get; set; } = new List<BM_TR_LoanAgeRate_Bank>();

    [InverseProperty("Bank")]
    public virtual ICollection<BM_TR_QuestionAnswerScore> BM_TR_QuestionAnswerScores { get; set; } = new List<BM_TR_QuestionAnswerScore>();

    [InverseProperty("Bank")]
    public virtual ICollection<BM_TR_Set_Score> BM_TR_Set_Scores { get; set; } = new List<BM_TR_Set_Score>();

    [InverseProperty("Bank")]
    public virtual ICollection<BM_TS_Matching_Detail> BM_TS_Matching_Details { get; set; } = new List<BM_TS_Matching_Detail>();

    [InverseProperty("Bank")]
    public virtual ICollection<BM_TS_Matching_QuestionAnswer> BM_TS_Matching_QuestionAnswers { get; set; } = new List<BM_TS_Matching_QuestionAnswer>();

    [InverseProperty("Bank")]
    public virtual ICollection<BM_TS_Matching_ScoreSet> BM_TS_Matching_ScoreSets { get; set; } = new List<BM_TS_Matching_ScoreSet>();

    [InverseProperty("Bank")]
    public virtual ICollection<PR_BankDocument> PR_BankDocuments { get; set; } = new List<PR_BankDocument>();

    [InverseProperty("Bank")]
    public virtual ICollection<PR_UserBank_Mapping> PR_UserBank_Mappings { get; set; } = new List<PR_UserBank_Mapping>();

    [InverseProperty("Bank")]
    public virtual ICollection<TR_ContactLog> TR_ContactLogs { get; set; } = new List<TR_ContactLog>();

    [InverseProperty("Bank")]
    public virtual ICollection<TR_Register_BankCounter> TR_Register_BankCounters { get; set; } = new List<TR_Register_BankCounter>();

    [InverseProperty("Bank")]
    public virtual ICollection<TR_Register_ProjectBankStaff> TR_Register_ProjectBankStaffs { get; set; } = new List<TR_Register_ProjectBankStaff>();

    [InverseProperty("CustomerBank")]
    public virtual ICollection<TR_ReviseUnitPromotion> TR_ReviseUnitPromotions { get; set; } = new List<TR_ReviseUnitPromotion>();

    [InverseProperty("Cashier_Bank")]
    public virtual ICollection<TR_TransferDocument> TR_TransferDocuments { get; set; } = new List<TR_TransferDocument>();

    [InverseProperty("Bank")]
    public virtual ICollection<tm_LineToken> tm_LineTokens { get; set; } = new List<tm_LineToken>();
}
