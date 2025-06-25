using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_UnitFurniture")]
public partial class TR_UnitFurniture
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public Guid? UnitDocumentID { get; set; }

    public Guid? CMSignID { get; set; }

    [StringLength(1000)]
    public string? CMSignName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CMSignDate { get; set; }

    public int? CheckStatusID { get; set; }

    public string? CheckRemark { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CheckDate { get; set; }

    public int? CheckBy { get; set; }

    public Guid? CustomerSignID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CustomerSignDate { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ClearDate { get; set; }

    public int? ClearBy { get; set; }

    [ForeignKey("CMSignID")]
    [InverseProperty("TR_UnitFurnitureCMSigns")]
    public virtual TR_SignResource? CMSign { get; set; }

    [ForeignKey("CheckStatusID")]
    [InverseProperty("TR_UnitFurnitures")]
    public virtual tm_Ext? CheckStatus { get; set; }

    [ForeignKey("CustomerSignID")]
    [InverseProperty("TR_UnitFurnitureCustomerSigns")]
    public virtual TR_SignResource? CustomerSign { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_UnitFurnitures")]
    public virtual tm_Project? Project { get; set; }

    [InverseProperty("UnitFurniture")]
    public virtual ICollection<TR_UnitFurniture_Detail> TR_UnitFurniture_Details { get; set; } = new List<TR_UnitFurniture_Detail>();

    [ForeignKey("UnitID")]
    [InverseProperty("TR_UnitFurnitures")]
    public virtual tm_Unit? Unit { get; set; }

    [ForeignKey("UnitDocumentID")]
    [InverseProperty("TR_UnitFurnitures")]
    public virtual TR_UnitDocument? UnitDocument { get; set; }
}
