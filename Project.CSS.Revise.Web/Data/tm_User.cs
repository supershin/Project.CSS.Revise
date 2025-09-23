using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_User")]
[Index("UserID", "FirstName", Name = "NonClusteredIndex-20190528-085408", IsUnique = true)]
public partial class tm_User
{
    [Key]
    public int ID { get; set; }

    public int? QCTypeID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? UserID { get; set; }

    public int? TitleID { get; set; }

    [StringLength(200)]
    public string? FirstName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? FirstName_Eng { get; set; }

    public int? TitleID_Eng { get; set; }

    [StringLength(200)]
    public string? LastName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? LastName_Eng { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Password { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? Mobile { get; set; }

    public int? DepartmentID { get; set; }

    public int? RoleID { get; set; }

    public bool? FlagAdmin { get; set; }

    public bool? FlagReadonly { get; set; }

    public bool? FlagActive { get; set; }

    public bool? IsQCFinishPlan { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<BM_TR_UserAdmin> BM_TR_UserAdmins { get; set; } = new List<BM_TR_UserAdmin>();

    [ForeignKey("QCTypeID")]
    [InverseProperty("tm_Users")]
    public virtual tm_Ext? QCType { get; set; }

    [ForeignKey("RoleID")]
    [InverseProperty("tm_Users")]
    public virtual tm_Role? Role { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<TR_CustomerSatisfaction> TR_CustomerSatisfactions { get; set; } = new List<TR_CustomerSatisfaction>();

    [InverseProperty("User")]
    public virtual ICollection<TR_DeviceSignIn> TR_DeviceSignIns { get; set; } = new List<TR_DeviceSignIn>();

    [InverseProperty("SignUser")]
    public virtual ICollection<TR_Letter_C> TR_Letter_Cs { get; set; } = new List<TR_Letter_C>();

    [InverseProperty("User")]
    public virtual ICollection<TR_ProjectUserSign> TR_ProjectUserSigns { get; set; } = new List<TR_ProjectUserSign>();

    [InverseProperty("ResponsiblePerson")]
    public virtual ICollection<TR_QC1> TR_QC1s { get; set; } = new List<TR_QC1>();

    [InverseProperty("ResponsiblePerson")]
    public virtual ICollection<TR_QC2> TR_QC2s { get; set; } = new List<TR_QC2>();

    [InverseProperty("ResponsiblePerson")]
    public virtual ICollection<TR_QC3> TR_QC3s { get; set; } = new List<TR_QC3>();

    [InverseProperty("ResponsiblePerson")]
    public virtual ICollection<TR_QC4> TR_QC4s { get; set; } = new List<TR_QC4>();

    [InverseProperty("ResponsiblePerson")]
    public virtual ICollection<TR_QC5> TR_QC5s { get; set; } = new List<TR_QC5>();

    [InverseProperty("ResponsiblePerson")]
    public virtual ICollection<TR_QC6> TR_QC6s { get; set; } = new List<TR_QC6>();

    [InverseProperty("User")]
    public virtual ICollection<TR_QC_Defect_OverDueExpect_UserPermission> TR_QC_Defect_OverDueExpect_UserPermissions { get; set; } = new List<TR_QC_Defect_OverDueExpect_UserPermission>();

    [InverseProperty("Responsible")]
    public virtual ICollection<TR_RegisterLog> TR_RegisterLogs { get; set; } = new List<TR_RegisterLog>();

    [InverseProperty("User")]
    public virtual ICollection<TR_ReviseUnitPromoton_Approver> TR_ReviseUnitPromoton_Approvers { get; set; } = new List<TR_ReviseUnitPromoton_Approver>();

    [InverseProperty("User")]
    public virtual ICollection<TR_UserPosition> TR_UserPositions { get; set; } = new List<TR_UserPosition>();

    [InverseProperty("User")]
    public virtual TR_UserSignResource? TR_UserSignResource { get; set; }

    [ForeignKey("TitleID")]
    [InverseProperty("tm_UserTitles")]
    public virtual tm_TitleName? Title { get; set; }

    [ForeignKey("TitleID_Eng")]
    [InverseProperty("tm_UserTitleID_EngNavigations")]
    public virtual tm_TitleName? TitleID_EngNavigation { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<tm_BU_Mapping> tm_BU_Mappings { get; set; } = new List<tm_BU_Mapping>();

    [InverseProperty("User")]
    public virtual ICollection<tm_ProjectUser_Mapping> tm_ProjectUser_Mappings { get; set; } = new List<tm_ProjectUser_Mapping>();

    [InverseProperty("UserID_MappingNavigation")]
    public virtual ICollection<tm_Responsible_Mapping> tm_Responsible_Mappings { get; set; } = new List<tm_Responsible_Mapping>();
}
