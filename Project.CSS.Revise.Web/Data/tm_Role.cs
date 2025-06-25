using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Role")]
public partial class tm_Role
{
    [Key]
    public int ID { get; set; }

    public int? QCTypeID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("QCTypeID")]
    [InverseProperty("tm_Roles")]
    public virtual tm_Ext? QCType { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<TR_MenuRolePermission> TR_MenuRolePermissions { get; set; } = new List<TR_MenuRolePermission>();

    [InverseProperty("Role")]
    public virtual ICollection<tm_User> tm_Users { get; set; } = new List<tm_User>();
}
