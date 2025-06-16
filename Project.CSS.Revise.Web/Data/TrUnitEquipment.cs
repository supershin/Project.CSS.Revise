using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrUnitEquipment
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public Guid? UnitDocumentId { get; set; }

    public Guid? CustomerSignId { get; set; }

    public DateTime? CustomerSignDate { get; set; }

    public Guid? JmsignId { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? ClearDate { get; set; }

    public int? ClearBy { get; set; }

    public virtual TrSignResource? CustomerSign { get; set; }

    public virtual TrSignResource? Jmsign { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TmUnit? Unit { get; set; }

    public virtual TrUnitDocument? UnitDocument { get; set; }
}
