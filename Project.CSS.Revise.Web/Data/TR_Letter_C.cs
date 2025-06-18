using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Letter_CS")]
public partial class TR_Letter_C
{
    [Key]
    public Guid ID { get; set; }

    public int? EventID { get; set; }

    [StringLength(500)]
    public string? EventLocation { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EventStartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EventEndDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? CodeVerify { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? PercentProgress { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ExpectTransfer { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FINPlusStartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FINPlusEndDate { get; set; }

    [StringLength(3000)]
    public string? Promotion { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ElectricMeter { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? WaterMaintainCost { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CentralValue { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InspectLastDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpectLastDate { get; set; }

    public int? SignUserID { get; set; }

    [StringLength(200)]
    public string? SignUserPosition { get; set; }

    [ForeignKey("EventID")]
    [InverseProperty("TR_Letter_Cs")]
    public virtual tm_Event? Event { get; set; }

    [ForeignKey("SignUserID")]
    [InverseProperty("TR_Letter_Cs")]
    public virtual tm_User? SignUser { get; set; }
}
