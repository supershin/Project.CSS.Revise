using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_BU")]
public partial class tm_BU
{
    [Key]
    public int ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("BU")]
    public virtual ICollection<tm_BUProject_Mapping> tm_BUProject_Mappings { get; set; } = new List<tm_BUProject_Mapping>();
}
