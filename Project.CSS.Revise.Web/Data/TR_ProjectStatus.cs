using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_ProjectStatus")]
public partial class TR_ProjectStatus
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public int? StatusID { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_ProjectStatuses")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("StatusID")]
    [InverseProperty("TR_ProjectStatuses")]
    public virtual tm_Ext? Status { get; set; }
}
