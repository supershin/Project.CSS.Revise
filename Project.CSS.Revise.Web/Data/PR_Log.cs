using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

public partial class PR_Log
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UserName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Controller { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Action { get; set; }

    public string? Url { get; set; }

    public string? Form { get; set; }

    public string? QueryString { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }
}
