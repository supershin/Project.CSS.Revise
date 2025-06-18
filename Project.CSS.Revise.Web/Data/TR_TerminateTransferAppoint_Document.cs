using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_TerminateTransferAppoint_Document")]
public partial class TR_TerminateTransferAppoint_Document
{
    [Key]
    public Guid ID { get; set; }

    public Guid? TerminateTransferAppointID { get; set; }

    public Guid? UnitDocumentID { get; set; }

    [ForeignKey("TerminateTransferAppointID")]
    [InverseProperty("TR_TerminateTransferAppoint_Documents")]
    public virtual TR_TerminateTransferAppoint? TerminateTransferAppoint { get; set; }

    [ForeignKey("UnitDocumentID")]
    [InverseProperty("TR_TerminateTransferAppoint_Documents")]
    public virtual TR_UnitDocument? UnitDocument { get; set; }
}
