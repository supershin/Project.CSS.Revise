using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_TagEvent")]
public partial class TR_TagEvent
{
    [Key]
    public int ID { get; set; }

    public int? EventID { get; set; }

    public int? TagID { get; set; }

    [ForeignKey("EventID")]
    [InverseProperty("TR_TagEvents")]
    public virtual tm_Event? Event { get; set; }

    [ForeignKey("TagID")]
    [InverseProperty("TR_TagEvents")]
    public virtual tm_Tag? Tag { get; set; }
}
