using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC4")]
public partial class TR_QC4
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public int? ResponsiblePersonID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QC_Date { get; set; }

    public int? QC_StatusID { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_QC4s")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("QC_StatusID")]
    [InverseProperty("TR_QC4s")]
    public virtual tm_Ext? QC_Status { get; set; }

    [ForeignKey("ResponsiblePersonID")]
    [InverseProperty("TR_QC4s")]
    public virtual tm_User? ResponsiblePerson { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_QC4s")]
    public virtual tm_Unit? Unit { get; set; }
}
