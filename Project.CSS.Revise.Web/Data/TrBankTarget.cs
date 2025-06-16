using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrBankTarget
{
    public int Id { get; set; }

    public int? BankId { get; set; }

    public int? Yearly { get; set; }

    public decimal? Amount { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmBank? Bank { get; set; }
}
