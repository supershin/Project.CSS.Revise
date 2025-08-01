﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_BR67")]
public partial class temp_BR67
{
    [StringLength(255)]
    public string? BankCode { get; set; }

    [StringLength(255)]
    public string? UnitCode { get; set; }

    [StringLength(255)]
    public string? CustomerName { get; set; }

    [StringLength(255)]
    public string? UnitStatus_CS { get; set; }

    [StringLength(255)]
    public string? ProgressStatus { get; set; }
}
