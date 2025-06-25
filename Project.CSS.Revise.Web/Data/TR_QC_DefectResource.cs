using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC_DefectResource")]
public partial class TR_QC_DefectResource
{
    [Key]
    public Guid ID { get; set; }

    public Guid? DefectID { get; set; }

    public Guid? ResourceID { get; set; }

    [ForeignKey("DefectID")]
    [InverseProperty("TR_QC_DefectResources")]
    public virtual TR_QC_Defect? Defect { get; set; }

    [ForeignKey("ResourceID")]
    [InverseProperty("TR_QC_DefectResources")]
    public virtual TR_Resource? Resource { get; set; }
}
