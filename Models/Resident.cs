using System;
using System.Collections.Generic;

namespace MgeniTrack.Models;

public partial class Resident
{
    public int ResidentId { get; set; }

    public int UserId { get; set; }

    public string HouseNumber { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<VisitorInvitation> VisitorInvitations { get; set; } = new List<VisitorInvitation>();
}
