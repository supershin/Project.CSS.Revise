using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_QC5_CheckList")]
public partial class tm_QC5_CheckList
{
    [Key]
    public int ID { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? ProjectType { get; set; }

    public int? ParentID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Code { get; set; }

    public string? Name { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("Question")]
    public virtual ICollection<TR_QC5_CheckList_Detail> TR_QC5_CheckList_Details { get; set; } = new List<TR_QC5_CheckList_Detail>();
}
