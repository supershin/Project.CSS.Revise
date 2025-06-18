using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[PrimaryKey("ProjectID", "UnitCode")]
[Table("temp_AVA")]
public partial class temp_AVA
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string ProjectID { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? ProjectName { get; set; }

    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string UnitCode { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Build { get; set; }

    public int? Floor { get; set; }
}
