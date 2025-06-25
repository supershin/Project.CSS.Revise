using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_DefectAreaType_Mapping")]
public partial class tm_DefectAreaType_Mapping
{
    [Key]
    public int ID { get; set; }

    public int? DefectAreaID { get; set; }

    public int? DefectTypeID { get; set; }

    [ForeignKey("DefectAreaID")]
    [InverseProperty("tm_DefectAreaType_Mappings")]
    public virtual tm_DefectArea? DefectArea { get; set; }

    [ForeignKey("DefectTypeID")]
    [InverseProperty("tm_DefectAreaType_Mappings")]
    public virtual tm_DefectType? DefectType { get; set; }
}
