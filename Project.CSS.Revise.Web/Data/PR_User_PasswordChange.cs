using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("PR_User_PasswordChange")]
public partial class PR_User_PasswordChange
{
    [Key]
    public int ID { get; set; }

    public int? UserID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Password { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ChangeDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [ForeignKey("UserID")]
    [InverseProperty("PR_User_PasswordChanges")]
    public virtual PR_User? User { get; set; }
}
