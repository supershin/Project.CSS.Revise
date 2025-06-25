using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("Line_User")]
public partial class Line_User
{
    [Key]
    [StringLength(100)]
    [Unicode(false)]
    public string UserID { get; set; } = null!;

    [StringLength(500)]
    public string? DisplayName { get; set; }

    [Unicode(false)]
    public string? PictureUrl { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Language { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("LineUser")]
    public virtual ICollection<Line_UserContract> Line_UserContracts { get; set; } = new List<Line_UserContract>();
}
