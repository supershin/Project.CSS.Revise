using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC_CheckList")]
public partial class TR_QC_CheckList
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public int? QCTypeID { get; set; }

    public Guid? QC_ID { get; set; }

    public int? TopicID { get; set; }

    public int? SubjectID { get; set; }

    public int? SubjectStatus { get; set; }

    public string? Remark { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_QC_CheckLists")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("QCTypeID")]
    [InverseProperty("TR_QC_CheckLists")]
    public virtual tm_Ext? QCType { get; set; }

    [ForeignKey("SubjectID")]
    [InverseProperty("TR_QC_CheckLists")]
    public virtual tm_Subject? Subject { get; set; }

    [ForeignKey("TopicID")]
    [InverseProperty("TR_QC_CheckLists")]
    public virtual tm_Topic? Topic { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_QC_CheckLists")]
    public virtual tm_Unit? Unit { get; set; }
}
