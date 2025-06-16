using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmUser
{
    public int Id { get; set; }

    public int? QctypeId { get; set; }

    public string? UserId { get; set; }

    public int? TitleId { get; set; }

    public string? FirstName { get; set; }

    public string? FirstNameEng { get; set; }

    public int? TitleIdEng { get; set; }

    public string? LastName { get; set; }

    public string? LastNameEng { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public int? DepartmentId { get; set; }

    public int? RoleId { get; set; }

    public bool? FlagAdmin { get; set; }

    public bool? FlagReadonly { get; set; }

    public bool? FlagActive { get; set; }

    public bool? IsQcfinishPlan { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<BmTrUserAdmin> BmTrUserAdmins { get; set; } = new List<BmTrUserAdmin>();

    public virtual TmExt? Qctype { get; set; }

    public virtual TmRole? Role { get; set; }

    public virtual TmTitleName? Title { get; set; }

    public virtual TmTitleName? TitleIdEngNavigation { get; set; }

    public virtual ICollection<TmProjectUserMapping> TmProjectUserMappings { get; set; } = new List<TmProjectUserMapping>();

    public virtual ICollection<TmResponsibleMapping> TmResponsibleMappings { get; set; } = new List<TmResponsibleMapping>();

    public virtual ICollection<TrCustomerSatisfaction> TrCustomerSatisfactions { get; set; } = new List<TrCustomerSatisfaction>();

    public virtual ICollection<TrDeviceSignIn> TrDeviceSignIns { get; set; } = new List<TrDeviceSignIn>();

    public virtual ICollection<TrLetterC> TrLetterCs { get; set; } = new List<TrLetterC>();

    public virtual ICollection<TrProjectUserSign> TrProjectUserSigns { get; set; } = new List<TrProjectUserSign>();

    public virtual ICollection<TrQc1> TrQc1s { get; set; } = new List<TrQc1>();

    public virtual ICollection<TrQc2> TrQc2s { get; set; } = new List<TrQc2>();

    public virtual ICollection<TrQc3> TrQc3s { get; set; } = new List<TrQc3>();

    public virtual ICollection<TrQc4> TrQc4s { get; set; } = new List<TrQc4>();

    public virtual ICollection<TrQc5> TrQc5s { get; set; } = new List<TrQc5>();

    public virtual ICollection<TrQc6> TrQc6s { get; set; } = new List<TrQc6>();

    public virtual ICollection<TrQcDefectOverDueExpectUserPermission> TrQcDefectOverDueExpectUserPermissions { get; set; } = new List<TrQcDefectOverDueExpectUserPermission>();

    public virtual ICollection<TrRegisterLog> TrRegisterLogs { get; set; } = new List<TrRegisterLog>();

    public virtual ICollection<TrReviseUnitPromotonApprover> TrReviseUnitPromotonApprovers { get; set; } = new List<TrReviseUnitPromotonApprover>();

    public virtual ICollection<TrUserPosition> TrUserPositions { get; set; } = new List<TrUserPosition>();

    public virtual TrUserSignResource? TrUserSignResource { get; set; }
}
