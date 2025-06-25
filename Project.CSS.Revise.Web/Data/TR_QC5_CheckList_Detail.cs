using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC5_CheckList_Detail")]
public partial class TR_QC5_CheckList_Detail
{
    [Key]
    public int ID { get; set; }

    public Guid? QC5CheckListID { get; set; }

    public int? QuestionID { get; set; }

    public int? AnswerID { get; set; }

    public string? Remark { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Score { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDtae { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("AnswerID")]
    [InverseProperty("TR_QC5_CheckList_Details")]
    public virtual tm_Ext? Answer { get; set; }

    [ForeignKey("QC5CheckListID")]
    [InverseProperty("TR_QC5_CheckList_Details")]
    public virtual TR_QC5_CheckList? QC5CheckList { get; set; }

    [ForeignKey("QuestionID")]
    [InverseProperty("TR_QC5_CheckList_Details")]
    public virtual tm_QC5_CheckList? Question { get; set; }
}
