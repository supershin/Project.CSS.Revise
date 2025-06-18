using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
public partial class vw_UnitMatrix_QCProgress
{
    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? UnitCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Build { get; set; }

    public int? Floor { get; set; }

    public int? Room { get; set; }

    [StringLength(500)]
    public string? CustomerName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QC5_FinishDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastQC5Date { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastQC6Date { get; set; }

    public int? UnitStatus_CS { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReceiveRoomDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransferDate { get; set; }

    public int Verify_QC5_CheckLis { get; set; }

    public int MatrixTypeID { get; set; }

    [Unicode(false)]
    public string? BGColor { get; set; }

    [StringLength(200)]
    public string? MatrixTypeName { get; set; }

    public int? MatrixTypeLineOrder { get; set; }
}
