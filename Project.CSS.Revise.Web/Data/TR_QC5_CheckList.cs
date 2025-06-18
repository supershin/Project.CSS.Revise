using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC5_CheckList")]
public partial class TR_QC5_CheckList
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CheckDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ScoreResult { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_QC5_CheckLists")]
    public virtual tm_Project? Project { get; set; }

    [InverseProperty("QC5CheckList")]
    public virtual ICollection<TR_QC5_CheckList_Detail> TR_QC5_CheckList_Details { get; set; } = new List<TR_QC5_CheckList_Detail>();

    [ForeignKey("UnitID")]
    [InverseProperty("TR_QC5_CheckLists")]
    public virtual tm_Unit? Unit { get; set; }
}
