using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Funiture")]
public partial class tm_Funiture
{
    [Key]
    public int ID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Name { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CraeteDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("Furniture")]
    public virtual ICollection<TR_UnitFurniture_Detail> TR_UnitFurniture_Details { get; set; } = new List<TR_UnitFurniture_Detail>();
}
