using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_UnitDocumentNo")]
public partial class TR_UnitDocumentNo
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public int? QCTypeID { get; set; }

    public Guid? QC_ID { get; set; }

    public int? DocumentTypeID { get; set; }

    public int? DocumentRunning { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? DocumentNo { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("DocumentTypeID")]
    [InverseProperty("TR_UnitDocumentNoDocumentTypes")]
    public virtual tm_Ext? DocumentType { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_UnitDocumentNos")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("QCTypeID")]
    [InverseProperty("TR_UnitDocumentNoQCTypes")]
    public virtual tm_Ext? QCType { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_UnitDocumentNos")]
    public virtual tm_Unit? Unit { get; set; }
}
