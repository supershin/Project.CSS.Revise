using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrUnitEquipmentDetail
{
    public long Id { get; set; }

    public Guid? UnitEquipmentId { get; set; }

    public int? EquipmentId { get; set; }

    public int? Amount { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }
}
