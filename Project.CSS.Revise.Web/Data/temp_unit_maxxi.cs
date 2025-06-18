using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_unit_maxxi")]
public partial class temp_unit_maxxi
{
    public double? UnitCode { get; set; }

    public double? Floor { get; set; }

    [StringLength(255)]
    public string? Room { get; set; }

    public double? Area { get; set; }

    [Column("Addr No")]
    [StringLength(255)]
    public string? Addr_No { get; set; }

    public double? SellingPrice { get; set; }

    [StringLength(255)]
    public string? ContractNumber { get; set; }

    [StringLength(255)]
    public string? CustomerID { get; set; }

    [StringLength(255)]
    public string? CustomerName { get; set; }

    [StringLength(255)]
    public string? CustomerMobile { get; set; }
}
