using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_cs_response")]
public partial class temp_cs_response
{
    [StringLength(50)]
    [Unicode(false)]
    public string? UnitCode { get; set; }

    public int? UserID { get; set; }

    [StringLength(2000)]
    public string? CSResponse { get; set; }
}
