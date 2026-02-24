using System;
using System.Collections.Generic;

namespace MgeniTrack.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Firstname { get; set; } = null!;

    public string? Secondname { get; set; }

    public string? Gender { get; set; }

    public string Passwordhash { get; set; } = null!;

    public string? IdNumber { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? UserStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastLogin { get; set; }

    public int? CreatedBy { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual Resident? Resident { get; set; }

    public virtual ICollection<SystemSetting> SystemSettings { get; set; } = new List<SystemSetting>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public virtual ICollection<Visit> VisitCheckedInByNavigations { get; set; } = new List<Visit>();

    public virtual ICollection<Visit> VisitCheckedOutByNavigations { get; set; } = new List<Visit>();
}
