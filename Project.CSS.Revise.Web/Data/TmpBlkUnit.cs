using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("TmpBlkUnit")]
public partial class TmpBlkUnit
{
    [StringLength(100)]
    [Unicode(false)]
    public string? FREPRJNM { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? FREPRJNO { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? FSERIALNO { get; set; }

    public int? FRESTATUS { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BOOKDATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AGRDATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TRANSDATE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? FADDRNO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FCUCODE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FCUNAME { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FTEL { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FADD1 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FADD2 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FADD3 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? FPROVINCE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? FPOSTAL { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FTELNO { get; set; }
}
