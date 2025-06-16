using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrCustomerIncome
{
    public Guid Id { get; set; }

    public Guid? LoanId { get; set; }

    public Guid? CustomerId { get; set; }

    public int? IncomeId { get; set; }

    public string? OtherText { get; set; }

    public decimal? OtherValue { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public virtual PrCustomer? Customer { get; set; }

    public virtual TmExt? Income { get; set; }

    public virtual PrLoan? Loan { get; set; }
}
