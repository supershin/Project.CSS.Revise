using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrRegisterBankCounter
{
    public int Id { get; set; }

    public int? RegisterLogId { get; set; }

    public int? BankId { get; set; }

    public string? BankCounterStatus { get; set; }

    public DateTime? CheckInDate { get; set; }

    public DateTime? InProcessDate { get; set; }

    public DateTime? CheckOutDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual TmBank? Bank { get; set; }

    public virtual TrRegisterLog? RegisterLog { get; set; }
}
