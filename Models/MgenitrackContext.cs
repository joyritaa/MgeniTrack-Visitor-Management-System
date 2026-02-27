using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MgeniTrack.Models;

public partial class MgenitrackContext : DbContext
{
    public MgenitrackContext()
    {
    }

    public MgenitrackContext(DbContextOptions<MgenitrackContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Resident> Residents { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SystemSetting> SystemSettings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    public virtual DbSet<VisitorInvitation> VisitorInvitations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

//warning:To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=mgenitrack;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PRIMARY");

            entity.ToTable("activity_logs");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.LogId)
                .HasColumnType("int(11)")
                .HasColumnName("log_id");
            entity.Property(e => e.ActionDetails)
                .HasColumnType("text")
                .HasColumnName("action_details");
            entity.Property(e => e.ActionType)
                .HasMaxLength(100)
                .HasColumnName("action_type");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(50)
                .HasColumnName("ip_address");
            entity.Property(e => e.RelatedEntityId)
                .HasColumnType("int(11)")
                .HasColumnName("related_entity_id");
            entity.Property(e => e.RelatedEntityType)
                .HasMaxLength(50)
                .HasColumnName("related_entity_type");
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("time_stamp");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("activity_logs_ibfk_1");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PRIMARY");

            entity.ToTable("notifications");

            entity.HasIndex(e => e.ResidentId, "resident_id");

            entity.HasIndex(e => e.VisitId, "visit_id");

            entity.Property(e => e.NotificationId)
                .HasColumnType("int(11)")
                .HasColumnName("notification_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsRead)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_read");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");
            entity.Property(e => e.ResidentId)
                .HasColumnType("int(11)")
                .HasColumnName("resident_id");
            entity.Property(e => e.VisitId)
                .HasColumnType("int(11)")
                .HasColumnName("visit_id");

            entity.HasOne(d => d.Resident).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.ResidentId)
                .HasConstraintName("notifications_ibfk_1");

            entity.HasOne(d => d.Visit).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.VisitId)
                .HasConstraintName("notifications_ibfk_2");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PRIMARY");

            entity.ToTable("reports");

            entity.HasIndex(e => e.GeneratedBy, "generated_by");

            entity.Property(e => e.ReportId)
                .HasColumnType("int(11)")
                .HasColumnName("report_id");
            entity.Property(e => e.DateFrom).HasColumnName("date_from");
            entity.Property(e => e.DateTo).HasColumnName("date_to");
            entity.Property(e => e.FileFormat)
                .HasMaxLength(50)
                .HasColumnName("file_format");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("file_path");
            entity.Property(e => e.GeneratedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("generated_at");
            entity.Property(e => e.GeneratedBy)
                .HasColumnType("int(11)")
                .HasColumnName("generated_by");
            entity.Property(e => e.ReportType)
                .HasMaxLength(100)
                .HasColumnName("report_type");
            entity.Property(e => e.TotalCheckIns)
                .HasColumnType("int(11)")
                .HasColumnName("total_check_ins");
            entity.Property(e => e.TotalCheckOuts)
                .HasColumnType("int(11)")
                .HasColumnName("total_check_outs");
            entity.Property(e => e.TotalVisitors)
                .HasColumnType("int(11)")
                .HasColumnName("total_visitors");

            entity.HasOne(d => d.GeneratedByNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.GeneratedBy)
                .HasConstraintName("reports_ibfk_1");
        });

        modelBuilder.Entity<Resident>(entity =>
        {
            entity.HasKey(e => e.ResidentId).HasName("PRIMARY");

            entity.ToTable("residents");

            entity.HasIndex(e => e.UserId, "user_id").IsUnique();

            entity.Property(e => e.ResidentId)
                .HasColumnType("int(11)")
                .HasColumnName("resident_id");
            entity.Property(e => e.HouseNumber)
                .HasMaxLength(20)
                .HasColumnName("house_number");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Resident)
                .HasForeignKey<Resident>(d => d.UserId)
                .HasConstraintName("residents_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "role_name").IsUnique();

            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.RoleDescription)
                .HasMaxLength(255)
                .HasColumnName("role_description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<SystemSetting>(entity =>
        {
            entity.HasKey(e => e.SettingId).HasName("PRIMARY");

            entity.ToTable("system_settings");

            entity.HasIndex(e => e.SettingKey, "setting_key").IsUnique();

            entity.HasIndex(e => e.UpdatedBy, "updated_by");

            entity.Property(e => e.SettingId)
                .HasColumnType("int(11)")
                .HasColumnName("setting_id");
            entity.Property(e => e.SettingKey)
                .HasMaxLength(100)
                .HasColumnName("setting_key");
            entity.Property(e => e.SettingValue)
                .HasMaxLength(255)
                .HasColumnName("setting_value");
            entity.Property(e => e.SysDescription)
                .HasColumnType("text")
                .HasColumnName("sys_description");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("int(11)")
                .HasColumnName("updated_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.SystemSettings)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("system_settings_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.CreatedBy, "created_by");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.IdNumber, "id_number").IsUnique();

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("int(11)")
                .HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Gender)
                .HasColumnType("enum('Male','Female','Other')")
                .HasColumnName("gender");
            entity.Property(e => e.IdNumber)
                .HasMaxLength(50)
                .HasColumnName("id_number");
            entity.Property(e => e.LastLogin)
                .HasColumnType("datetime")
                .HasColumnName("last_login");
            entity.Property(e => e.Passwordhash)
                .HasMaxLength(255)
                .HasColumnName("passwordhash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Secondname)
                .HasMaxLength(50)
                .HasColumnName("secondname");
            entity.Property(e => e.UserStatus)
                .HasDefaultValueSql("'Active'")
                .HasColumnType("enum('Active','Inactive')")
                .HasColumnName("user_status");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("users_ibfk_1");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PRIMARY");

            entity.ToTable("user_roles");

            entity.HasIndex(e => e.RoleId, "role_id");

            entity.HasIndex(e => new { e.UserId, e.RoleId }, "user_id").IsUnique();

            entity.Property(e => e.UserRoleId)
                .HasColumnType("int(11)")
                .HasColumnName("user_role_id");
            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("assigned_at");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.Shift)
                .HasColumnType("enum('Day','Night')")
                .HasColumnName("shift");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.UserStatus)
                .HasDefaultValueSql("'Active'")
                .HasColumnType("enum('Active','Inactive')")
                .HasColumnName("user_status");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("user_roles_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_roles_ibfk_1");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.VisitId).HasName("PRIMARY");

            entity.ToTable("visits");

            entity.HasIndex(e => e.CheckedInBy, "checked_in_by");

            entity.HasIndex(e => e.CheckedOutBy, "checked_out_by");

            entity.HasIndex(e => e.InvitationId, "invitation_id");

            entity.HasIndex(e => e.VisitorId, "visitor_id");

            entity.Property(e => e.VisitId)
                .HasColumnType("int(11)")
                .HasColumnName("visit_id");
            entity.Property(e => e.CarRegistration)
                .HasMaxLength(50)
                .HasColumnName("car_registration");
            entity.Property(e => e.CheckInMethod)
                .HasColumnType("enum('QR','Manual')")
                .HasColumnName("check_in_method");
            entity.Property(e => e.CheckedInBy)
                .HasColumnType("int(11)")
                .HasColumnName("checked_in_by");
            entity.Property(e => e.CheckedOutBy)
                .HasColumnType("int(11)")
                .HasColumnName("checked_out_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.HouseNumber)
                .HasMaxLength(20)
                .HasColumnName("house_number");
            entity.Property(e => e.InvitationId)
                .HasColumnType("int(11)")
                .HasColumnName("invitation_id");
            entity.Property(e => e.NumberOfOccupants)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)")
                .HasColumnName("number_of_occupants");
            entity.Property(e => e.PurposeOfVisit)
                .HasMaxLength(255)
                .HasColumnName("purpose_of_visit");
            entity.Property(e => e.QrCode)
                .HasMaxLength(255)
                .HasColumnName("qr_code");
            entity.Property(e => e.TimeIn)
                .HasColumnType("datetime")
                .HasColumnName("time_in");
            entity.Property(e => e.TimeOut)
                .HasColumnType("datetime")
                .HasColumnName("time_out");
            entity.Property(e => e.VisitDuration)
                .HasColumnType("int(11)")
                .HasColumnName("visit_duration");
            entity.Property(e => e.VisitStatus)
                .HasDefaultValueSql("'CheckedIn'")
                .HasColumnType("enum('CheckedIn','CheckedOut')")
                .HasColumnName("visit_status");
            entity.Property(e => e.VisitorId)
                .HasColumnType("int(11)")
                .HasColumnName("visitor_id");

            entity.HasOne(d => d.CheckedInByNavigation).WithMany(p => p.VisitCheckedInByNavigations)
                .HasForeignKey(d => d.CheckedInBy)
                .HasConstraintName("visits_ibfk_2");

            entity.HasOne(d => d.CheckedOutByNavigation).WithMany(p => p.VisitCheckedOutByNavigations)
                .HasForeignKey(d => d.CheckedOutBy)
                .HasConstraintName("visits_ibfk_3");

            entity.HasOne(d => d.Invitation).WithMany(p => p.Visits)
                .HasForeignKey(d => d.InvitationId)
                .HasConstraintName("visits_ibfk_4");

            entity.HasOne(d => d.Visitor).WithMany(p => p.Visits)
                .HasForeignKey(d => d.VisitorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("visits_ibfk_1");
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.VisitorId).HasName("PRIMARY");

            entity.ToTable("visitors");

            entity.HasIndex(e => e.InvitedViaInvitationId, "invited_via_invitation_id");

            entity.Property(e => e.VisitorId)
                .HasColumnType("int(11)")
                .HasColumnName("visitor_id");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .HasColumnName("contact_number");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FirstVisitDate)
                .HasColumnType("datetime")
                .HasColumnName("first_visit_date");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.IdNumber)
                .HasMaxLength(50)
                .HasColumnName("id_number");
            entity.Property(e => e.InvitedViaInvitationId)
                .HasColumnType("int(11)")
                .HasColumnName("invited_via_invitation_id");
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(255)
                .HasColumnName("photo_url");
            entity.Property(e => e.TotalVisits)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)")
                .HasColumnName("total_visits");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.InvitedViaInvitation).WithMany(p => p.Visitors)
                .HasForeignKey(d => d.InvitedViaInvitationId)
                .HasConstraintName("visitors_ibfk_1");
        });

        modelBuilder.Entity<VisitorInvitation>(entity =>
        {
            entity.HasKey(e => e.InvitationId).HasName("PRIMARY");

            entity.ToTable("visitor_invitations");

            entity.HasIndex(e => e.InvitationToken, "invitation_token").IsUnique();

            entity.HasIndex(e => e.ResidentId, "resident_id");

            entity.Property(e => e.InvitationId)
                .HasColumnType("int(11)")
                .HasColumnName("invitation_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpectedDate).HasColumnName("expected_date");
            entity.Property(e => e.InvitationToken).HasColumnName("invitation_token");
            entity.Property(e => e.PurposeOfVisit)
                .HasMaxLength(255)
                .HasColumnName("purpose_of_visit");
            entity.Property(e => e.QrCodePath)
                .HasMaxLength(255)
                .HasColumnName("qr_code_path");
            entity.Property(e => e.ResidentId)
                .HasColumnType("int(11)")
                .HasColumnName("resident_id");
            entity.Property(e => e.VisitStatus)
                .HasDefaultValueSql("'Pending'")
                .HasColumnType("enum('Pending','Arrived','Expired','Cancelled')")
                .HasColumnName("visit_status");
            entity.Property(e => e.VisitorEmail)
                .HasMaxLength(100)
                .HasColumnName("visitor_email");
            entity.Property(e => e.VisitorName)
                .HasMaxLength(100)
                .HasColumnName("visitor_name");
            entity.Property(e => e.VisitorPhone)
                .HasMaxLength(20)
                .HasColumnName("visitor_phone");

            entity.HasOne(d => d.Resident).WithMany(p => p.VisitorInvitations)
                .HasForeignKey(d => d.ResidentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("visitor_invitations_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
