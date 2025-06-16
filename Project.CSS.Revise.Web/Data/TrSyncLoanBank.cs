using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrSyncLoanBank
{
    public Guid Id { get; set; }

    public Guid? SyncId { get; set; }

    public string? ProjectId { get; set; }

    public string? UnitCode { get; set; }

    public string? ContractNumber { get; set; }

    public string? ContractName { get; set; }

    public decimal? ContractSellingPrice { get; set; }

    public string? BankCode { get; set; }

    public string? BankBranchId { get; set; }

    public string? BankBranchName { get; set; }

    public int? LoanStatus { get; set; }

    public decimal? LoanReqAmount { get; set; }

    public decimal? PriAmount { get; set; }

    public DateTime? ApproveDate { get; set; }

    public decimal? AppAmount { get; set; }

    public int? Status { get; set; }

    public string? BankContactName { get; set; }

    public string? BankContactPhone { get; set; }

    public string? BankContactEmail { get; set; }

    public DateTime? ReviewDate { get; set; }

    public DateTime? CompleteDocDate { get; set; }

    public DateTime? SentDocDate { get; set; }

    public int? ReasonId { get; set; }

    public DateTime? LoanReqDate { get; set; }

    public DateTime? LoanStatusDate { get; set; }

    public DateTime? LoanSignDate { get; set; }

    public DateTime? MortgageDate { get; set; }

    public DateTime? PriApproveDate { get; set; }

    public decimal? AppLifeInsurance { get; set; }

    public decimal? AppFireInsurance { get; set; }

    public decimal? AppDecorate { get; set; }

    public decimal? AppOther { get; set; }

    public decimal? PayPeriodAmount { get; set; }

    public decimal? PayDecorate { get; set; }

    public decimal? PayOther { get; set; }

    public string? Remark { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public virtual TrSync? Sync { get; set; }
}
