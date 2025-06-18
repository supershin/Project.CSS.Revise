using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QuestionAnswer")]
public partial class TR_QuestionAnswer
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public int? QuestionID { get; set; }

    public int? AnswerID { get; set; }

    public string? OtherText { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QuestionDate { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ClearDate { get; set; }

    public int? ClearBy { get; set; }

    [ForeignKey("AnswerID")]
    [InverseProperty("TR_QuestionAnswers")]
    public virtual tm_Answer? Answer { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_QuestionAnswers")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("QuestionID")]
    [InverseProperty("TR_QuestionAnswers")]
    public virtual tm_Question? Question { get; set; }
}
