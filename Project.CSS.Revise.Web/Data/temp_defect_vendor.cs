using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_defect_vendor")]
public partial class temp_defect_vendor
{
    public double? DefectTypeID { get; set; }

    [StringLength(255)]
    public string? DefectTypeName { get; set; }

    public double? DefectDescID { get; set; }

    [StringLength(255)]
    public string? DefectDescName { get; set; }

    [StringLength(255)]
    public string? Vandor { get; set; }
}
