using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_SignResource")]
public partial class TR_SignResource
{
    [Key]
    public Guid ID { get; set; }

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

    [InverseProperty("ApproveSign")]
    public virtual ICollection<TR_Letter> TR_Letters { get; set; } = new List<TR_Letter>();

    [InverseProperty("SignResource")]
    public virtual ICollection<TR_QC5> TR_QC5s { get; set; } = new List<TR_QC5>();

    [InverseProperty("SignResource")]
    public virtual ICollection<TR_QC6_Unsold> TR_QC6_Unsolds { get; set; } = new List<TR_QC6_Unsold>();

    [InverseProperty("SignResource")]
    public virtual ICollection<TR_QC6> TR_QC6s { get; set; } = new List<TR_QC6>();

    [InverseProperty("SignResource")]
    public virtual ICollection<TR_ReceiveRoomAgreementSign> TR_ReceiveRoomAgreementSigns { get; set; } = new List<TR_ReceiveRoomAgreementSign>();

    [InverseProperty("SignResource")]
    public virtual ICollection<TR_ReceiveRoomSign> TR_ReceiveRoomSigns { get; set; } = new List<TR_ReceiveRoomSign>();

    [InverseProperty("CustomerSign")]
    public virtual ICollection<TR_UnitEquipment> TR_UnitEquipmentCustomerSigns { get; set; } = new List<TR_UnitEquipment>();

    [InverseProperty("JMSign")]
    public virtual ICollection<TR_UnitEquipment> TR_UnitEquipmentJMSigns { get; set; } = new List<TR_UnitEquipment>();

    [InverseProperty("CMSign")]
    public virtual ICollection<TR_UnitFurniture> TR_UnitFurnitureCMSigns { get; set; } = new List<TR_UnitFurniture>();

    [InverseProperty("CustomerSign")]
    public virtual ICollection<TR_UnitFurniture> TR_UnitFurnitureCustomerSigns { get; set; } = new List<TR_UnitFurniture>();

    [InverseProperty("IDCardResource")]
    public virtual ICollection<TR_UnitPromotionSign> TR_UnitPromotionSignIDCardResources { get; set; } = new List<TR_UnitPromotionSign>();

    [InverseProperty("SignResource")]
    public virtual ICollection<TR_UnitPromotionSign> TR_UnitPromotionSignSignResources { get; set; } = new List<TR_UnitPromotionSign>();

    [InverseProperty("SignResource")]
    public virtual ICollection<TR_UserSignResource> TR_UserSignResources { get; set; } = new List<TR_UserSignResource>();
}
