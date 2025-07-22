using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_ProjectEvent")]
public partial class TR_ProjectEvent
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public int? EventID { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("EventID")]
    [InverseProperty("TR_ProjectEvents")]
    public virtual tm_Event? Event { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_ProjectEvents")]
    public virtual tm_Project? Project { get; set; }
}
