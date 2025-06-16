using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrLetter
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public int? LetterTypeId { get; set; }

    public int? SendTypeId { get; set; }

    public string? SendLang { get; set; }

    public string? ContractNumber { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerEmail { get; set; }

    public DateTime? TransferDueDate { get; set; }

    public int? VerifyById { get; set; }

    public string? VerifyBy { get; set; }

    public int? VerifyStatusId { get; set; }

    public DateTime? VerifyDate { get; set; }

    public string? VerifyDetail { get; set; }

    public int? ApproveStatusId { get; set; }

    public string? ApproveDetail { get; set; }

    public DateTime? ApproveDate { get; set; }

    public string? ApproveBy { get; set; }

    public int? ApproveById { get; set; }

    public Guid? ApproveSignId { get; set; }

    public string? ApprovePosition { get; set; }

    public DateTime? PrintDate { get; set; }

    public string? PrintBy { get; set; }

    public int? PrintById { get; set; }

    public DateTime? SendDueDate { get; set; }

    public int? SendStatusId { get; set; }

    public int? SendReasonId { get; set; }

    public string? SendRemark { get; set; }

    public DateTime? SendUpdateDate { get; set; }

    public string? SendUpdateBy { get; set; }

    public int? SendUpdateById { get; set; }

    public Guid? LetterReferenceId { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TrSignResource? ApproveSign { get; set; }

    public virtual TmExt? ApproveStatus { get; set; }

    public virtual ICollection<TrLetter> InverseLetterReference { get; set; } = new List<TrLetter>();

    public virtual TrLetter? LetterReference { get; set; }

    public virtual TmExt? LetterType { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TmLetterSendReason? SendReason { get; set; }

    public virtual TmExt? SendStatus { get; set; }

    public virtual TmExt? SendType { get; set; }

    public virtual ICollection<TrLetterAttach> TrLetterAttaches { get; set; } = new List<TrLetterAttach>();

    public virtual ICollection<TrLetterLotDetail> TrLetterLotDetails { get; set; } = new List<TrLetterLotDetail>();

    public virtual TmUnit? Unit { get; set; }

    public virtual TmExt? VerifyStatus { get; set; }
}
