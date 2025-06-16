using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrContractVerify
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public string? UnitCode { get; set; }

    public string? ContractNumber { get; set; }

    public string? ContractName { get; set; }

    public string? ContractMobile { get; set; }

    public decimal? ContractSellingPrice { get; set; }

    public string? CodeVerify { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }
}
