using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_ProjectAppointLimit_Mapping")]
public partial class TR_ProjectAppointLimit_Mapping
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public int? DayID { get; set; }

    public int? TimeID { get; set; }

    public int? UnitLimitValue { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("DayID")]
    [InverseProperty("TR_ProjectAppointLimit_MappingDays")]
    public virtual tm_Ext? Day { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_ProjectAppointLimit_Mappings")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("TimeID")]
    [InverseProperty("TR_ProjectAppointLimit_MappingTimes")]
    public virtual tm_Ext? Time { get; set; }
}
