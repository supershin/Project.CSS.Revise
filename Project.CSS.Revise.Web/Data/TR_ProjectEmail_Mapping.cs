using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_ProjectEmail_Mapping")]
public partial class TR_ProjectEmail_Mapping
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(10)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [StringLength(10)]
    public string? UpdateBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_ProjectEmail_Mappings")]
    public virtual tm_Project? Project { get; set; }
}
