using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Subject")]
public partial class tm_Subject
{
    [Key]
    public int ID { get; set; }

    public int? TopicID { get; set; }

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

    [InverseProperty("Subject")]
    public virtual ICollection<TR_QC_CheckList> TR_QC_CheckLists { get; set; } = new List<TR_QC_CheckList>();

    [ForeignKey("TopicID")]
    [InverseProperty("tm_Subjects")]
    public virtual tm_Topic? Topic { get; set; }
}
