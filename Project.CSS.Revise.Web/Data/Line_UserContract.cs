using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

public partial class Line_UserContract
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? LineUserID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ContractNumber { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreatBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("LineUserID")]
    [InverseProperty("Line_UserContracts")]
    public virtual Line_User? LineUser { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("Line_UserContracts")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("UnitID")]
    [InverseProperty("Line_UserContracts")]
    public virtual tm_Unit? Unit { get; set; }
}
