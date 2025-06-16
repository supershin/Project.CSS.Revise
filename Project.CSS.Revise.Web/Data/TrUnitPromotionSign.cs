using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrUnitPromotionSign
{
    public Guid Id { get; set; }

    public Guid? UnitId { get; set; }

    public Guid? SignResourceId { get; set; }

    public Guid? IdcardResourceId { get; set; }

    public DateTime? SignDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public virtual TrSignResource? IdcardResource { get; set; }

    public virtual TrSignResource? SignResource { get; set; }

    public virtual TmUnit? Unit { get; set; }
}
