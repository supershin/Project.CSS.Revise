using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrRemarkUnitStatusMapping
{
    public int Id { get; set; }

    public int UnitStatusCsId { get; set; }

    public int RemarkUnitStatusCsId { get; set; }

    public virtual TmExt RemarkUnitStatusCs { get; set; } = null!;

    public virtual TmExt UnitStatusCs { get; set; } = null!;
}
