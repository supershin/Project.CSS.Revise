using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_MenuAction")]
public partial class tm_MenuAction
{
    [Key]
    public int ID { get; set; }

    public int MenuID { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    public bool View { get; set; }

    public bool Add { get; set; }

    public bool Update { get; set; }

    public bool Delete { get; set; }

    public bool Download { get; set; }

    public bool FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("MenuID")]
    [InverseProperty("tm_MenuActions")]
    public virtual tm_Menu Menu { get; set; } = null!;
}
