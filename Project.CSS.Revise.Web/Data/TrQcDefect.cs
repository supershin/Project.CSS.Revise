using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrQcDefect
{
    public Guid Id { get; set; }

    public string? ProjectId { get; set; }

    public Guid? UnitId { get; set; }

    public Guid? QcId { get; set; }

    public int? QctypeId { get; set; }

    public int? DefectStatusId { get; set; }

    public int? DefectAreaId { get; set; }

    public int? DefectTypeId { get; set; }

    public int? DefectDescriptionId { get; set; }

    public string? DefectDetail { get; set; }

    public string? Note { get; set; }

    public Guid? FloorPlanId { get; set; }

    public decimal? PointX { get; set; }

    public decimal? PointY { get; set; }

    public bool? FlagActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? ClearDate { get; set; }

    public int? ClearBy { get; set; }

    public bool? IsMajor { get; set; }

    public virtual TmDefectArea? DefectArea { get; set; }

    public virtual TmDefectDescription? DefectDescription { get; set; }

    public virtual TmExt? DefectStatus { get; set; }

    public virtual TmDefectType? DefectType { get; set; }

    public virtual TmProject? Project { get; set; }

    public virtual ICollection<TrQcDefectOverDueExpect> TrQcDefectOverDueExpects { get; set; } = new List<TrQcDefectOverDueExpect>();

    public virtual ICollection<TrQcDefectResource> TrQcDefectResources { get; set; } = new List<TrQcDefectResource>();

    public virtual TmUnit? Unit { get; set; }
}
