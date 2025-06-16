using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class LineUser
{
    public string UserId { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string? PictureUrl { get; set; }

    public string? Language { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<LineUserContract> LineUserContracts { get; set; } = new List<LineUserContract>();
}
