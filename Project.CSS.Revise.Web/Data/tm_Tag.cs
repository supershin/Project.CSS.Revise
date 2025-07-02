using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Tag")]
public partial class tm_Tag
{
    [Key]
    public int ID { get; set; }

    [StringLength(200)]
    public string? Name { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("Tag")]
    public virtual ICollection<TR_TagEvent> TR_TagEvents { get; set; } = new List<TR_TagEvent>();
}
