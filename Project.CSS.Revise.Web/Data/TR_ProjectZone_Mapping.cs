using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[PrimaryKey("ProjectZoneID", "ProjectID")]
[Table("TR_ProjectZone_Mapping")]
public partial class TR_ProjectZone_Mapping
{
    [Key]
    public int ProjectZoneID { get; set; }

    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string ProjectID { get; set; } = null!;

    [ForeignKey("ProjectZoneID")]
    [InverseProperty("TR_ProjectZone_Mappings")]
    public virtual tm_Ext ProjectZone { get; set; } = null!;
}
