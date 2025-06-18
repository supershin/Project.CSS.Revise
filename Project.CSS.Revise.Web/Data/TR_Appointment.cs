using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Appointment")]
public partial class TR_Appointment
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public int? AppointmentTypeID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AppointDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? StartTime { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? EndTime { get; set; }

    public string? Remark { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ClearDate { get; set; }

    public int? ClearBy { get; set; }

    [InverseProperty("Appointment")]
    public virtual ICollection<Line_UserAppointment> Line_UserAppointments { get; set; } = new List<Line_UserAppointment>();

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_Appointments")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_Appointments")]
    public virtual tm_Unit? Unit { get; set; }
}
