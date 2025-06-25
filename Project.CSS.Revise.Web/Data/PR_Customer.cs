using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("PR_Customer")]
public partial class PR_Customer
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? IDCardNo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    public int? Age { get; set; }

    [StringLength(2000)]
    public string? AddressName { get; set; }

    [StringLength(200)]
    public string? SubDistrict { get; set; }

    [StringLength(200)]
    public string? District { get; set; }

    [StringLength(200)]
    public string? Province { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ZipCode { get; set; }

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

    [InverseProperty("Customer")]
    public virtual ICollection<PR_CustomerCareer> PR_CustomerCareers { get; set; } = new List<PR_CustomerCareer>();

    [InverseProperty("Customer")]
    public virtual ICollection<PR_CustomerDebt> PR_CustomerDebts { get; set; } = new List<PR_CustomerDebt>();

    [InverseProperty("Customer")]
    public virtual ICollection<PR_CustomerIncome> PR_CustomerIncomes { get; set; } = new List<PR_CustomerIncome>();

    [InverseProperty("Customer")]
    public virtual ICollection<PR_LoanCustomerAttach> PR_LoanCustomerAttaches { get; set; } = new List<PR_LoanCustomerAttach>();

    [InverseProperty("Customer")]
    public virtual ICollection<PR_LoanCustomer> PR_LoanCustomers { get; set; } = new List<PR_LoanCustomer>();
}
