using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("PR_UserBank_Mapping")]
public partial class PR_UserBank_Mapping
{
    [Key]
    public int ID { get; set; }

    public int? UserID { get; set; }

    public int? BankID { get; set; }

    [ForeignKey("BankID")]
    [InverseProperty("PR_UserBank_Mappings")]
    public virtual tm_Bank? Bank { get; set; }

    [ForeignKey("UserID")]
    [InverseProperty("PR_UserBank_Mappings")]
    public virtual PR_User? User { get; set; }
}
