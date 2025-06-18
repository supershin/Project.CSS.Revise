using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_edit_letter")]
public partial class temp_edit_letter
{
    [Column("No#")]
    public double? No_ { get; set; }

    [StringLength(255)]
    public string? UnitCode { get; set; }

    [StringLength(255)]
    public string? ContractNumber { get; set; }

    [StringLength(255)]
    public string? CustomerName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LetterDueDate_CS { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? จดหมายลงวันที่ { get; set; }

    [StringLength(255)]
    public string? ประเภทจดหมาย { get; set; }
}
