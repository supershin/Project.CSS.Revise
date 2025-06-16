using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrTerminateTransferAppointDocument
{
    public Guid Id { get; set; }

    public Guid? TerminateTransferAppointId { get; set; }

    public Guid? UnitDocumentId { get; set; }

    public virtual TrTerminateTransferAppoint? TerminateTransferAppoint { get; set; }

    public virtual TrUnitDocument? UnitDocument { get; set; }
}
