using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_ReviseUnitPromotion")]
public partial class TR_ReviseUnitPromotion
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public Guid? UnitDocumentID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ContractNumber { get; set; }

    [StringLength(500)]
    public string? ContractCustomerName { get; set; }

    [StringLength(500)]
    public string? ContractCustomerSureName { get; set; }

    public string? ContractAddress { get; set; }

    [StringLength(100)]
    public string? ContractMoo { get; set; }

    [StringLength(100)]
    public string? ContractSoi { get; set; }

    [StringLength(100)]
    public string? ContractRoad { get; set; }

    [StringLength(100)]
    public string? ContractSubdistrict { get; set; }

    [StringLength(100)]
    public string? ContractDistrict { get; set; }

    [StringLength(100)]
    public string? ContractProvince { get; set; }

    [StringLength(10)]
    public string? ContractPostalCode { get; set; }

    [StringLength(100)]
    public string? ContractMobile { get; set; }

    public int? ApproveStatusID { get; set; }

    public string? ApproveRemark { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ApproveDate { get; set; }

    public int? ApproveBy { get; set; }

    public int? ApproveStatusID_2 { get; set; }

    public string? ApproveRemark_2 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ApproveDate_2 { get; set; }

    public int? ApproveBy_2 { get; set; }

    public Guid? CustomerSignID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CustomerSignDate { get; set; }

    public Guid? CustomerResourceID { get; set; }

    public int? CustomerBankID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CustomerBookBank { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PrintDate { get; set; }

    public int? PrintBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SendMailDate { get; set; }

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

    [ForeignKey("ApproveStatusID")]
    [InverseProperty("TR_ReviseUnitPromotionApproveStatuses")]
    public virtual tm_Ext? ApproveStatus { get; set; }

    [ForeignKey("ApproveStatusID_2")]
    [InverseProperty("TR_ReviseUnitPromotionApproveStatusID_2Navigations")]
    public virtual tm_Ext? ApproveStatusID_2Navigation { get; set; }

    [ForeignKey("CustomerBankID")]
    [InverseProperty("TR_ReviseUnitPromotions")]
    public virtual tm_Bank? CustomerBank { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_ReviseUnitPromotions")]
    public virtual tm_Project? Project { get; set; }

    [InverseProperty("ReviseUnitPromotion")]
    public virtual ICollection<TR_ReviseUnitPromotion_Detail> TR_ReviseUnitPromotion_Details { get; set; } = new List<TR_ReviseUnitPromotion_Detail>();

    [ForeignKey("UnitID")]
    [InverseProperty("TR_ReviseUnitPromotions")]
    public virtual tm_Unit? Unit { get; set; }
}
