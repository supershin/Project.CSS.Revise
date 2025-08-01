﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("BM_TR_Set_Score")]
public partial class BM_TR_Set_Score
{
    [Key]
    public int ID { get; set; }

    public int? SetID { get; set; }

    public int? BankID { get; set; }

    public int? ScoreTypeID { get; set; }

    public int? Score { get; set; }

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

    [ForeignKey("BankID")]
    [InverseProperty("BM_TR_Set_Scores")]
    public virtual tm_Bank? Bank { get; set; }

    [ForeignKey("ScoreTypeID")]
    [InverseProperty("BM_TR_Set_Scores")]
    public virtual BM_Master_ScoreType? ScoreType { get; set; }

    [ForeignKey("SetID")]
    [InverseProperty("BM_TR_Set_Scores")]
    public virtual BM_Master_Set? Set { get; set; }
}
