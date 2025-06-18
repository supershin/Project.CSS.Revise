using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_BUProject_Mapping")]
public partial class tm_BUProject_Mapping
{
    [Key]
    public int ID { get; set; }

    public int? BUID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    [ForeignKey("BUID")]
    [InverseProperty("tm_BUProject_Mappings")]
    public virtual tm_BU? BU { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("tm_BUProject_Mappings")]
    public virtual tm_Project? Project { get; set; }
}
