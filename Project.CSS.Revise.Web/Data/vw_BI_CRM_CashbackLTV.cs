using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
public partial class vw_BI_CRM_CashbackLTV
{
    [StringLength(10)]
    public string? ProjectID { get; set; }

    [StringLength(6)]
    [Unicode(false)]
    public string? YYYYMM { get; set; }

    [Column(TypeName = "numeric(38, 2)")]
    public decimal? CashbackLTV { get; set; }
}
