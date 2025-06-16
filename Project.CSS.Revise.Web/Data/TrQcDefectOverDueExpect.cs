using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrQcDefectOverDueExpect
{
    public long Id { get; set; }

    public Guid? DefectId { get; set; }

    public DateTime? OpenDate { get; set; }

    public DateTime? InprocessDate { get; set; }

    public DateTime? ExpectDate { get; set; }

    public int? EstimateStatusId { get; set; }

    public DateTime? EstimateDate { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TrQcDefect? Defect { get; set; }

    public virtual TmExt? EstimateStatus { get; set; }
}
