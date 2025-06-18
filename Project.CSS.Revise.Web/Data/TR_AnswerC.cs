using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_AnswerCS")]
public partial class TR_AnswerC
{
    [Key]
    public int ID { get; set; }

    public int? QuestionID { get; set; }

    public string? AnswerName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? ImageUrl { get; set; }

    public bool? FlagOtherText { get; set; }

    public int? Revision { get; set; }

    public int? Score { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("QuestionID")]
    [InverseProperty("TR_AnswerCs")]
    public virtual TR_QuestionC? Question { get; set; }

    [InverseProperty("Answer")]
    public virtual ICollection<TR_CustomerSatisfaction_Detail> TR_CustomerSatisfaction_Details { get; set; } = new List<TR_CustomerSatisfaction_Detail>();
}
