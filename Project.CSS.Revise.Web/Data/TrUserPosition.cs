using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrUserPosition
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? PositionId { get; set; }

    public virtual TmPosition? Position { get; set; }

    public virtual TmUser? User { get; set; }
}
