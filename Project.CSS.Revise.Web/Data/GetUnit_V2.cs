using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("GetUnit_V2")]
public partial class GetUnit_V2
{
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? UnitCode { get; set; }

    [StringLength(500)]
    public string? AddrNo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Build { get; set; }

    public int? Floor { get; set; }

    public int? Room { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? UnitType { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Area { get; set; }

    [StringLength(50)]
    public string? UnitStatus { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SellingPrice { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ContractNumber { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? CodeVerify { get; set; }

    public int? CustomerID { get; set; }

    [StringLength(500)]
    public string? CustomerName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? CustomerMobile { get; set; }

    public string? ContractName { get; set; }

    [Unicode(false)]
    public string? ContractMobile { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FinanceCareDraft { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? FinplusSubmitDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Preapprove_FINPlus { get; set; }

    [StringLength(9)]
    [Unicode(false)]
    public string? LoanStatusName_FINPlus { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? LoanBankName_FINPlus { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? LoanBankName_Select { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? BookDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ContractDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Appoint_Date { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ContactLog_Date { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? QC5_FinishDate { get; set; }

    public int? QC6 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? QC6_Status { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? QC6_Date { get; set; }

    public int? Inspect_Count { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Inspect_Date { get; set; }

    public int? Defect { get; set; }

    [StringLength(400)]
    public string? DefectStatusName { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? TransferDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ReceiveRoomDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ReceiveDocument { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ReceiveRoomAgreementDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? SaveDocument { get; set; }

    [StringLength(100)]
    public string? UnitStatus_CS { get; set; }

    [StringLength(100)]
    public string? RemarkUnitStatus_CS { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? LetterDueDate_CS { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ExpectTransfer { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? TransferDueDate_CS { get; set; }

    [StringLength(50)]
    public string? ExpectTransferBy { get; set; }

    [StringLength(100)]
    public string? MeterTypeName { get; set; }

    [StringLength(200)]
    public string? CSResponse { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? LastExpectDate { get; set; }

    public int? LineContract { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Redemption { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? BankSelected_CS { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? FreeAll { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TransferNetPrice { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ExpectTransferDeviate { get; set; }
}
