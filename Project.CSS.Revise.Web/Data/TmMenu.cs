using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmMenu
{
    public int Id { get; set; }

    public int? QctypeId { get; set; }

    public string? Name { get; set; }

    public int? ParentId { get; set; }

    public string? ControllerName { get; set; }

    public string? ActionName { get; set; }

    public string? Url { get; set; }

    public string? Icon { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmExt? Qctype { get; set; }

    public virtual ICollection<TrMenuRolePermission> TrMenuRolePermissions { get; set; } = new List<TrMenuRolePermission>();
}
