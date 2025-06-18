using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_ProjectShopEvent")]
public partial class TR_ProjectShopEvent
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public int? ShopID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EventDate { get; set; }

    public int? UnitQuota { get; set; }

    public int? ShopQuota { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_ProjectShopEvents")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("ShopID")]
    [InverseProperty("TR_ProjectShopEvents")]
    public virtual tm_Shop? Shop { get; set; }
}
