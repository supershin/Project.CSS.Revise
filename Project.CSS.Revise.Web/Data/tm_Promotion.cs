using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Promotion")]
public partial class tm_Promotion
{
    [Key]
    public int ID { get; set; }

    public string? Name { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [InverseProperty("Promotion")]
    public virtual ICollection<TR_UnitPromotion> TR_UnitPromotions { get; set; } = new List<TR_UnitPromotion>();
}
