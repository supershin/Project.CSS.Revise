using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("BM_Master_Set_QuestionAnswer")]
public partial class BM_Master_Set_QuestionAnswer
{
    [Key]
    public int ID { get; set; }

    public int? SetID { get; set; }

    public int? QuestionID { get; set; }

    public int? AnswerID { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? UpdateBy { get; set; }

    [ForeignKey("AnswerID")]
    [InverseProperty("BM_Master_Set_QuestionAnswers")]
    public virtual BM_Master_QuestionAnswer? Answer { get; set; }

    [ForeignKey("QuestionID")]
    [InverseProperty("BM_Master_Set_QuestionAnswers")]
    public virtual BM_Master_Question? Question { get; set; }

    [ForeignKey("SetID")]
    [InverseProperty("BM_Master_Set_QuestionAnswers")]
    public virtual BM_Master_Set? Set { get; set; }
}
