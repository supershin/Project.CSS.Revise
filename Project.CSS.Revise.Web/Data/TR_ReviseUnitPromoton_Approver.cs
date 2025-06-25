using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_ReviseUnitPromoton_Approver")]
public partial class TR_ReviseUnitPromoton_Approver
{
    [Key]
    public int ID { get; set; }

    public int? ApproveRoleID { get; set; }

    public int? UserID { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBY { get; set; }

    [ForeignKey("ApproveRoleID")]
    [InverseProperty("TR_ReviseUnitPromoton_Approvers")]
    public virtual tm_Ext? ApproveRole { get; set; }

    [ForeignKey("UserID")]
    [InverseProperty("TR_ReviseUnitPromoton_Approvers")]
    public virtual tm_User? User { get; set; }
}
