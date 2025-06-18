using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
public partial class vw_Unit
{
    [StringLength(15)]
    public string ProjectID { get; set; } = null!;

    public string? ProjectName { get; set; }

    [StringLength(20)]
    public string? UnitCode { get; set; }

    [StringLength(50)]
    public string? Build { get; set; }

    [StringLength(50)]
    public string? Floor { get; set; }
}
