using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Keyless]
[Table("TR_DefectHistory_20240518")]
public partial class TR_DefectHistory_20240518
{
    public int ID { get; set; }

    public Guid? DefectID { get; set; }

    public int? DefectStatusID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }
}
