using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
public partial class vw_Contract_BankAccount
{
    [StringLength(50)]
    [Unicode(false)]
    public string? ContractNumber { get; set; }

    [StringLength(20)]
    public string? ContractID { get; set; }

    [StringLength(50)]
    public string? CustomerID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BankCode { get; set; }

    [StringLength(50)]
    public string? BankID { get; set; }

    [StringLength(8000)]
    [Unicode(false)]
    public string? CustomerBookBank { get; set; }
}
