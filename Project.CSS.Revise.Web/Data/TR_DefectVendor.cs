using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_DefectVendor")]
[Index("DefectID", Name = "NonClusteredIndex-20230725-112141")]
public partial class TR_DefectVendor
{
    [Key]
    public Guid DefectID { get; set; }

    public int? VendorID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpectDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FastTrackDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InprocessDate { get; set; }

    public int? InprocessBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? WaitDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RequestQC6Date { get; set; }

    public int? WaitBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FinishDate { get; set; }

    public int? FinishBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CloseDate { get; set; }

    public string? Remark { get; set; }

    public int? CloseBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }
}
