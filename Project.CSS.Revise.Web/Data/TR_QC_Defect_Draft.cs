using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC_Defect_Draft")]
public partial class TR_QC_Defect_Draft
{
    [Key]
    public Guid DefectID { get; set; }

    public Guid? UnitID { get; set; }

    public int? DefectStatusID { get; set; }

    public string? Remark { get; set; }
}
