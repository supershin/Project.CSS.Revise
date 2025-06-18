using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("DefectTypeDesc")]
public partial class DefectTypeDesc
{
    [StringLength(255)]
    public string? DefectType { get; set; }

    [StringLength(255)]
    public string? DefectDescription { get; set; }
}
