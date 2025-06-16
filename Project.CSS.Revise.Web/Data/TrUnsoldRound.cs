using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrUnsoldRound
{
    public int Id { get; set; }

    public string? ProjectId { get; set; }

    public DateTime? RoundDate { get; set; }

    public bool? FlagSendMail { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual ICollection<TrUnsoldRoundUnit> TrUnsoldRoundUnits { get; set; } = new List<TrUnsoldRoundUnit>();
}
