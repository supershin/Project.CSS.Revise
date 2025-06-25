using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("Line_Register")]
public partial class Line_Register
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? LineUserID { get; set; }

    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(250)]
    public string? LastName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RegisterDate { get; set; }

    public bool? FlagAccept { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public Guid? CreatBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public Guid? UpdateBy { get; set; }

    [InverseProperty("Register")]
    public virtual ICollection<Line_RegisterQRCode> Line_RegisterQRCodes { get; set; } = new List<Line_RegisterQRCode>();
}
