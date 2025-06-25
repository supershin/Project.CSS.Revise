using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_LineToken")]
public partial class tm_LineToken
{
    [Key]
    public int ID { get; set; }

    public int? ProjectZoneID { get; set; }

    public int? BankID { get; set; }

    [Unicode(false)]
    public string? Token { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("BankID")]
    [InverseProperty("tm_LineTokens")]
    public virtual tm_Bank? Bank { get; set; }

    [ForeignKey("ProjectZoneID")]
    [InverseProperty("tm_LineTokens")]
    public virtual tm_Ext? ProjectZone { get; set; }
}
