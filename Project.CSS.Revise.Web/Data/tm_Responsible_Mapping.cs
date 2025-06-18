using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_Responsible_Mapping")]
public partial class tm_Responsible_Mapping
{
    [Key]
    public int ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public int? UserID_Mapping { get; set; }

    public int? QCTypeID { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("tm_Responsible_Mappings")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("QCTypeID")]
    [InverseProperty("tm_Responsible_Mappings")]
    public virtual tm_Ext? QCType { get; set; }

    [ForeignKey("UserID_Mapping")]
    [InverseProperty("tm_Responsible_Mappings")]
    public virtual tm_User? UserID_MappingNavigation { get; set; }
}
