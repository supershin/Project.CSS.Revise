using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrTerminateTransferAppoint
{
    public Guid Id { get; set; }

    public Guid? UnitId { get; set; }

    public DateTime? TerminateDate { get; set; }

    public int? TerminateStatusId { get; set; }

    public string? Detail { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpDateBy { get; set; }

    public virtual TmExt? TerminateStatus { get; set; }

    public virtual ICollection<TrTerminateTransferAppointDocument> TrTerminateTransferAppointDocuments { get; set; } = new List<TrTerminateTransferAppointDocument>();

    public virtual TmUnit? Unit { get; set; }
}
