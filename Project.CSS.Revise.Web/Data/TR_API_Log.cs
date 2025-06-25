using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_API_Log")]
public partial class TR_API_Log
{
    [Key]
    public Guid ID { get; set; }

    [Column(TypeName = "text")]
    public string? Module { get; set; }

    [Column(TypeName = "text")]
    public string? Inbound { get; set; }

    [Column(TypeName = "text")]
    public string? OutBound { get; set; }

    [Column(TypeName = "text")]
    public string? ErrorMessage { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? CreateBy { get; set; }
}
