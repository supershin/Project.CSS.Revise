using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
public partial class vw_Defect
{
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public int? QCTypeID { get; set; }

    public int? DefectAreaID { get; set; }

    [StringLength(1000)]
    public string? AreaName { get; set; }

    public int? DefectTypeID { get; set; }

    [StringLength(1000)]
    public string? TypeName { get; set; }

    public int? DefectDescriptionID { get; set; }

    [StringLength(1000)]
    public string? DescName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }
}
