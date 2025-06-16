using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrUserPasswordChange
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Password { get; set; }

    public DateTime? ChangeDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public virtual PrUser? User { get; set; }
}
