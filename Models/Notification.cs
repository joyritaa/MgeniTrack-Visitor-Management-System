using System;
using System.Collections.Generic;

namespace MgeniTrack.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? ResidentId { get; set; }

    public int? VisitId { get; set; }

    public string? Message { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Resident? Resident { get; set; }

    public virtual Visit? Visit { get; set; }
}
