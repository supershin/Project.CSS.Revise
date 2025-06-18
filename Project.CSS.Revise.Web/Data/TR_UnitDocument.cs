using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_UnitDocument")]
public partial class TR_UnitDocument
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public int? QCTypeID { get; set; }

    public int? DocumentTypeID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? DocumentNo { get; set; }

    public int? Seq { get; set; }

    [StringLength(500)]
    public string? OriginalFileName { get; set; }

    [StringLength(500)]
    public string? FileName { get; set; }

    [StringLength(500)]
    public string? FilePath { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? MimeType { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("DocumentTypeID")]
    [InverseProperty("TR_UnitDocumentDocumentTypes")]
    public virtual tm_Ext? DocumentType { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_UnitDocuments")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("QCTypeID")]
    [InverseProperty("TR_UnitDocumentQCTypes")]
    public virtual tm_Ext? QCType { get; set; }

    [InverseProperty("UnitDocument")]
    public virtual ICollection<TR_TerminateTransferAppoint_Document> TR_TerminateTransferAppoint_Documents { get; set; } = new List<TR_TerminateTransferAppoint_Document>();

    [InverseProperty("UnitDocument")]
    public virtual ICollection<TR_UnitEquipment> TR_UnitEquipments { get; set; } = new List<TR_UnitEquipment>();

    [InverseProperty("UnitDocument")]
    public virtual ICollection<TR_UnitFurniture> TR_UnitFurnitures { get; set; } = new List<TR_UnitFurniture>();

    [ForeignKey("UnitID")]
    [InverseProperty("TR_UnitDocuments")]
    public virtual tm_Unit? Unit { get; set; }
}
