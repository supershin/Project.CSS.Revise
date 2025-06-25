using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("temp_unit")]
public partial class temp_unit
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string UnitCode { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? UnitStatus_CS { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CSResponse { get; set; }
}
