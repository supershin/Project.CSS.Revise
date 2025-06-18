using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_RegisterBank")]
public partial class TR_RegisterBank
{
    [Key]
    public int ID { get; set; }

    public int? RegisterLogID { get; set; }

    public int? BankID { get; set; }

    [ForeignKey("RegisterLogID")]
    [InverseProperty("TR_RegisterBanks")]
    public virtual TR_RegisterLog? RegisterLog { get; set; }
}
