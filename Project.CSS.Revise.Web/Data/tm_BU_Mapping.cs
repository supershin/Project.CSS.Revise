using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_BU_Mapping")]
public partial class tm_BU_Mapping
{
    [Key]
    public int ID { get; set; }

    public int BUID { get; set; }

    public int UserID { get; set; }

    public bool FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("BUID")]
    [InverseProperty("tm_BU_Mappings")]
    public virtual tm_BU BU { get; set; } = null!;

    [ForeignKey("UserID")]
    [InverseProperty("tm_BU_Mappings")]
    public virtual tm_User User { get; set; } = null!;
}
