using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_UnitFurniture_Detail")]
public partial class TR_UnitFurniture_Detail
{
    [Key]
    public long ID { get; set; }

    public Guid? UnitFurnitureID { get; set; }

    public int? FurnitureID { get; set; }

    public int? Amount { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("FurnitureID")]
    [InverseProperty("TR_UnitFurniture_Details")]
    public virtual tm_Funiture? Furniture { get; set; }

    [ForeignKey("UnitFurnitureID")]
    [InverseProperty("TR_UnitFurniture_Details")]
    public virtual TR_UnitFurniture? UnitFurniture { get; set; }
}
