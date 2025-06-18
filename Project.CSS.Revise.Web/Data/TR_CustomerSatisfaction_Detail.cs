using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_CustomerSatisfaction_Detail")]
public partial class TR_CustomerSatisfaction_Detail
{
    [Key]
    public int ID { get; set; }

    public int? CustomerSatisfactionID { get; set; }

    public int? QuestionID { get; set; }

    public int? AnswerID { get; set; }

    public string? OtherText { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("AnswerID")]
    [InverseProperty("TR_CustomerSatisfaction_Details")]
    public virtual TR_AnswerC? Answer { get; set; }

    [ForeignKey("CustomerSatisfactionID")]
    [InverseProperty("TR_CustomerSatisfaction_Details")]
    public virtual TR_CustomerSatisfaction? CustomerSatisfaction { get; set; }

    [ForeignKey("QuestionID")]
    [InverseProperty("TR_CustomerSatisfaction_Details")]
    public virtual TR_QuestionC? Question { get; set; }
}
