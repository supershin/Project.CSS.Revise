using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_DeviceSignIn")]
public partial class TR_DeviceSignIn
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? DeviceID { get; set; }

    public int? UserID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SignInDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SignOutDate { get; set; }

    public bool? IsSignIn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("UserID")]
    [InverseProperty("TR_DeviceSignIns")]
    public virtual tm_User? User { get; set; }
}
