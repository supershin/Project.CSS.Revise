using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_ReceiveRoomSign")]
public partial class TR_ReceiveRoomSign
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public Guid? SignResourceID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReceiveRoomDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_ReceiveRoomSigns")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("SignResourceID")]
    [InverseProperty("TR_ReceiveRoomSigns")]
    public virtual TR_SignResource? SignResource { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_ReceiveRoomSigns")]
    public virtual tm_Unit? Unit { get; set; }
}
