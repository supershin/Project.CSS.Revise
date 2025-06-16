using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class TrDefectHistory
{
    public int Id { get; set; }

    public Guid? DefectId { get; set; }

    public int? DefectStatusId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }
}
