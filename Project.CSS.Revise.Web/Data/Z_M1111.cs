using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("Z_M1111")]
public partial class Z_M1111
{
    public Guid? id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? FSERIALNO { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? FADDRNO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FCUNAME { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FTEL { get; set; }
}
