using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class LineRegister
{
    public Guid Id { get; set; }

    public string? LineUserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public DateTime? RegisterDate { get; set; }

    public bool? FlagAccept { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public Guid? CreatBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public Guid? UpdateBy { get; set; }

    public virtual ICollection<LineRegisterQrcode> LineRegisterQrcodes { get; set; } = new List<LineRegisterQrcode>();
}
