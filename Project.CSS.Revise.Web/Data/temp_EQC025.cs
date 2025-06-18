using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("temp_EQC025")]
public partial class temp_EQC025
{
    [StringLength(255)]
    public string? ProjectID { get; set; }

    [StringLength(255)]
    public string? ProjectName { get; set; }

    [StringLength(255)]
    public string? UnitCode { get; set; }

    [StringLength(255)]
    public string? ACTIVE { get; set; }

    [StringLength(255)]
    public string? UnitStatus { get; set; }

    public double? SellingPrice { get; set; }

    [StringLength(255)]
    public string? CustomerName { get; set; }

    [StringLength(255)]
    public string? BookDate { get; set; }

    [StringLength(255)]
    public string? UnitStatus_CS { get; set; }

    [StringLength(255)]
    public string? RemarkUnitStatus_CS { get; set; }

    [StringLength(255)]
    public string? LetterDueDate_CS { get; set; }

    [StringLength(255)]
    public string? ExpectTransfer { get; set; }

    [StringLength(255)]
    public string? TransferDueDate_CS { get; set; }

    [StringLength(255)]
    public string? ExpectTransferBy { get; set; }

    [StringLength(255)]
    public string? CSResponse { get; set; }

    [StringLength(255)]
    public string? Q5 { get; set; }

    [StringLength(255)]
    public string? Q1 { get; set; }

    [StringLength(255)]
    public string? Q2 { get; set; }

    [StringLength(255)]
    public string? Q3 { get; set; }

    [StringLength(255)]
    public string? Q4 { get; set; }

    [StringLength(255)]
    public string? Q6 { get; set; }

    [StringLength(255)]
    public string? Q6_Detail { get; set; }

    [StringLength(255)]
    public string? Q7 { get; set; }

    [StringLength(255)]
    public string? Q7_Detail { get; set; }

    [StringLength(255)]
    public string? Q8 { get; set; }

    [StringLength(255)]
    public string? Q8_Detail { get; set; }

    [StringLength(255)]
    public string? Q9 { get; set; }

    [StringLength(255)]
    public string? Q9_Detail { get; set; }

    [StringLength(255)]
    public string? Q10 { get; set; }
}
