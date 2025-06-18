using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_Register_CallStaffCounter")]
public partial class TR_Register_CallStaffCounter
{
    [Key]
    public int ID { get; set; }

    public int? RegisterLogID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CallStaffStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ActionDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [ForeignKey("RegisterLogID")]
    [InverseProperty("TR_Register_CallStaffCounters")]
    public virtual TR_RegisterLog? RegisterLog { get; set; }
}
