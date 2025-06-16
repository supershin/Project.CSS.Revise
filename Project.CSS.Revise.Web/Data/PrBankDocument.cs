using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class PrBankDocument
{
    public Guid Id { get; set; }

    public int? BankId { get; set; }

    public string? DocumentName { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public string? MimeType { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual TmBank? Bank { get; set; }
}
