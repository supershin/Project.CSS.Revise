using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrMenuRolePermission
{
    public int Id { get; set; }

    public int? QctypeId { get; set; }

    public int? MenuId { get; set; }

    public int? DepartmentId { get; set; }

    public int? RoleId { get; set; }

    public bool? View { get; set; }

    public bool? Add { get; set; }

    public bool? Update { get; set; }

    public bool? Delete { get; set; }

    public bool? Download { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmExt? Department { get; set; }

    public virtual TmMenu? Menu { get; set; }

    public virtual TmExt? Qctype { get; set; }

    public virtual TmRole? Role { get; set; }
}
