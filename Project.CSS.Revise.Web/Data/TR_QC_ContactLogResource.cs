using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC_ContactLogResource")]
public partial class TR_QC_ContactLogResource
{
    [Key]
    public int ID { get; set; }

    public Guid? QCContactLogID { get; set; }

    public Guid? ResourceID { get; set; }

    [ForeignKey("QCContactLogID")]
    [InverseProperty("TR_QC_ContactLogResources")]
    public virtual TR_ContactLog? QCContactLog { get; set; }

    [ForeignKey("ResourceID")]
    [InverseProperty("TR_QC_ContactLogResources")]
    public virtual TR_Resource? Resource { get; set; }
}
