using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_ProjectUser_Mapping")]
public partial class tm_ProjectUser_Mapping
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public int? UserID { get; set; }

    public int? GroupUserID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("GroupUserID")]
    [InverseProperty("tm_ProjectUser_Mappings")]
    public virtual tm_Ext? GroupUser { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("tm_ProjectUser_Mappings")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("UserID")]
    [InverseProperty("tm_ProjectUser_Mappings")]
    public virtual tm_User? User { get; set; }
}
