using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_MenuRolePermission")]
public partial class TR_MenuRolePermission
{
    [Key]
    public int ID { get; set; }

    public int? QCTypeID { get; set; }

    public int? MenuID { get; set; }

    public int? DepartmentID { get; set; }

    public int? RoleID { get; set; }

    public bool? View { get; set; }

    public bool? Add { get; set; }

    public bool? Update { get; set; }

    public bool? Delete { get; set; }

    public bool? Download { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("DepartmentID")]
    [InverseProperty("TR_MenuRolePermissionDepartments")]
    public virtual tm_Ext? Department { get; set; }

    [ForeignKey("MenuID")]
    [InverseProperty("TR_MenuRolePermissions")]
    public virtual tm_Menu? Menu { get; set; }

    [ForeignKey("QCTypeID")]
    [InverseProperty("TR_MenuRolePermissionQCTypes")]
    public virtual tm_Ext? QCType { get; set; }

    [ForeignKey("RoleID")]
    [InverseProperty("TR_MenuRolePermissions")]
    public virtual tm_Role? Role { get; set; }
}
