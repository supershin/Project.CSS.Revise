using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrRegisterCallStaffCounter
{
    public int Id { get; set; }

    public int? RegisterLogId { get; set; }

    public string? CallStaffStatus { get; set; }

    public DateTime? ActionDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public virtual TrRegisterLog? RegisterLog { get; set; }
}
