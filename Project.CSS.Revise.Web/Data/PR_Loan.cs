using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("PR_Loan")]
[Index("ProjectID", "UnitCode", "ContractNumber", "DraftDate", "SubmitDate", Name = "NonClusteredIndex-20201109-102716")]
public partial class PR_Loan
{
    [Key]
    public Guid ID { get; set; }

    public int? UserTypeID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? UnitCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ContractNumber { get; set; }

    public string? ContractName { get; set; }

    [StringLength(200)]
    public string? ContractMobile { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ContractSellingPrice { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DraftDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? DraftBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SubmitDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? SubmitBy { get; set; }

    public bool? FlagAccept { get; set; }

    public bool? ConsentAccept { get; set; }

    public bool? SubscribeAccept { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ConsentSubscribeBy { get; set; }

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

    [InverseProperty("Loan")]
    public virtual ICollection<PR_CustomerCareer> PR_CustomerCareers { get; set; } = new List<PR_CustomerCareer>();

    [InverseProperty("Loan")]
    public virtual ICollection<PR_CustomerDebt> PR_CustomerDebts { get; set; } = new List<PR_CustomerDebt>();

    [InverseProperty("Loan")]
    public virtual ICollection<PR_CustomerIncome> PR_CustomerIncomes { get; set; } = new List<PR_CustomerIncome>();

    [InverseProperty("Loan")]
    public virtual ICollection<PR_LoanBankAttachFile> PR_LoanBankAttachFiles { get; set; } = new List<PR_LoanBankAttachFile>();

    [InverseProperty("Loan")]
    public virtual ICollection<PR_LoanCustomerAttach> PR_LoanCustomerAttaches { get; set; } = new List<PR_LoanCustomerAttach>();

    [InverseProperty("Loan")]
    public virtual ICollection<PR_LoanCustomer> PR_LoanCustomers { get; set; } = new List<PR_LoanCustomer>();

    [ForeignKey("ProjectID")]
    [InverseProperty("PR_Loans")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("UserTypeID")]
    [InverseProperty("PR_Loans")]
    public virtual tm_Ext? UserType { get; set; }
}
