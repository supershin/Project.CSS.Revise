using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_CompanyProject")]
public partial class TR_CompanyProject
{
    [Key]
    public int ID { get; set; }

    public int? CompanyID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    [ForeignKey("CompanyID")]
    [InverseProperty("TR_CompanyProjects")]
    public virtual tm_Company? Company { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_CompanyProjects")]
    public virtual tm_Project? Project { get; set; }
}
