using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_UnitPromotionSign_Detail")]
public partial class TR_UnitPromotionSign_Detail
{
    [Key]
    public int ID { get; set; }

    public Guid? UnitPromotionSignID { get; set; }

    public int? PromotionID { get; set; }
}
