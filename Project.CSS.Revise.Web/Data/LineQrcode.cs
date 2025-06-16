using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class LineQrcode
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<LineRegisterQrcode> LineRegisterQrcodes { get; set; } = new List<LineRegisterQrcode>();
}
