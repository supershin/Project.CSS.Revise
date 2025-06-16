using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrReviseUnitPromotionDetail
{
    public long Id { get; set; }

    public Guid? ReviseUnitPromotionId { get; set; }

    public string? PromotionId { get; set; }

    public string? MpromotionId { get; set; }

    public int? PdetailId { get; set; }

    public string? PromotionDescription { get; set; }

    public decimal? PromotionAmount { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual TrReviseUnitPromotion? ReviseUnitPromotion { get; set; }
}
