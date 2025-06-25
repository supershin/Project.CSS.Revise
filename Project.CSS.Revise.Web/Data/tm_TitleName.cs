using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_TitleName")]
public partial class tm_TitleName
{
    [Key]
    public int ID { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Lang { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [InverseProperty("TitleID_EngNavigation")]
    public virtual ICollection<tm_User> tm_UserTitleID_EngNavigations { get; set; } = new List<tm_User>();

    [InverseProperty("Title")]
    public virtual ICollection<tm_User> tm_UserTitles { get; set; } = new List<tm_User>();
}
