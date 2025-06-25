using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_line")]
public partial class temp_line
{
    public int? ID { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? LineKey { get; set; }
}
