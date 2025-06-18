using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_UnitStatus")]
public partial class tm_UnitStatus
{
    [Key]
    public int ID { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [InverseProperty("UnitStatusNavigation")]
    public virtual ICollection<tm_Unit> tm_Units { get; set; } = new List<tm_Unit>();
}
