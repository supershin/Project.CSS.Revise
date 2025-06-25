using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QuestionCS")]
public partial class TR_QuestionC
{
    [Key]
    public int ID { get; set; }

    public int? QuestionTypeID { get; set; }

    public string? QuestionName { get; set; }

    public bool? FlagMultiChoice { get; set; }

    public bool? FlagCorporate { get; set; }

    public bool? FlagRating { get; set; }

    public int? Revision { get; set; }

    public int? Col { get; set; }

    public int? LineOrder { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("QuestionTypeID")]
    [InverseProperty("TR_QuestionCs")]
    public virtual tm_Ext? QuestionType { get; set; }

    [InverseProperty("Question")]
    public virtual ICollection<TR_AnswerC> TR_AnswerCs { get; set; } = new List<TR_AnswerC>();

    [InverseProperty("Question")]
    public virtual ICollection<TR_CustomerSatisfaction_Detail> TR_CustomerSatisfaction_Details { get; set; } = new List<TR_CustomerSatisfaction_Detail>();
}
