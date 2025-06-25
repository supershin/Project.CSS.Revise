using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_ProjectCounter_Mapping")]
public partial class TR_ProjectCounter_Mapping
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public int? QueueTypeID { get; set; }

    public int? StartCounter { get; set; }

    public int? EndCounter { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_ProjectCounter_Mappings")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("QueueTypeID")]
    [InverseProperty("TR_ProjectCounter_Mappings")]
    public virtual tm_Ext? QueueType { get; set; }
}
