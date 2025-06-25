using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("PR_LoanBank")]
[Index("LoanID", "BankID", "BankProgressID", "BankStatusID", "DraftDate", "SubmitDate", Name = "NonClusteredIndex-20201109-102602")]
public partial class PR_LoanBank
{
    [Key]
    public Guid ID { get; set; }

    public Guid? LoanID { get; set; }

    public int? BankID { get; set; }

    public int? UserTypeID { get; set; }

    public int? BankProgressID { get; set; }

    public string? BankProgressDetail { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PreTotalApprove { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PreHousingLoanLimit { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PreDecorationCreditLine { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PreFireInsurance { get; set; }

    public int? BankStatusID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ApproveDate { get; set; }

    public string? PersonalLoan { get; set; }

    public string? PersonalLoan_2 { get; set; }

    public int? MarriedStatusID { get; set; }

    public string? MarriedStatusOther { get; set; }

    public int? PersonalOwner { get; set; }

    public string? PersonalOwnerDetail { get; set; }

    public string? PersonalOwner_1 { get; set; }

    public string? PersonalOwner_2 { get; set; }

    public string? PersonalOwner_3 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalApprove { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? HousingLoanLimit { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DecorationCreditLine { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? MRTA { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? FireInsurance { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? OtherApprove { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? LessFirstInstallment { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? RevenueStamp { get; set; }

    public int? MortgageTypeID { get; set; }

    public string? MortgageTypeText { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransferDueDate { get; set; }

    public string? BankStaffName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? BankStaffMobile { get; set; }

    public string? ApproveRemark { get; set; }

    public int? RejectReasonID { get; set; }

    public string? RejectReasonNote { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? RejectHL { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? RejectFN { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? RejectPL { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? RejectCooperative { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? RejectOther { get; set; }

    public string? CancelNote { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DraftDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? DraftBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SubmitDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? SubmitBy { get; set; }

    public string? Remark { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ClearSubmitDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ClearSubmitBy { get; set; }

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

    [InverseProperty("LoanBank")]
    public virtual ICollection<PR_LoanBankAttachFile> PR_LoanBankAttachFiles { get; set; } = new List<PR_LoanBankAttachFile>();

    [InverseProperty("LoanBank")]
    public virtual PR_LoanBank_Explain? PR_LoanBank_Explain { get; set; }
}
