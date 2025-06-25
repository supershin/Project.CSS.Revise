using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("Line_UserAppointment")]
public partial class Line_UserAppointment
{
    [Key]
    public Guid ID { get; set; }

    public Guid? AppointmentID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? LineUserID { get; set; }

    [ForeignKey("AppointmentID")]
    [InverseProperty("Line_UserAppointments")]
    public virtual TR_Appointment? Appointment { get; set; }
}
