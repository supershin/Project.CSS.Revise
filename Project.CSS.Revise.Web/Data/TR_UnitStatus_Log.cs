using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_UnitStatus_Log")]
public partial class TR_UnitStatus_Log
{
    [Key]
    public int ID { get; set; }

    public Guid? UnitID { get; set; }

    public int? StatusID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [ForeignKey("StatusID")]
    [InverseProperty("TR_UnitStatus_Logs")]
    public virtual tm_Ext? Status { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_UnitStatus_Logs")]
    public virtual tm_Unit? Unit { get; set; }
}
