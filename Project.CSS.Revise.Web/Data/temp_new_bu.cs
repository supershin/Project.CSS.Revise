using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_new_bu")]
public partial class temp_new_bu
{
    [StringLength(50)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    [StringLength(400)]
    public string? ProjectName { get; set; }

    public int? BUID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BUName { get; set; }
}
