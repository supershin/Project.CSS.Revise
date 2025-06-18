using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Question")]
public partial class tm_Question
{
    [Key]
    public int ID { get; set; }

    public string? QuestionName { get; set; }

    public bool? FlagMulti { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("Question")]
    public virtual ICollection<TR_QuestionAnswer> TR_QuestionAnswers { get; set; } = new List<TR_QuestionAnswer>();

    [InverseProperty("Question")]
    public virtual ICollection<tm_Answer> tm_Answers { get; set; } = new List<tm_Answer>();
}
