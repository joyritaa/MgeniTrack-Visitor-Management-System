using System;
using System.Collections.Generic;

namespace MgeniTrack.Models;

public partial class VisitorInvitation
{
    public required int InvitationId { get; set; }

    public int ResidentId { get; set; }

    public string VisitorName { get; set; } = null!;

    public string? VisitorPhone { get; set; }

    public string? VisitorEmail { get; set; }

    public string? PurposeOfVisit { get; set; }

    public DateOnly? ExpectedDate { get; set; }

    public string? InvitationToken { get; set; }

    public string? QrCodePath { get; set; }

    public string? VisitStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Resident Resident { get; set; } = null!;

    public virtual ICollection<Visitor> Visitors { get; set; } = new List<Visitor>();

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
