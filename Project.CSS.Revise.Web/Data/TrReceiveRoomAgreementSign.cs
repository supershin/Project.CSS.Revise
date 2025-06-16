using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrReceiveRoomAgreementSign
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public Guid? SignResourceId { get; set; }

    public DateTime? ReceiveRoomAgreementDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TrSignResource? SignResource { get; set; }

    public virtual TmUnit? Unit { get; set; }
}
