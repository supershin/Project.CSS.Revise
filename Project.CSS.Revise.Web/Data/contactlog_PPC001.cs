using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("contactlog_PPC001")]
public partial class contactlog_PPC001
{
    [StringLength(255)]
    public string? ProjectID { get; set; }

    [StringLength(255)]
    public string? Unit { get; set; }

    [StringLength(255)]
    public string? BankCode { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ContactDate { get; set; }

    [StringLength(255)]
    public string? ContactName { get; set; }

    public string? Detail { get; set; }

    [StringLength(255)]
    public string? CSRespoonse { get; set; }
}
