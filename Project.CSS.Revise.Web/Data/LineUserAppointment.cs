using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class LineUserAppointment
{
    public Guid Id { get; set; }

    public Guid? AppointmentId { get; set; }

    public string? LineUserId { get; set; }

    public virtual TrAppointment? Appointment { get; set; }
}
