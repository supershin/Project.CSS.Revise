using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Topic")]
public partial class tm_Topic
{
    [Key]
    public int ID { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? ProjectType { get; set; }

    public int? QCTypeID { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("Topic")]
    public virtual ICollection<TR_QC_CheckList> TR_QC_CheckLists { get; set; } = new List<TR_QC_CheckList>();

    [InverseProperty("Topic")]
    public virtual ICollection<tm_Subject> tm_Subjects { get; set; } = new List<tm_Subject>();
}
