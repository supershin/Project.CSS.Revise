using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Sync_LoanBank")]
public partial class TR_Sync_LoanBank
{
    [Key]
    public Guid ID { get; set; }

    public Guid? SyncID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UnitCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ContractNumber { get; set; }

    [StringLength(2000)]
    public string? ContractName { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ContractSellingPrice { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BankCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BankBranchID { get; set; }

    [StringLength(2000)]
    public string? BankBranchName { get; set; }

    public int? LoanStatus { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? LoanReqAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PriAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ApproveDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? AppAmount { get; set; }

    public int? Status { get; set; }

    [StringLength(200)]
    public string? BankContactName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? BankContactPhone { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? BankContactEmail { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReviewDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CompleteDocDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SentDocDate { get; set; }

    public int? ReasonID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LoanReqDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LoanStatusDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LoanSignDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? MortgageDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PriApproveDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? AppLifeInsurance { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? AppFireInsurance { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? AppDecorate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? AppOther { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PayPeriodAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PayDecorate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PayOther { get; set; }

    public string? Remark { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [ForeignKey("SyncID")]
    [InverseProperty("TR_Sync_LoanBanks")]
    public virtual TR_Sync? Sync { get; set; }
}
