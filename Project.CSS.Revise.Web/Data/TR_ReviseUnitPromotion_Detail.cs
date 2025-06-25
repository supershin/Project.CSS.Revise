using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_ReviseUnitPromotion_Detail")]
public partial class TR_ReviseUnitPromotion_Detail
{
    [Key]
    public long ID { get; set; }

    public Guid? ReviseUnitPromotionID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? PromotionID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? MPromotionID { get; set; }

    public int? PDetailID { get; set; }

    public string? PromotionDescription { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PromotionAmount { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("ReviseUnitPromotionID")]
    [InverseProperty("TR_ReviseUnitPromotion_Details")]
    public virtual TR_ReviseUnitPromotion? ReviseUnitPromotion { get; set; }
}
