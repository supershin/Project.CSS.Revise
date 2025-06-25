using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Answer")]
public partial class tm_Answer
{
    [Key]
    public int ID { get; set; }

    public int? QuestionID { get; set; }

    public string? AnswerName { get; set; }

    public bool? FlagOtherText { get; set; }

    [StringLength(200)]
    public string? OtherText { get; set; }

    [StringLength(200)]
    public string? OtherTextAfter { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("QuestionID")]
    [InverseProperty("tm_Answers")]
    public virtual tm_Question? Question { get; set; }

    [InverseProperty("Answer")]
    public virtual ICollection<TR_QuestionAnswer> TR_QuestionAnswers { get; set; } = new List<TR_QuestionAnswer>();
}
