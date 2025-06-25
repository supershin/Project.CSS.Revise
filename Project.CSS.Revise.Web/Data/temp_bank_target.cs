using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_bank_target")]
public partial class temp_bank_target
{
    [StringLength(50)]
    [Unicode(false)]
    public string? Bank { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Target { get; set; }

    public int? Yearly { get; set; }
}
