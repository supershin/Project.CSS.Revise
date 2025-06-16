using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrCustomerCareer
{
    public Guid Id { get; set; }

    public Guid? LoanId { get; set; }

    public Guid? CustomerId { get; set; }

    public int? CareerId { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public virtual TmExt? Career { get; set; }

    public virtual PrCustomer? Customer { get; set; }

    public virtual PrLoan? Loan { get; set; }
}
