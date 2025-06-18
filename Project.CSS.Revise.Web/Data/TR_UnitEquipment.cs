using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_UnitEquipment")]
public partial class TR_UnitEquipment
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public Guid? UnitDocumentID { get; set; }

    public Guid? CustomerSignID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CustomerSignDate { get; set; }

    public Guid? JMSignID { get; set; }

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

    [ForeignKey("CustomerSignID")]
    [InverseProperty("TR_UnitEquipmentCustomerSigns")]
    public virtual TR_SignResource? CustomerSign { get; set; }

    [ForeignKey("JMSignID")]
    [InverseProperty("TR_UnitEquipmentJMSigns")]
    public virtual TR_SignResource? JMSign { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_UnitEquipments")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_UnitEquipments")]
    public virtual tm_Unit? Unit { get; set; }

    [ForeignKey("UnitDocumentID")]
    [InverseProperty("TR_UnitEquipments")]
    public virtual TR_UnitDocument? UnitDocument { get; set; }
}
