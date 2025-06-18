using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_UserPosition")]
public partial class TR_UserPosition
{
    [Key]
    public int ID { get; set; }

    public int? UserID { get; set; }

    public int? PositionID { get; set; }

    [ForeignKey("PositionID")]
    [InverseProperty("TR_UserPositions")]
    public virtual tm_Position? Position { get; set; }

    [ForeignKey("UserID")]
    [InverseProperty("TR_UserPositions")]
    public virtual tm_User? User { get; set; }
}
