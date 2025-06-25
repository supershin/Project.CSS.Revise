﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_ProjectUnitFloorPlan")]
public partial class TR_ProjectUnitFloorPlan
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? ProjectFloorPlanID { get; set; }

    public Guid? UnitID { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_ProjectUnitFloorPlans")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("ProjectFloorPlanID")]
    [InverseProperty("TR_ProjectUnitFloorPlans")]
    public virtual TR_ProjectFloorPlan? ProjectFloorPlan { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("TR_ProjectUnitFloorPlans")]
    public virtual tm_Unit? Unit { get; set; }
}
