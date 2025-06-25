using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_PowerOfAttorney")]
public partial class TR_PowerOfAttorney
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SignDate { get; set; }

    public Guid? SignResourceID { get; set; }

    public Guid? SignResourceID_2 { get; set; }

    public Guid? IDCardResourceID { get; set; }

    public Guid? IDCardResourceID_2 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }
}
