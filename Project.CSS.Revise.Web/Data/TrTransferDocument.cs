using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrTransferDocument
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public Guid? SignResourceId { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerMobile { get; set; }

    public bool? Owner { get; set; }

    public bool? OwnerR { get; set; }

    public bool? OwnerC { get; set; }

    public bool? Contract { get; set; }

    public bool? ContractR { get; set; }

    public bool? ContractC { get; set; }

    public bool? Mortgage { get; set; }

    public bool? MortgageR { get; set; }

    public bool? MortgageC { get; set; }

    public bool? Receipt { get; set; }

    public bool? ReceiptR { get; set; }

    public bool? ReceiptC { get; set; }

    public bool? RegisterHome { get; set; }

    public bool? Rebate { get; set; }

    public decimal? RebateAmt { get; set; }

    public bool? Cashier { get; set; }

    public int? CashierBankId { get; set; }

    public string? Cashier1No { get; set; }

    public decimal? Cashier1Amt { get; set; }

    public string? Cashier2No { get; set; }

    public decimal? Cashier2Amt { get; set; }

    public string? Cashier3No { get; set; }

    public decimal? Cashier3Amt { get; set; }

    public bool? Cash { get; set; }

    public decimal? CashAmt { get; set; }

    public bool? Other { get; set; }

    public string? OtherText { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? PrintDate { get; set; }

    public int? PrintBy { get; set; }

    public virtual TmBank? CashierBank { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TmUnit? Unit { get; set; }
}
