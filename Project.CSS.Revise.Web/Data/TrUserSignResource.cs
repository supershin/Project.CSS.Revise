using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrUserSignResource
{
    public int UserId { get; set; }

    public Guid? SignResourceId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TrSignResource? SignResource { get; set; }

    public virtual TmUser User { get; set; } = null!;
}
