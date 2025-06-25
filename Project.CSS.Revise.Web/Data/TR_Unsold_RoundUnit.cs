using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Unsold_RoundUnit")]
public partial class TR_Unsold_RoundUnit
{
    [Key]
    public long ID { get; set; }

    public int? RoundID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_Unsold_RoundUnits")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("RoundID")]
    [InverseProperty("TR_Unsold_RoundUnits")]
    public virtual TR_Unsold_Round? Round { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_Unsold_RoundUnits")]
    public virtual tm_Unit? Unit { get; set; }
}
