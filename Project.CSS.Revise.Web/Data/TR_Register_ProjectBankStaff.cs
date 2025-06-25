using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Register_ProjectBankStaff")]
public partial class TR_Register_ProjectBankStaff
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public int? BankID { get; set; }

    public int? Staff { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? updateBy { get; set; }

    [ForeignKey("BankID")]
    [InverseProperty("TR_Register_ProjectBankStaffs")]
    public virtual tm_Bank? Bank { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_Register_ProjectBankStaffs")]
    public virtual tm_Project? Project { get; set; }
}
