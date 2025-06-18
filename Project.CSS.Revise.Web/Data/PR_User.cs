using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("PR_User")]
public partial class PR_User
{
    [Key]
    public int ID { get; set; }

    public int? UserTypeID { get; set; }

    [StringLength(200)]
    public string? FirstName { get; set; }

    [StringLength(200)]
    public string? LastName { get; set; }

    [StringLength(100)]
    public string? Mobile { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? UserName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Password { get; set; }

    public bool? ConsentAccept { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? UpdateBy { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<PR_UserBank_Mapping> PR_UserBank_Mappings { get; set; } = new List<PR_UserBank_Mapping>();

    [InverseProperty("User")]
    public virtual ICollection<PR_User_PasswordChange> PR_User_PasswordChanges { get; set; } = new List<PR_User_PasswordChange>();

    [ForeignKey("UserTypeID")]
    [InverseProperty("PR_Users")]
    public virtual tm_Ext? UserType { get; set; }
}
