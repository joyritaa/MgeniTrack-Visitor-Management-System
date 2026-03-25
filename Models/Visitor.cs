using System;
using System.Collections.Generic;

namespace MgeniTrack.Models;

public partial class Visitor
{
    public int VisitorId { get; set; }

    public string FullName { get; set; } = null!;

    public string? IdNumber { get; set; }

    public string? ContactNumber { get; set; }

    public string? PhotoUrl { get; set; }

    public DateTime? FirstVisitDate { get; set; }

    public int? TotalVisits { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? InvitedViaInvitationId { get; set; }

    public virtual VisitorInvitation? InvitedViaInvitation { get; set; }

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
