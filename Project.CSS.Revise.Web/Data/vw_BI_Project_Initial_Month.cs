using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
public partial class vw_BI_Project_Initial_Month
{
    [StringLength(20)]
    [Unicode(false)]
    public string ProjectID { get; set; } = null!;

    [StringLength(500)]
    public string? ProjectName { get; set; }

    [StringLength(200)]
    public string? ProjectStatus { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? MD { get; set; }

    [StringLength(6)]
    [Unicode(false)]
    public string? YYYYMM { get; set; }

    public int? Year { get; set; }

    public int? Month { get; set; }

    [StringLength(6)]
    [Unicode(false)]
    public string? LastMonth { get; set; }
}
