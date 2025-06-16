using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrProjectBankUserMapping
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public int? BankUserId { get; set; }
}
