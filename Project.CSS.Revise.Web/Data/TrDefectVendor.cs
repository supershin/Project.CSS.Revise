using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrDefectVendor
{
    public Guid DefectId { get; set; }

    public int? VendorId { get; set; }

    public DateTime? ExpectDate { get; set; }

    public DateTime? InprocessDate { get; set; }

    public int? InprocessBy { get; set; }

    public DateTime? WaitDate { get; set; }

    public DateTime? RequestQc6date { get; set; }

    public int? WaitBy { get; set; }

    public DateTime? FinishDate { get; set; }

    public int? FinishBy { get; set; }

    public DateTime? CloseDate { get; set; }

    public string? Remark { get; set; }

    public int? CloseBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }
}
