using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_RemarkUnitStatus_Mapping")]
public partial class TR_RemarkUnitStatus_Mapping
{
    [Key]
    public int ID { get; set; }

    public int UnitStatusCS_ID { get; set; }

    public int RemarkUnitStatusCS_ID { get; set; }

    [ForeignKey("RemarkUnitStatusCS_ID")]
    [InverseProperty("TR_RemarkUnitStatus_MappingRemarkUnitStatusCs")]
    public virtual tm_Ext RemarkUnitStatusCS { get; set; } = null!;

    [ForeignKey("UnitStatusCS_ID")]
    [InverseProperty("TR_RemarkUnitStatus_MappingUnitStatusCs")]
    public virtual tm_Ext UnitStatusCS { get; set; } = null!;
}
