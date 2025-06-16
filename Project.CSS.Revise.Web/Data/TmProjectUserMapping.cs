using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmProjectUserMapping
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public int? UserId { get; set; }

    public int? GroupUserId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmExt? GroupUser { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TmUser? User { get; set; }
}
