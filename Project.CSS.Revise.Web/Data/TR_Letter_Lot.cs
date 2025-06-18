using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Letter_Lot")]
[Index("LotNo", Name = "NonClusteredIndex-20220420-133306", IsUnique = true)]
[Index("LotNo", Name = "NonClusteredIndex-20220505-085742", IsUnique = true)]
public partial class TR_Letter_Lot
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? LotNo { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("Lot")]
    public virtual ICollection<TR_Letter_Lot_Detail> TR_Letter_Lot_Details { get; set; } = new List<TR_Letter_Lot_Detail>();

    [InverseProperty("Lot")]
    public virtual ICollection<TR_Letter_Lot_Resource> TR_Letter_Lot_Resources { get; set; } = new List<TR_Letter_Lot_Resource>();
}
