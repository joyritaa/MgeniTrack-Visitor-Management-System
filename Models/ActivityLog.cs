using System;
using System.Collections.Generic;

namespace MgeniTrack.Models;

public partial class ActivityLog
{
    public int LogId { get; set; }

    public int? UserId { get; set; }

    public string? ActionType { get; set; }

    public string? ActionDetails { get; set; }

    public string? RelatedEntityType { get; set; }

    public int? RelatedEntityId { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime? TimeStamp { get; set; }

    public virtual User? User { get; set; }
}
