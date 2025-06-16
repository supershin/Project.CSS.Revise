using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrLoanBank
{
    public Guid Id { get; set; }

    public Guid? LoanId { get; set; }

    public int? BankId { get; set; }

    public int? UserTypeId { get; set; }

    public int? BankProgressId { get; set; }

    public string? BankProgressDetail { get; set; }

    public decimal? PreTotalApprove { get; set; }

    public decimal? PreHousingLoanLimit { get; set; }

    public decimal? PreDecorationCreditLine { get; set; }

    public decimal? PreFireInsurance { get; set; }

    public int? BankStatusId { get; set; }

    public DateTime? ApproveDate { get; set; }

    public string? PersonalLoan { get; set; }

    public string? PersonalLoan2 { get; set; }

    public int? MarriedStatusId { get; set; }

    public string? MarriedStatusOther { get; set; }

    public int? PersonalOwner { get; set; }

    public string? PersonalOwnerDetail { get; set; }

    public string? PersonalOwner1 { get; set; }

    public string? PersonalOwner2 { get; set; }

    public string? PersonalOwner3 { get; set; }

    public decimal? TotalApprove { get; set; }

    public decimal? HousingLoanLimit { get; set; }

    public decimal? DecorationCreditLine { get; set; }

    public decimal? Mrta { get; set; }

    public decimal? FireInsurance { get; set; }

    public decimal? OtherApprove { get; set; }

    public decimal? LessFirstInstallment { get; set; }

    public decimal? RevenueStamp { get; set; }

    public int? MortgageTypeId { get; set; }

    public string? MortgageTypeText { get; set; }

    public DateTime? TransferDueDate { get; set; }

    public string? BankStaffName { get; set; }

    public string? BankStaffMobile { get; set; }

    public string? ApproveRemark { get; set; }

    public int? RejectReasonId { get; set; }

    public string? RejectReasonNote { get; set; }

    public decimal? RejectHl { get; set; }

    public decimal? RejectFn { get; set; }

    public decimal? RejectPl { get; set; }

    public decimal? RejectCooperative { get; set; }

    public decimal? RejectOther { get; set; }

    public string? CancelNote { get; set; }

    public DateTime? DraftDate { get; set; }

    public string? DraftBy { get; set; }

    public DateTime? SubmitDate { get; set; }

    public string? SubmitBy { get; set; }

    public string? Remark { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? ClearSubmitDate { get; set; }

    public string? ClearSubmitBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<PrLoanBankAttachFile> PrLoanBankAttachFiles { get; set; } = new List<PrLoanBankAttachFile>();

    public virtual PrLoanBankExplain? PrLoanBankExplain { get; set; }
}
