using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC_Defect_OverDueExpect_UserPermission")]
public partial class TR_QC_Defect_OverDueExpect_UserPermission
{
    [Key]
    public int ID { get; set; }

    public int? UserID { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("UserID")]
    [InverseProperty("TR_QC_Defect_OverDueExpect_UserPermissions")]
    public virtual tm_User? User { get; set; }
}
