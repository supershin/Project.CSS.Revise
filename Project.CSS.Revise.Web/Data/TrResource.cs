using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrResource
{
    public Guid Id { get; set; }

    public string? OriginalFileName { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public string? MimeType { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<TrQcContactLogResource> TrQcContactLogResources { get; set; } = new List<TrQcContactLogResource>();

    public virtual ICollection<TrQcDefectResource> TrQcDefectResources { get; set; } = new List<TrQcDefectResource>();
}
