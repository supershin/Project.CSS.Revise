using System;
using System.Collections.Generic;

namespace Project.CSS.Revise.Web.Data;

public partial class LinePostLog
{
    public Guid Id { get; set; }

    public string? ReplyToken { get; set; }

    public string? Type { get; set; }

    public string? MessageId { get; set; }

    public string? MessageText { get; set; }

    public string? MessageType { get; set; }

    public string? MessageStickerId { get; set; }

    public string? MessagePackageId { get; set; }

    public string? MessageStickerResourceType { get; set; }

    public string? SourceUserId { get; set; }

    public string? SourceRoomId { get; set; }

    public string? SourceGroupId { get; set; }

    public DateTime? CreateDate { get; set; }
}
