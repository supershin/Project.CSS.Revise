using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_unit_cmd005")]
public partial class temp_unit_cmd005
{
    [StringLength(255)]
    public string? ProjectID { get; set; }

    [StringLength(255)]
    public string? UnitCode { get; set; }
}
