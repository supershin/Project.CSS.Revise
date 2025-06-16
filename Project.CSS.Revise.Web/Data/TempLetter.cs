using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TempLetter
{
    public string? UnitCode { get; set; }

    public double? SendType { get; set; }

    public string? SendLang { get; set; }

    public DateOnly? SendDate { get; set; }

    public DateOnly? TransferDate { get; set; }

    public string? ContractNumber { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerMobile { get; set; }

    public string? CustomerEmail { get; set; }

    public string? Csresponse { get; set; }
}
