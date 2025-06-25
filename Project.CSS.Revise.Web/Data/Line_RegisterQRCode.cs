using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("Line_RegisterQRCode")]
public partial class Line_RegisterQRCode
{
    [Key]
    public long ID { get; set; }

    public Guid? RegisterID { get; set; }

    public int? QRCodeID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpireDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public Guid? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public Guid? UpdateBy { get; set; }

    [ForeignKey("QRCodeID")]
    [InverseProperty("Line_RegisterQRCodes")]
    public virtual Line_QRCode? QRCode { get; set; }

    [ForeignKey("RegisterID")]
    [InverseProperty("Line_RegisterQRCodes")]
    public virtual Line_Register? Register { get; set; }
}
