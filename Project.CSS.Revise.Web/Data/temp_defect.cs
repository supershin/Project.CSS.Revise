using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_defect")]
public partial class temp_defect
{
    public string? DefectArea { get; set; }

    public string? DefectType { get; set; }

    public string? DefectDescription { get; set; }
}
