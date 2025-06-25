using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_ProjectLandOffice")]
public partial class TR_ProjectLandOffice
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public int? LandOfficeID { get; set; }

    [ForeignKey("LandOfficeID")]
    [InverseProperty("TR_ProjectLandOffices")]
    public virtual tm_LandOffice? LandOffice { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_ProjectLandOffices")]
    public virtual tm_Project? Project { get; set; }
}
