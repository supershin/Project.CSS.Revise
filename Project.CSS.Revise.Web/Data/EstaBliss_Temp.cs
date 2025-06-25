using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("EstaBliss_Temp")]
public partial class EstaBliss_Temp
{
    [StringLength(255)]
    public string? UnitCode { get; set; }

    [StringLength(255)]
    public string? AdjustStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpectDate { get; set; }

    [StringLength(255)]
    public string? Transfer { get; set; }
}
