using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrAttachFile
{
    public Guid Id { get; set; }

    public int? UserTypeId { get; set; }

    public string? IdcardNo { get; set; }

    public int? AttachTypeId { get; set; }

    public int? Seq { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public string? MimeType { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual TmExt? AttachType { get; set; }

    public virtual ICollection<PrLoanCustomerAttach> PrLoanCustomerAttaches { get; set; } = new List<PrLoanCustomerAttach>();

    public virtual TmExt? UserType { get; set; }
}
