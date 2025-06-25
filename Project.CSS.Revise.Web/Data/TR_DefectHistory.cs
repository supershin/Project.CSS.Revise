using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_DefectHistory")]
public partial class TR_DefectHistory
{
    [Key]
    public int ID { get; set; }

    public Guid? DefectID { get; set; }

    public int? DefectStatusID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }
}
