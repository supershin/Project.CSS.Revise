using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_UnitPromotionSign")]
public partial class TR_UnitPromotionSign
{
    [Key]
    public Guid ID { get; set; }

    public Guid? UnitID { get; set; }

    public Guid? SignResourceID { get; set; }

    public Guid? IDCardResourceID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SignDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [ForeignKey("IDCardResourceID")]
    [InverseProperty("TR_UnitPromotionSignIDCardResources")]
    public virtual TR_SignResource? IDCardResource { get; set; }

    [ForeignKey("SignResourceID")]
    [InverseProperty("TR_UnitPromotionSignSignResources")]
    public virtual TR_SignResource? SignResource { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_UnitPromotionSigns")]
    public virtual tm_Unit? Unit { get; set; }
}
