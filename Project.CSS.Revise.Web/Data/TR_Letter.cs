using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Letter")]
public partial class TR_Letter
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public int? LetterTypeID { get; set; }

    public int? SendTypeID { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? SendLang { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ContractNumber { get; set; }

    [StringLength(1000)]
    public string? CustomerName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? CustomerEmail { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransferDueDate { get; set; }

    public int? VerifyByID { get; set; }

    [StringLength(1000)]
    public string? VerifyBy { get; set; }

    public int? VerifyStatusID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? VerifyDate { get; set; }

    public string? VerifyDetail { get; set; }

    public int? ApproveStatusID { get; set; }

    public string? ApproveDetail { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ApproveDate { get; set; }

    [StringLength(1000)]
    public string? ApproveBy { get; set; }

    public int? ApproveByID { get; set; }

    public Guid? ApproveSignID { get; set; }

    [StringLength(200)]
    public string? ApprovePosition { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PrintDate { get; set; }

    [StringLength(1000)]
    public string? PrintBy { get; set; }

    public int? PrintByID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SendDueDate { get; set; }

    public int? SendStatusID { get; set; }

    public int? SendReasonID { get; set; }

    public string? SendRemark { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SendUpdateDate { get; set; }

    [StringLength(1000)]
    public string? SendUpdateBy { get; set; }

    public int? SendUpdateByID { get; set; }

    public Guid? LetterReferenceID { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("ApproveSignID")]
    [InverseProperty("TR_Letters")]
    public virtual TR_SignResource? ApproveSign { get; set; }

    [ForeignKey("ApproveStatusID")]
    [InverseProperty("TR_LetterApproveStatuses")]
    public virtual tm_Ext? ApproveStatus { get; set; }

    [InverseProperty("LetterReference")]
    public virtual ICollection<TR_Letter> InverseLetterReference { get; set; } = new List<TR_Letter>();

    [ForeignKey("LetterReferenceID")]
    [InverseProperty("InverseLetterReference")]
    public virtual TR_Letter? LetterReference { get; set; }

    [ForeignKey("LetterTypeID")]
    [InverseProperty("TR_LetterLetterTypes")]
    public virtual tm_Ext? LetterType { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_Letters")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("SendReasonID")]
    [InverseProperty("TR_Letters")]
    public virtual tm_LetterSendReason? SendReason { get; set; }

    [ForeignKey("SendStatusID")]
    [InverseProperty("TR_LetterSendStatuses")]
    public virtual tm_Ext? SendStatus { get; set; }

    [ForeignKey("SendTypeID")]
    [InverseProperty("TR_LetterSendTypes")]
    public virtual tm_Ext? SendType { get; set; }

    [InverseProperty("Letter")]
    public virtual ICollection<TR_Letter_Attach> TR_Letter_Attaches { get; set; } = new List<TR_Letter_Attach>();

    [InverseProperty("Letter")]
    public virtual ICollection<TR_Letter_Lot_Detail> TR_Letter_Lot_Details { get; set; } = new List<TR_Letter_Lot_Detail>();

    [ForeignKey("UnitID")]
    [InverseProperty("TR_Letters")]
    public virtual tm_Unit? Unit { get; set; }

    [ForeignKey("VerifyStatusID")]
    [InverseProperty("TR_LetterVerifyStatuses")]
    public virtual tm_Ext? VerifyStatus { get; set; }
}
