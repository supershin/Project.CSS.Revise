using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Unsold_Round")]
public partial class TR_Unsold_Round
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RoundDate { get; set; }

    public bool? FlagSendMail { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_Unsold_Rounds")]
    public virtual tm_Project? Project { get; set; }

    [InverseProperty("Round")]
    public virtual ICollection<TR_Unsold_RoundUnit> TR_Unsold_RoundUnits { get; set; } = new List<TR_Unsold_RoundUnit>();
}
