using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_TransferDocument")]
public partial class TR_TransferDocument
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public Guid? SignResourceID { get; set; }

    [StringLength(1000)]
    public string? CustomerName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CustomerMobile { get; set; }

    public bool? Owner { get; set; }

    public bool? Owner_R { get; set; }

    public bool? Owner_C { get; set; }

    public bool? Contract { get; set; }

    public bool? Contract_R { get; set; }

    public bool? Contract_C { get; set; }

    public bool? Mortgage { get; set; }

    public bool? Mortgage_R { get; set; }

    public bool? Mortgage_C { get; set; }

    public bool? Receipt { get; set; }

    public bool? Receipt_R { get; set; }

    public bool? Receipt_C { get; set; }

    public bool? RegisterHome { get; set; }

    public bool? Rebate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Rebate_Amt { get; set; }

    public bool? Cashier { get; set; }

    public int? Cashier_BankID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Cashier1_No { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Cashier1_Amt { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Cashier2_No { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Cashier2_Amt { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Cashier3_No { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Cashier3_Amt { get; set; }

    public bool? Cash { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Cash_Amt { get; set; }

    public bool? Other { get; set; }

    public string? OtherText { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PrintDate { get; set; }

    public int? PrintBy { get; set; }

    [ForeignKey("Cashier_BankID")]
    [InverseProperty("TR_TransferDocuments")]
    public virtual tm_Bank? Cashier_Bank { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_TransferDocuments")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_TransferDocuments")]
    public virtual tm_Unit? Unit { get; set; }
}
