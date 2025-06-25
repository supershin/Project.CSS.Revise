using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_letter")]
public partial class temp_letter
{
    [StringLength(255)]
    public string? UnitCode { get; set; }

    public double? SendType { get; set; }

    [StringLength(255)]
    public string? SendLang { get; set; }

    public DateOnly? SendDate { get; set; }

    public DateOnly? TransferDate { get; set; }

    [StringLength(255)]
    public string? ContractNumber { get; set; }

    [StringLength(255)]
    public string? CustomerName { get; set; }

    [StringLength(255)]
    public string? CustomerMobile { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? CustomerEmail { get; set; }

    [StringLength(255)]
    public string? CSResponse { get; set; }
}
