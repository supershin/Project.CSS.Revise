using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrUserBankMapping
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? BankId { get; set; }

    public virtual TmBank? Bank { get; set; }

    public virtual PrUser? User { get; set; }
}
