using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_LetterDayDue")]
public partial class tm_LetterDayDue
{
    [Key]
    public int ID { get; set; }

    public int? DayDue { get; set; }

    public int? DayFrom { get; set; }

    public int? DayTo { get; set; }

    public bool? FlagAcive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }
}
