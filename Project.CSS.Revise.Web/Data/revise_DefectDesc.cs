using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("revise_DefectDesc")]
public partial class revise_DefectDesc
{
    public double? DefectTypeID { get; set; }

    [StringLength(255)]
    public string? DefectType { get; set; }

    [StringLength(255)]
    public string? DefectDiscription { get; set; }

    public double? LineOrder { get; set; }
}
