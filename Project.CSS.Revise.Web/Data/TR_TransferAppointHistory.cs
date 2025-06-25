using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_TransferAppointHistory")]
public partial class TR_TransferAppointHistory
{
    [Key]
    public int ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BU { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public int? TransferUnit { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TransferValue { get; set; }

    public int? TransferAppointUnit { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TransferAppointValue { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }
}
