using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrUser
{
    public int Id { get; set; }

    public int? UserTypeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public bool? ConsentAccept { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<PrUserBankMapping> PrUserBankMappings { get; set; } = new List<PrUserBankMapping>();

    public virtual ICollection<PrUserPasswordChange> PrUserPasswordChanges { get; set; } = new List<PrUserPasswordChange>();

    public virtual TmExt? UserType { get; set; }
}
