using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("revise_DefectType")]
public partial class revise_DefectType
{
    public double? DefectTypeID { get; set; }

    [StringLength(255)]
    public string? DefectType { get; set; }

    public double? LineOrder { get; set; }
}
