using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("defect_type_20211105")]
public partial class defect_type_20211105
{
    [StringLength(255)]
    public string? DefectArea { get; set; }

    [StringLength(255)]
    public string? DefectType { get; set; }

    [StringLength(255)]
    public string? DefectDescription { get; set; }
}
