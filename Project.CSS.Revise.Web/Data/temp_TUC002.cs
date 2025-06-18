using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_TUC002")]
public partial class temp_TUC002
{
    [StringLength(255)]
    public string? ProjectID { get; set; }

    [StringLength(255)]
    public string? ProjectName { get; set; }

    [StringLength(255)]
    public string? UnitCode { get; set; }

    [StringLength(255)]
    public string? AddrNo { get; set; }

    [StringLength(255)]
    public string? Build { get; set; }

    [StringLength(255)]
    public string? Floor { get; set; }

    [StringLength(255)]
    public string? Room { get; set; }

    [StringLength(255)]
    public string? UnitType { get; set; }

    [StringLength(255)]
    public string? Area { get; set; }

    [StringLength(255)]
    public string? UnitStatus { get; set; }

    [StringLength(255)]
    public string? SellingPrice { get; set; }

    [StringLength(255)]
    public string? ContractNumber { get; set; }

    [StringLength(255)]
    public string? CodeVerify { get; set; }

    [StringLength(255)]
    public string? CustomerID { get; set; }

    [StringLength(255)]
    public string? CustomerName { get; set; }

    [StringLength(255)]
    public string? CustomerMobile { get; set; }

    [StringLength(255)]
    public string? ContractName { get; set; }

    [StringLength(255)]
    public string? ContractMobile { get; set; }

    [StringLength(255)]
    public string? FinanceCareDraft { get; set; }

    [StringLength(255)]
    public string? LoanStatusName_FINPlus { get; set; }

    [StringLength(255)]
    public string? LoanBankName_FINPlus { get; set; }

    [StringLength(255)]
    public string? LoanBankName_Select { get; set; }

    [StringLength(255)]
    public string? BookDate { get; set; }

    [StringLength(255)]
    public string? ContractDate { get; set; }

    [StringLength(255)]
    public string? Appoint_Date { get; set; }

    [StringLength(255)]
    public string? ContactLog_Date { get; set; }

    [StringLength(255)]
    public string? QC5_FinishDate { get; set; }

    [StringLength(255)]
    public string? QC6_Status { get; set; }

    [StringLength(255)]
    public string? QC6_Date { get; set; }

    [StringLength(255)]
    public string? Inspect_Count { get; set; }

    [StringLength(255)]
    public string? Defect { get; set; }

    [StringLength(255)]
    public string? DefectStatus { get; set; }

    [StringLength(255)]
    public string? TransferDate { get; set; }

    [StringLength(255)]
    public string? ReceiveRoomDate { get; set; }

    [StringLength(255)]
    public string? ReceiveDocument { get; set; }

    [StringLength(255)]
    public string? ReceiveRoomAgreementDate { get; set; }

    [StringLength(255)]
    public string? SaveDocument { get; set; }

    [StringLength(255)]
    public string? UnitStatus_CS { get; set; }

    [StringLength(255)]
    public string? RemarkUnitStatus_CS { get; set; }

    [StringLength(255)]
    public string? LetterDueDate_CS { get; set; }

    [StringLength(255)]
    public string? ExpectTransfer { get; set; }

    [StringLength(255)]
    public string? TransferDueDate_CS { get; set; }

    [StringLength(255)]
    public string? ExpectTransferBy { get; set; }

    [StringLength(255)]
    public string? MeterTypeName { get; set; }

    [StringLength(255)]
    public string? CSResponse { get; set; }

    [StringLength(255)]
    public string? LastExpectDate { get; set; }

    [StringLength(255)]
    public string? LineContract { get; set; }

    [StringLength(255)]
    public string? Redemption { get; set; }

    [StringLength(255)]
    public string? BankSelected_CS { get; set; }

    [StringLength(255)]
    public string? Q5 { get; set; }

    [StringLength(255)]
    public string? Q5_Detail { get; set; }

    [StringLength(255)]
    public string? Q1 { get; set; }

    [StringLength(255)]
    public string? Q1_Detail { get; set; }

    [StringLength(255)]
    public string? Q2 { get; set; }

    [StringLength(255)]
    public string? Q2_Detail { get; set; }

    [StringLength(255)]
    public string? Q3 { get; set; }

    [StringLength(255)]
    public string? Q3_Detail { get; set; }

    [StringLength(255)]
    public string? Q4 { get; set; }

    [StringLength(255)]
    public string? Q4_Detail { get; set; }

    [StringLength(255)]
    public string? Q6 { get; set; }

    [StringLength(255)]
    public string? Q6_Detail { get; set; }

    [StringLength(255)]
    public string? Q7 { get; set; }

    [StringLength(255)]
    public string? Q7_Detail { get; set; }

    [StringLength(255)]
    public string? Q8 { get; set; }

    [StringLength(255)]
    public string? Q8_Detail { get; set; }

    [StringLength(255)]
    public string? Q9 { get; set; }

    [StringLength(255)]
    public string? Q9_Detail { get; set; }

    [StringLength(255)]
    public string? Q10 { get; set; }
}
