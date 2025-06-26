using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("TR_QC6")]
public partial class TR_QC6
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProjectID { get; set; }

    public Guid? UnitID { get; set; }

    public int? ResponsiblePersonID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? QC_Date { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? QC_Time { get; set; }

    [StringLength(500)]
    public string? CustomerName { get; set; }

    public int? QC_StatusID { get; set; }

    public int? CustRelationID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Email { get; set; }

    public Guid? SignResourceID { get; set; }

    public bool? FlagActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ClearDate { get; set; }

    public int? ClearBy { get; set; }

    [ForeignKey("CustRelationID")]
    [InverseProperty("TR_QC6CustRelations")]
    public virtual tm_Ext? CustRelation { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TR_QC6s")]
    public virtual tm_Project? Project { get; set; }

    [ForeignKey("QC_StatusID")]
    [InverseProperty("TR_QC6QC_Statuses")]
    public virtual tm_Ext? QC_Status { get; set; }

    [ForeignKey("ResponsiblePersonID")]
    [InverseProperty("TR_QC6s")]
    public virtual tm_User? ResponsiblePerson { get; set; }

    [ForeignKey("SignResourceID")]
    [InverseProperty("TR_QC6s")]
    public virtual TR_SignResource? SignResource { get; set; }

    [InverseProperty("QC")]
    public virtual ICollection<TR_ContactLog> TR_ContactLogs { get; set; } = new List<TR_ContactLog>();

    [ForeignKey("UnitID")]
    [InverseProperty("TR_QC6s")]
    public virtual tm_Unit? Unit { get; set; }
}
