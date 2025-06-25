using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_LetterSendReason")]
public partial class tm_LetterSendReason
{
    [Key]
    public int ID { get; set; }

    [StringLength(500)]
    public string? Name { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("SendReason")]
    public virtual ICollection<TR_Letter> TR_Letters { get; set; } = new List<TR_Letter>();
}
