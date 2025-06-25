using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_UnitUser_Mapping")]
[Index("ProjectID", "UnitCode", "UserID", Name = "NonClusteredIndex-20230421-095349")]
public partial class TR_UnitUser_Mapping
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? UnitCode { get; set; }

    public int? UserID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }
}
