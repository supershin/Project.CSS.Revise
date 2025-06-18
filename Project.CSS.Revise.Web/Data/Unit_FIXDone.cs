using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("Unit_FIXDone")]
public partial class Unit_FIXDone
{
    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? UnitCode { get; set; }

    [StringLength(500)]
    public string? CustomerName { get; set; }

    public int? CntInspect { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? InspectStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReceiveRoomDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransferDate { get; set; }
}
