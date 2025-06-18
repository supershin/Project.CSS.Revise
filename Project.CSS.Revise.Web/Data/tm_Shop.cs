using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Shop")]
public partial class tm_Shop
{
    [Key]
    public int ID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Name { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("Shop")]
    public virtual ICollection<TR_ProjectShopEvent> TR_ProjectShopEvents { get; set; } = new List<TR_ProjectShopEvent>();
}
