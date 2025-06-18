using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("tm_DataSource")]
public partial class tm_DataSource
{
    [Key]
    public Guid Datasource_ID { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? DatasourceName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? DatasourceDesc { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }
}
