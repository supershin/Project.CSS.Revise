using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Register_BankCounter")]
public partial class TR_Register_BankCounter
{
    [Key]
    public int ID { get; set; }

    public int? RegisterLogID { get; set; }

    public int? BankID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BankCounterStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CheckInDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InProcessDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CheckOutDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [ForeignKey("BankID")]
    [InverseProperty("TR_Register_BankCounters")]
    public virtual tm_Bank? Bank { get; set; }

    [ForeignKey("RegisterLogID")]
    [InverseProperty("TR_Register_BankCounters")]
    public virtual TR_RegisterLog? RegisterLog { get; set; }
}
