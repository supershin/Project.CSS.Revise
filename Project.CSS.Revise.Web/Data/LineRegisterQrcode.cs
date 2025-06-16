using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class LineRegisterQrcode
{
    public long Id { get; set; }

    public Guid? RegisterId { get; set; }

    public int? QrcodeId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? ExpireDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public Guid? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public Guid? UpdateBy { get; set; }

    public virtual LineQrcode? Qrcode { get; set; }

    public virtual LineRegister? Register { get; set; }
}
