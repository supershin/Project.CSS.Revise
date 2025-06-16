using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrUnitDocument
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public int? QctypeId { get; set; }

    public int? DocumentTypeId { get; set; }

    public string? DocumentNo { get; set; }

    public int? Seq { get; set; }

    public string? OriginalFileName { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public string? MimeType { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmExt? DocumentType { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual TmExt? Qctype { get; set; }

    public virtual ICollection<TrTerminateTransferAppointDocument> TrTerminateTransferAppointDocuments { get; set; } = new List<TrTerminateTransferAppointDocument>();

    public virtual ICollection<TrUnitEquipment> TrUnitEquipments { get; set; } = new List<TrUnitEquipment>();

    public virtual ICollection<TrUnitFurniture> TrUnitFurnitures { get; set; } = new List<TrUnitFurniture>();

    public virtual TmUnit? Unit { get; set; }
}
