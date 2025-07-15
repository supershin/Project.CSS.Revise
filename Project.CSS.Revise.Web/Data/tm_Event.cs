using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Event")]
public partial class tm_Event
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    [StringLength(500)]
    public string? Name { get; set; }

    [StringLength(500)]
    public string? Location { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CraeteDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("tm_Events")]
    public virtual tm_Project? Project { get; set; }

    [InverseProperty("Event")]
    public virtual ICollection<TR_Event_EventType> TR_Event_EventTypes { get; set; } = new List<TR_Event_EventType>();

    [InverseProperty("Event")]
    public virtual ICollection<TR_Letter_C> TR_Letter_Cs { get; set; } = new List<TR_Letter_C>();

    [InverseProperty("Event")]
    public virtual ICollection<TR_ProjectEvent> TR_ProjectEvents { get; set; } = new List<TR_ProjectEvent>();

    [InverseProperty("Event")]
    public virtual ICollection<TR_ProjectShopEvent> TR_ProjectShopEvents { get; set; } = new List<TR_ProjectShopEvent>();

    [InverseProperty("Event")]
    public virtual ICollection<TR_TagEvent> TR_TagEvents { get; set; } = new List<TR_TagEvent>();

    [InverseProperty("Event")]
    public virtual ICollection<TR_UnitEvent> TR_UnitEvents { get; set; } = new List<TR_UnitEvent>();
}
