using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("Line_QRCode")]
public partial class Line_QRCode
{
    [Key]
    public int ID { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Code { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UpdateBy { get; set; }

    [InverseProperty("QRCode")]
    public virtual ICollection<Line_RegisterQRCode> Line_RegisterQRCodes { get; set; } = new List<Line_RegisterQRCode>();
}
