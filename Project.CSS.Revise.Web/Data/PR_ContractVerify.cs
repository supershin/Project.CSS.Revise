using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("PR_ContractVerify")]
[Index("ProjectID", "UnitCode", "ContractNumber", "CodeVerify", Name = "NonClusteredIndex-20201109-102759")]
[Index("ProjectID", "UnitCode", "ContractNumber", "CodeVerify", Name = "NonClusteredIndex-20220825-134044")]
public partial class PR_ContractVerify
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UnitCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ContractNumber { get; set; }

    public string? ContractName { get; set; }

    [Unicode(false)]
    public string? ContractMobile { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ContractSellingPrice { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? CodeVerify { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }
}
