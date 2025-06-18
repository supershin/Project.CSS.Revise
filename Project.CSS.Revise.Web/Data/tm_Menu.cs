using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Menu")]
public partial class tm_Menu
{
    [Key]
    public int ID { get; set; }

    public int? QCTypeID { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public int? ParentID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ControllerName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ActionName { get; set; }

    [Unicode(false)]
    public string? Url { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Icon { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("QCTypeID")]
    [InverseProperty("tm_Menus")]
    public virtual tm_Ext? QCType { get; set; }

    [InverseProperty("Menu")]
    public virtual ICollection<TR_MenuRolePermission> TR_MenuRolePermissions { get; set; } = new List<TR_MenuRolePermission>();
}
