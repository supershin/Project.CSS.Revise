using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrLoanCustomer
{
    public Guid Id { get; set; }

    public Guid? LoanId { get; set; }

    public Guid? CustomerId { get; set; }

    public int? Seq { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual PrCustomer? Customer { get; set; }

    public virtual PrLoan? Loan { get; set; }
}
