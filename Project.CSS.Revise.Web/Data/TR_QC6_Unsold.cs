using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC6_Unsold")]
public partial class TR_QC6_Unsold
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QC_Date { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? QC_Time { get; set; }

    public Guid? SignResourceID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CloseCaseDate { get; set; }

    public int? CloseCaseBy { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ClearDate { get; set; }

    public int? ClearBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_QC6_Unsolds")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("SignResourceID")]
    [InverseProperty("TR_QC6_Unsolds")]
    public virtual TR_SignResource? SignResource { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_QC6_Unsolds")]
    public virtual tm_Unit? Unit { get; set; }
}
