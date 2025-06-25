using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC6_Unsold_SendMail")]
public partial class TR_QC6_Unsold_SendMail
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? SendToType { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Email { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_QC6_Unsold_SendMails")]
    public virtual tm_Project? Project { get; set; }
}
