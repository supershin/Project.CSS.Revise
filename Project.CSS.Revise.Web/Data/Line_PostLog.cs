using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("Line_PostLog")]
public partial class Line_PostLog
{
    [Key]
    public Guid id { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? replyToken { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? type { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? message_id { get; set; }

    public string? message_text { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? message_type { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? message_stickerId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? message_packageId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? message_stickerResourceType { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? source_userId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? source_roomId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? source_groupId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createDate { get; set; }
}
