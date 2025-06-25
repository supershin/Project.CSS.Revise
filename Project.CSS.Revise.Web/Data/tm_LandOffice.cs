using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_LandOffice")]
public partial class tm_LandOffice
{
    [Key]
    public int ID { get; set; }

    [StringLength(2000)]
    public string? Name { get; set; }

    [StringLength(2000)]
    [Unicode(false)]
    public string? Name_Eng { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("LandOffice")]
    public virtual ICollection<TR_ProjectLandOffice> TR_ProjectLandOffices { get; set; } = new List<TR_ProjectLandOffice>();
}
