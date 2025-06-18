using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Letter_Lot_Detail")]
[Index("LetterNo", Name = "NonClusteredIndex-20220420-133358", IsUnique = true)]
[Index("LetterNo", Name = "NonClusteredIndex-20220505-085754", IsUnique = true)]
public partial class TR_Letter_Lot_Detail
{
    [Key]
    public Guid ID { get; set; }

    public Guid? LotID { get; set; }

    public Guid? LetterID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? LetterNo { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("LetterID")]
    [InverseProperty("TR_Letter_Lot_Details")]
    public virtual TR_Letter? Letter { get; set; }

    [ForeignKey("LotID")]
    [InverseProperty("TR_Letter_Lot_Details")]
    public virtual TR_Letter_Lot? Lot { get; set; }
}
