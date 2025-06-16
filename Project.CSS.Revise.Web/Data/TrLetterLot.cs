using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrLetterLot
{
    public Guid Id { get; set; }

    public string? LotNo { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual ICollection<TrLetterLotDetail> TrLetterLotDetails { get; set; } = new List<TrLetterLotDetail>();

    public virtual ICollection<TrLetterLotResource> TrLetterLotResources { get; set; } = new List<TrLetterLotResource>();
}
