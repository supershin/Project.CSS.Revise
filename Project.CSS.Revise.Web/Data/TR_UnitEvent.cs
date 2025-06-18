using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_UnitEvent")]
public partial class TR_UnitEvent
{
    [Key]
    public int ID { get; set; }

    public int? EventID { get; set; }

    public Guid? UnitID { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CraeteDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("EventID")]
    [InverseProperty("TR_UnitEvents")]
    public virtual tm_Event? Event { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_UnitEvents")]
    public virtual tm_Unit? Unit { get; set; }
}
