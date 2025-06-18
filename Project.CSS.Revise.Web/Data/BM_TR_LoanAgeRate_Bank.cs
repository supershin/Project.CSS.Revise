using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("BM_TR_LoanAgeRate_Bank")]
public partial class BM_TR_LoanAgeRate_Bank
{
    [Key]
    public int ID { get; set; }

    public int? BankID { get; set; }

    public int? MaxLoanYear { get; set; }

    public int? MaxLoanAge { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Rate { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? AverageRate { get; set; }

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
    [InverseProperty("BM_TR_LoanAgeRate_Banks")]
    public virtual tm_Bank? Bank { get; set; }
}
