using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrLetterLotDetail
{
    public Guid Id { get; set; }

    public Guid? LotId { get; set; }

    public Guid? LetterId { get; set; }

    public string? LetterNo { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TrLetter? Letter { get; set; }

    public virtual TrLetterLot? Lot { get; set; }
}
