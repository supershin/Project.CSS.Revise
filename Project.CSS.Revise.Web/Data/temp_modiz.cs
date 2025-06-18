using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_modiz")]
public partial class temp_modiz
{
    [StringLength(255)]
    public string? Build { get; set; }

    [StringLength(255)]
    public string? UnitCode { get; set; }

    [StringLength(255)]
    public string? UnitType { get; set; }

    [StringLength(255)]
    public string? Area { get; set; }

    [StringLength(255)]
    public string? ProjectID { get; set; }

    [StringLength(255)]
    public string? CustomerID { get; set; }

    [StringLength(255)]
    public string? CustomerName { get; set; }

    public double? SellingPrice { get; set; }
}
