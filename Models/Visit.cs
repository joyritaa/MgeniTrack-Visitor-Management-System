using System;
using System.Collections.Generic;

namespace MgeniTrack.Models;

public partial class Visit
{
    public int VisitId { get; set; }

    public int VisitorId { get; set; }

    public int? CheckedInBy { get; set; }

    public int? CheckedOutBy { get; set; }

    public string? HouseNumber { get; set; }

    public string? PurposeOfVisit { get; set; }

    public string? CarRegistration { get; set; }

    public int? NumberOfOccupants { get; set; }

    public DateTime? TimeIn { get; set; }

    public DateTime? TimeOut { get; set; }

    public string? VisitStatus { get; set; }

    public string? QrCode { get; set; }

    public int? VisitDuration { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? InvitationId { get; set; }

    public string? CheckInMethod { get; set; }

    public virtual User CheckedInByNavigation { get; set; } = null!;

    public virtual User CheckedOutByNavigation { get; set; } = null!;

    public virtual VisitorInvitation Invitation { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Visitor Visitor { get; set; } = null!;
}
