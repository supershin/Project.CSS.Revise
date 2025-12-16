using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_Unit_Status")]
public partial class temp_Unit_Status
{
    [StringLength(255)]
    public string? UnitCode { get; set; }

    [StringLength(255)]
    public string? UnitStatus_CS { get; set; }

    [StringLength(255)]
    public string? RemarkUnitStatus { get; set; }
}
