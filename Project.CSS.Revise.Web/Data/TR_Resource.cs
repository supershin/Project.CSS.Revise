using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

public partial class TR_Resource
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(500)]
    public string? OriginalFileName { get; set; }

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

    [InverseProperty("Resource")]
    public virtual ICollection<TR_QC_ContactLogResource> TR_QC_ContactLogResources { get; set; } = new List<TR_QC_ContactLogResource>();

    [InverseProperty("Resource")]
    public virtual ICollection<TR_QC_DefectResource> TR_QC_DefectResources { get; set; } = new List<TR_QC_DefectResource>();
}
