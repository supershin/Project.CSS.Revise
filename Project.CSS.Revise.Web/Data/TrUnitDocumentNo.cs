using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrUnitDocumentNo
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public int? QctypeId { get; set; }

    public Guid? QcId { get; set; }

    public int? DocumentTypeId { get; set; }

    public int? DocumentRunning { get; set; }

    public string? DocumentNo { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmExt? DocumentType { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TmExt? Qctype { get; set; }

    public virtual TmUnit? Unit { get; set; }
}
