using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("DefectArea")]
public partial class DefectArea
{
    [Column("DefectArea")]
    [StringLength(255)]
    public string? DefectArea1 { get; set; }
}
