using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TmLetterDayDue
{
    public int Id { get; set; }

    public int? DayDue { get; set; }

    public int? DayFrom { get; set; }

    public int? DayTo { get; set; }

    public bool? FlagAcive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }
}
