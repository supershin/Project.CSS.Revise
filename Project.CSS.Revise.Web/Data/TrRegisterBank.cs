using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrRegisterBank
{
    public int Id { get; set; }

    public int? RegisterLogId { get; set; }

    public int? BankId { get; set; }

    public virtual TrRegisterLog? RegisterLog { get; set; }
}
