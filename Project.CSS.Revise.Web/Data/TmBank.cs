using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmBank
{
    public int Id { get; set; }

    public string? BankCode { get; set; }

    public string? BankName { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public virtual ICollection<BmMasterBank> BmMasterBanks { get; set; } = new List<BmMasterBank>();

    public virtual ICollection<BmTrLoanAgeRateBank> BmTrLoanAgeRateBanks { get; set; } = new List<BmTrLoanAgeRateBank>();

    public virtual ICollection<BmTrQuestionAnswerScore> BmTrQuestionAnswerScores { get; set; } = new List<BmTrQuestionAnswerScore>();

    public virtual ICollection<BmTrSetScore> BmTrSetScores { get; set; } = new List<BmTrSetScore>();

    public virtual ICollection<BmTsMatchingDetail> BmTsMatchingDetails { get; set; } = new List<BmTsMatchingDetail>();

    public virtual ICollection<BmTsMatchingQuestionAnswer> BmTsMatchingQuestionAnswers { get; set; } = new List<BmTsMatchingQuestionAnswer>();

    public virtual ICollection<BmTsMatchingScoreSet> BmTsMatchingScoreSets { get; set; } = new List<BmTsMatchingScoreSet>();

    public virtual ICollection<PrBankDocument> PrBankDocuments { get; set; } = new List<PrBankDocument>();

    public virtual ICollection<PrUserBankMapping> PrUserBankMappings { get; set; } = new List<PrUserBankMapping>();

    public virtual ICollection<TmLineToken> TmLineTokens { get; set; } = new List<TmLineToken>();

    public virtual ICollection<TrBankTarget> TrBankTargets { get; set; } = new List<TrBankTarget>();

    public virtual ICollection<TrContactLog> TrContactLogs { get; set; } = new List<TrContactLog>();

    public virtual ICollection<TrRegisterBankCounter> TrRegisterBankCounters { get; set; } = new List<TrRegisterBankCounter>();

    public virtual ICollection<TrRegisterProjectBankStaff> TrRegisterProjectBankStaffs { get; set; } = new List<TrRegisterProjectBankStaff>();

    public virtual ICollection<TrReviseUnitPromotion> TrReviseUnitPromotions { get; set; } = new List<TrReviseUnitPromotion>();

    public virtual ICollection<TrTransferDocument> TrTransferDocuments { get; set; } = new List<TrTransferDocument>();
}
