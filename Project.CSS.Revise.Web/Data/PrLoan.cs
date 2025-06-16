using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrLoan
{
    public Guid Id { get; set; }

    public int? UserTypeId { get; set; }

    public string? ProjectId { get; set; }

    public string? UnitCode { get; set; }

    public string? ContractNumber { get; set; }

    public string? ContractName { get; set; }

    public string? ContractMobile { get; set; }

    public decimal? ContractSellingPrice { get; set; }

    public DateTime? DraftDate { get; set; }

    public string? DraftBy { get; set; }

    public DateTime? SubmitDate { get; set; }

    public string? SubmitBy { get; set; }

    public bool? FlagAccept { get; set; }

    public bool? ConsentAccept { get; set; }

    public bool? SubscribeAccept { get; set; }

    public string? ConsentSubscribeBy { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<PrCustomerCareer> PrCustomerCareers { get; set; } = new List<PrCustomerCareer>();

    public virtual ICollection<PrCustomerDebt> PrCustomerDebts { get; set; } = new List<PrCustomerDebt>();

    public virtual ICollection<PrCustomerIncome> PrCustomerIncomes { get; set; } = new List<PrCustomerIncome>();

    public virtual ICollection<PrLoanBankAttachFile> PrLoanBankAttachFiles { get; set; } = new List<PrLoanBankAttachFile>();

    public virtual ICollection<PrLoanCustomerAttach> PrLoanCustomerAttaches { get; set; } = new List<PrLoanCustomerAttach>();

    public virtual ICollection<PrLoanCustomer> PrLoanCustomers { get; set; } = new List<PrLoanCustomer>();

    public virtual TmProject? Project { get; set; }

    public virtual TmExt? UserType { get; set; }
}
