using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Event_EventType")]
public partial class TR_Event_EventType
{
    [Key]
    public int ID { get; set; }

    public int EventID { get; set; }

    public int EventTypeID { get; set; }

    [ForeignKey("EventID")]
    [InverseProperty("TR_Event_EventTypes")]
    public virtual tm_Event Event { get; set; } = null!;

    [ForeignKey("EventTypeID")]
    [InverseProperty("TR_Event_EventTypes")]
    public virtual tm_EventType EventType { get; set; } = null!;
}
