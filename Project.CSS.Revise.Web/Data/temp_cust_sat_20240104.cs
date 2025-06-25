using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_cust_sat_20240104")]
public partial class temp_cust_sat_20240104
{
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public int? UserID { get; set; }

    public int? QuestionTypeID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QuestionDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UnitCode { get; set; }

    [Column(TypeName = "text")]
    public string? Remark { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ClientIP { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }
}
