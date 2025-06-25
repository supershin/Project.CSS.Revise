using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_UserSignResource")]
public partial class TR_UserSignResource
{
    [Key]
    public int UserID { get; set; }

    public Guid? SignResourceID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("SignResourceID")]
    [InverseProperty("TR_UserSignResources")]
    public virtual TR_SignResource? SignResource { get; set; }

    [ForeignKey("UserID")]
    [InverseProperty("TR_UserSignResource")]
    public virtual tm_User User { get; set; } = null!;
}
