using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrCustomer
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? IdcardNo { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public int? Age { get; set; }

    public string? AddressName { get; set; }

    public string? SubDistrict { get; set; }

    public string? District { get; set; }

    public string? Province { get; set; }

    public string? ZipCode { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<PrCustomerCareer> PrCustomerCareers { get; set; } = new List<PrCustomerCareer>();

    public virtual ICollection<PrCustomerDebt> PrCustomerDebts { get; set; } = new List<PrCustomerDebt>();

    public virtual ICollection<PrCustomerIncome> PrCustomerIncomes { get; set; } = new List<PrCustomerIncome>();

    public virtual ICollection<PrLoanCustomerAttach> PrLoanCustomerAttaches { get; set; } = new List<PrLoanCustomerAttach>();

    public virtual ICollection<PrLoanCustomer> PrLoanCustomers { get; set; } = new List<PrLoanCustomer>();
}
