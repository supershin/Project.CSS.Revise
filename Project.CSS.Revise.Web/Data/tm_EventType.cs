using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_EventType")]
public partial class tm_EventType
{
    [Key]
    public int ID { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    public int EventID { get; set; }

    [StringLength(20)]
    public string? ColorCode { get; set; }

    public bool FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CraeteDate { get; set; }

    public int CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("EventID")]
    [InverseProperty("tm_EventTypes")]
    public virtual tm_Event Event { get; set; } = null!;
}
