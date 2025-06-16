using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrSignResource
{
    public Guid Id { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public string? MimeType { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<TrLetter> TrLetters { get; set; } = new List<TrLetter>();

    public virtual ICollection<TrQc5> TrQc5s { get; set; } = new List<TrQc5>();

    public virtual ICollection<TrQc6Unsold> TrQc6Unsolds { get; set; } = new List<TrQc6Unsold>();

    public virtual ICollection<TrQc6> TrQc6s { get; set; } = new List<TrQc6>();

    public virtual ICollection<TrReceiveRoomAgreementSign> TrReceiveRoomAgreementSigns { get; set; } = new List<TrReceiveRoomAgreementSign>();

    public virtual ICollection<TrReceiveRoomSign> TrReceiveRoomSigns { get; set; } = new List<TrReceiveRoomSign>();

    public virtual ICollection<TrUnitEquipment> TrUnitEquipmentCustomerSigns { get; set; } = new List<TrUnitEquipment>();

    public virtual ICollection<TrUnitEquipment> TrUnitEquipmentJmsigns { get; set; } = new List<TrUnitEquipment>();

    public virtual ICollection<TrUnitFurniture> TrUnitFurnitureCmsigns { get; set; } = new List<TrUnitFurniture>();

    public virtual ICollection<TrUnitFurniture> TrUnitFurnitureCustomerSigns { get; set; } = new List<TrUnitFurniture>();

    public virtual ICollection<TrUnitPromotionSign> TrUnitPromotionSignIdcardResources { get; set; } = new List<TrUnitPromotionSign>();

    public virtual ICollection<TrUnitPromotionSign> TrUnitPromotionSignSignResources { get; set; } = new List<TrUnitPromotionSign>();

    public virtual ICollection<TrUserSignResource> TrUserSignResources { get; set; } = new List<TrUserSignResource>();
}
