using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("revise_Area")]
public partial class revise_Area
{
    public double? DefectAreaID { get; set; }

    [StringLength(255)]
    public string? DefectArea { get; set; }

    public double? LineOrder { get; set; }
}
