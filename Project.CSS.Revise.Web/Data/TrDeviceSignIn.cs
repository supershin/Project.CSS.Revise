using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrDeviceSignIn
{
    public Guid Id { get; set; }

    public string? DeviceId { get; set; }

    public int? UserId { get; set; }

    public DateTime? SignInDate { get; set; }

    public DateTime? SignOutDate { get; set; }

    public bool? IsSignIn { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmUser? User { get; set; }
}
