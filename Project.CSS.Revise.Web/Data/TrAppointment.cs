using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrAppointment
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public int? AppointmentTypeId { get; set; }

    public DateTime? AppointDate { get; set; }

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public string? Remark { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? ClearDate { get; set; }

    public int? ClearBy { get; set; }

    public virtual ICollection<LineUserAppointment> LineUserAppointments { get; set; } = new List<LineUserAppointment>();

    public virtual TmProject? Project { get; set; }

    public virtual TmUnit? Unit { get; set; }
}
