using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_UnitPromotion")]
public partial class TR_UnitPromotion
{
    [Key]
    public long ID { get; set; }

    public Guid? UnitID { get; set; }

    public int? PromotionID { get; set; }

    public bool? IsUsed { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("PromotionID")]
    [InverseProperty("TR_UnitPromotions")]
    public virtual tm_Promotion? Promotion { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_UnitPromotions")]
    public virtual tm_Unit? Unit { get; set; }
}
