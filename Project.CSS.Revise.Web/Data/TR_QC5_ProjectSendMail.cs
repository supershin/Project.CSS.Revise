using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC5_ProjectSendMail")]
public partial class TR_QC5_ProjectSendMail
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
    [InverseProperty("TR_QC5_ProjectSendMails")]
    public virtual tm_Project? Project { get; set; }
}
