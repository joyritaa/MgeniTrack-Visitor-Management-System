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
    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    public virtual DbSet<VisitorInvitation> VisitorInvitations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseMySql("server=localhost;database=mgenitrack;user=root",
             Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("utf8mb4_general_ci").HasCharSet("utf8mb4");

        modelBuilder.Entity<ActivityLog>(e => {
            e.HasKey(x => x.LogId).HasName("PRIMARY");
            e.ToTable("activity_logs");
            e.Property(x => x.LogId).HasColumnType("int(11)").HasColumnName("log_id");
            e.Property(x => x.ActionDetails).HasColumnType("text").HasColumnName("action_details");
            e.Property(x => x.ActionType).HasMaxLength(100).HasColumnName("action_type");
            e.Property(x => x.IpAddress).HasMaxLength(50).HasColumnName("ip_address");
            e.Property(x => x.RelatedEntityId).HasColumnType("int(11)").HasColumnName("related_entity_id");
            e.Property(x => x.RelatedEntityType).HasMaxLength(50).HasColumnName("related_entity_type");
            e.Property(x => x.TimeStamp).HasDefaultValueSql("current_timestamp()").HasColumnType("datetime").HasColumnName("time_stamp");
            e.Property(x => x.UserAgent).HasMaxLength(255).HasColumnName("user_agent");
            e.Property(x => x.UserId).HasColumnType("int(11)").HasColumnName("user_id");
            e.HasOne(x => x.User).WithMany(p => p.ActivityLogs).HasForeignKey(x => x.UserId).HasConstraintName("activity_logs_ibfk_1");
        });

        modelBuilder.Entity<Notification>(e => {
            e.HasKey(x => x.NotificationId).HasName("PRIMARY");
            e.ToTable("notifications");
            e.Property(x => x.NotificationId).HasColumnType("int(11)").HasColumnName("notification_id");
            e.Property(x => x.CreatedAt).HasDefaultValueSql("current_timestamp()").HasColumnType("datetime").HasColumnName("created_at");
            e.Property(x => x.IsRead).HasDefaultValueSql("'0'").HasColumnName("is_read");
            e.Property(x => x.Message).HasColumnType("text").HasColumnName("message");
            e.Property(x => x.ResidentId).HasColumnType("int(11)").HasColumnName("resident_id");
            e.Property(x => x.VisitId).HasColumnType("int(11)").HasColumnName("visit_id");
            e.Property(x => x.NotificationType).HasMaxLength(50).HasColumnName("notification_type");
            e.Property(x => x.Title).HasMaxLength(100).HasColumnName("title");
            e.Property(x => x.IsEmailSent).HasColumnName("is_email_sent");
            e.HasOne(x => x.Resident).WithMany(p => p.Notifications).HasForeignKey(x => x.ResidentId).HasConstraintName("notifications_ibfk_1");
            e.HasOne(x => x.Visit).WithMany(p => p.Notifications).HasForeignKey(x => x.VisitId).HasConstraintName("notifications_ibfk_2");
        });

        modelBuilder.Entity<Report>(e => {
            e.HasKey(x => x.ReportId).HasName("PRIMARY");
            e.ToTable("reports");
            e.Property(x => x.ReportId).HasColumnType("int(11)").HasColumnName("report_id");
            e.Property(x => x.DateFrom).HasColumnName("date_from");
            e.Property(x => x.DateTo).HasColumnName("date_to");
            e.Property(x => x.FileFormat).HasMaxLength(50).HasColumnName("file_format");
            e.Property(x => x.FilePath).HasMaxLength(255).HasColumnName("file_path");
            e.Property(x => x.GeneratedAt).HasDefaultValueSql("current_timestamp()").HasColumnType("datetime").HasColumnName("generated_at");
            e.Property(x => x.GeneratedBy).HasColumnType("int(11)").HasColumnName("generated_by");
            e.Property(x => x.ReportType).HasMaxLength(100).HasColumnName("report_type");
            e.Property(x => x.TotalCheckIns).HasColumnType("int(11)").HasColumnName("total_check_ins");
            e.Property(x => x.TotalCheckOuts).HasColumnType("int(11)").HasColumnName("total_check_outs");
            e.Property(x => x.TotalVisitors).HasColumnType("int(11)").HasColumnName("total_visitors");
            e.HasOne(x => x.GeneratedByNavigation).WithMany(p => p.Reports).HasForeignKey(x => x.GeneratedBy).HasConstraintName("reports_ibfk_1");
        });

        modelBuilder.Entity<Resident>(e => {
            e.HasKey(x => x.ResidentId).HasName("PRIMARY");
            e.ToTable("residents");
            e.HasIndex(x => x.UserId, "user_id").IsUnique();
            e.Property(x => x.ResidentId).HasColumnType("int(11)").HasColumnName("resident_id");
            e.Property(x => x.HouseNumber).HasMaxLength(20).HasColumnName("house_number");
            e.Property(x => x.UserId).HasColumnType("int(11)").HasColumnName("user_id");
            e.Property(x => x.UnitId).HasColumnType("int(11)").HasColumnName("unit_id");
            e.HasOne(x => x.User).WithOne(p => p.Resident).HasForeignKey<Resident>(x => x.UserId).HasConstraintName("residents_ibfk_1");
            e.HasOne(  x => x.Unit).WithMany(p => p.Residents).HasForeignKey(x => x.UnitId).IsRequired(false).HasConstraintName("fk_residents_unit");
        });

        modelBuilder.Entity<Role>(e => {
            e.HasKey(x => x.RoleId).HasName("PRIMARY");
            e.ToTable("roles");
            e.HasIndex(x => x.RoleName, "role_name").IsUnique();
            e.Property(x => x.RoleId).HasColumnType("int(11)").HasColumnName("role_id");
            e.Property(x => x.RoleDescription).HasMaxLength(255).HasColumnName("role_description");
            e.Property(x => x.RoleName).HasMaxLength(50).HasColumnName("role_name");
        });

        modelBuilder.Entity<SystemSetting>(e => {
            e.HasKey(x => x.SettingId).HasName("PRIMARY");
            e.ToTable("system_settings");
            e.Property(x => x.SettingId).HasColumnType("int(11)").HasColumnName("setting_id");
            e.Property(x => x.SettingKey).HasMaxLength(100).HasColumnName("setting_key");
            e.Property(x => x.SettingValue).HasMaxLength(255).HasColumnName("setting_value");
            e.Property(x => x.SysDescription).HasColumnType("text").HasColumnName("sys_description");
            e.Property(x => x.UpdatedAt).HasDefaultValueSql("current_timestamp()").HasColumnType("datetime").HasColumnName("updated_at");
            e.Property(x => x.UpdatedBy).HasColumnType("int(11)").HasColumnName("updated_by");
            e.HasOne(x => x.UpdatedByNavigation).WithMany(p => p.SystemSettings).HasForeignKey(x => x.UpdatedBy).HasConstraintName("system_settings_ibfk_1");
        });

        modelBuilder.Entity<Unit>(e => {
            e.HasKey(x => x.UnitId).HasName("PRIMARY");
            e.ToTable("units");
            e.HasIndex(x => x.UnitNumber, "unit_number").IsUnique();
            e.Property(x => x.UnitId).HasColumnType("int(11)").HasColumnName("unit_id");
            e.Property(x => x.UnitNumber).HasMaxLength(10).HasColumnName("unit_number");
            e.Property(x => x.Block).HasMaxLength(1).HasColumnName("block");
            e.Property(x => x.FloorNumber).HasColumnType("int(11)").HasColumnName("floor_number");
            e.Property(x => x.UnitPosition).HasColumnType("int(11)").HasColumnName("unit_position");
            e.Property(x => x.UnitType).HasColumnType("enum('Residential','BnB')").HasColumnName("unit_type");
            e.Property(x => x.IsOccupied).HasColumnName("is_occupied");
            e.Property(x => x.CreatedAt).HasDefaultValueSql("current_timestamp()").HasColumnType("datetime").HasColumnName("created_at");
        });

        modelBuilder.Entity<User>(e => {
            e.HasKey(x => x.UserId).HasName("PRIMARY");
            e.ToTable("users");
            e.HasIndex(x => x.Email, "email").IsUnique();
            e.HasIndex(x => x.IdNumber, "id_number").IsUnique();
            e.Property(x => x.UserId).HasColumnType("int(11)").HasColumnName("user_id");
            e.Property(x => x.CreatedAt).HasDefaultValueSql("current_timestamp()").HasColumnType("datetime").HasColumnName("created_at");
            e.Property(x => x.CreatedBy).HasColumnType("int(11)").HasColumnName("created_by");
            e.Property(x => x.Email).HasMaxLength(100).HasColumnName("email");
            e.Property(x => x.Firstname).HasMaxLength(50).HasColumnName("firstname");
            e.Property(x => x.Gender).HasColumnType("enum('Male','Female','Other')").HasColumnName("gender");
            e.Property(x => x.IdNumber).HasMaxLength(50).HasColumnName("id_number");
            e.Property(x => x.LastLogin).HasColumnType("datetime").HasColumnName("last_login");
            e.Property(x => x.Passwordhash).HasMaxLength(255).HasColumnName("passwordhash");
            e.Property(x => x.PhoneNumber).HasMaxLength(20).HasColumnName("phone_number");
            e.Property(x => x.Secondname).HasMaxLength(50).HasColumnName("secondname");
            e.Property(x => x.UserStatus).HasDefaultValueSql("'Active'").HasColumnType("enum('Active','Inactive')").HasColumnName("user_status");
            e.Property(x => x.SessionExpiresAt).HasColumnType("datetime").HasColumnName("session_expires_at");
            e.Property(x => x.LastActivityAt).HasColumnType("datetime").HasColumnName("last_activity_at");
            e.HasOne(x => x.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(x => x.CreatedBy).IsRequired(false).HasConstraintName("users_ibfk_1");
        });

        modelBuilder.Entity<UserRole>(e => {
            e.HasKey(x => x.UserRoleId).HasName("PRIMARY");
            e.ToTable("user_roles");
            e.HasIndex(x => new { x.UserId, x.RoleId }, "user_id").IsUnique();
            e.Property(x => x.UserRoleId).HasColumnType("int(11)").HasColumnName("user_role_id");
            e.Property(x => x.AssignedAt).HasDefaultValueSql("current_timestamp()").HasColumnType("datetime").HasColumnName("assigned_at");
            e.Property(x => x.RoleId).HasColumnType("int(11)").HasColumnName("role_id");
            e.Property(x => x.Shift).HasColumnType("enum('Day','Night')").HasColumnName("shift");
            e.Property(x => x.UserId).HasColumnType("int(11)").HasColumnName("user_id");
            e.Property(x => x.UserStatus).HasDefaultValueSql("'Active'").HasColumnType("enum('Active','Inactive')").HasColumnName("user_status");
            e.HasOne(x => x.Role).WithMany(p => p.UserRoles).HasForeignKey(x => x.RoleId).HasConstraintName("user_roles_ibfk_2");
            e.HasOne(x => x.User).WithMany(p => p.UserRoles).HasForeignKey(x => x.UserId).HasConstraintName("user_roles_ibfk_1");
        });

        modelBuilder.Entity<Visit>(e => {
            e.HasKey(x => x.VisitId).HasName("PRIMARY");
            e.ToTable("visits");
            e.Property(x => x.VisitId).HasColumnType("int(11)").HasColumnName("visit_id");
            e.Property(x => x.CarRegistration).HasMaxLength(50).HasColumnName("car_registration");
            e.Property(x => x.CheckInMethod).HasColumnType("enum('QR','Manual')").HasColumnName("check_in_method");
            e.Property(x => x.CheckedInBy).HasColumnType("int(11)").HasColumnName("checked_in_by");
            e.Property(x => x.CheckedOutBy).HasColumnType("int(11)").HasColumnName("checked_out_by");
            e.Property(x => x.CreatedAt).HasDefaultValueSql("current_timestamp()").HasColumnType("datetime").HasColumnName("created_at");
            e.Property(x => x.HouseNumber).HasMaxLength(20).HasColumnName("house_number");
            e.Property(x => x.InvitationId).HasColumnType("int(11)").HasColumnName("invitation_id");
            e.Property(x => x.NumberOfOccupants).HasDefaultValueSql("'1'").HasColumnType("int(11)").HasColumnName("number_of_occupants");
            e.Property(x => x.PurposeOfVisit).HasMaxLength(255).HasColumnName("purpose_of_visit");
            e.Property(x => x.QrCode).HasMaxLength(255).HasColumnName("qr_code");
            e.Property(x => x.TimeIn).HasColumnType("datetime").HasColumnName("time_in");
            e.Property(x => x.TimeOut).HasColumnType("datetime").HasColumnName("time_out");
            e.Property(x => x.VisitDuration).HasColumnType("int(11)").HasColumnName("visit_duration");
            e.Property(x => x.VisitStatus).HasDefaultValueSql("'CheckedIn'").HasColumnType("enum('CheckedIn','CheckedOut')").HasColumnName("visit_status");
            e.Property(x => x.VisitorId).HasColumnType("int(11)").HasColumnName("visitor_id");
            e.HasOne(x => x.CheckedInByNavigation).WithMany(p => p.VisitCheckedInByNavigations).HasForeignKey(x => x.CheckedInBy).IsRequired(false).HasConstraintName("visits_ibfk_2");
            e.HasOne(x => x.CheckedOutByNavigation).WithMany(p => p.VisitCheckedOutByNavigations).HasForeignKey(x => x.CheckedOutBy).IsRequired(false).HasConstraintName("visits_ibfk_3");
            e.HasOne(x => x.Invitation).WithMany(p => p.Visits).HasForeignKey(x => x.InvitationId).IsRequired(false).HasConstraintName("visits_ibfk_4");
            e.HasOne(x => x.Visitor).WithMany(p => p.Visits).HasForeignKey(x => x.VisitorId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("visits_ibfk_1");
        });

        modelBuilder.Entity<Visitor>(e => {
            e.HasKey(x => x.VisitorId).HasName("PRIMARY");
            e.ToTable("visitors");
            e.Property(x => x.VisitorId).HasColumnType("int(11)").HasColumnName("visitor_id");
            e.Property(x => x.ContactNumber).HasMaxLength(20).HasColumnName("contact_number");
            e.Property(x => x.CreatedAt).HasDefaultValueSql("current_timestamp()").HasColumnType("datetime").HasColumnName("created_at");
            e.Property(x => x.FirstVisitDate).HasColumnType("datetime").HasColumnName("first_visit_date");
            e.Property(x => x.FullName).HasMaxLength(100).HasColumnName("full_name");
            e.Property(x => x.IdNumber).HasMaxLength(50).HasColumnName("id_number");
            e.Property(x => x.InvitedViaInvitationId).HasColumnType("int(11)").HasColumnName("invited_via_invitation_id");
            e.Property(x => x.PhotoUrl).HasMaxLength(500).HasColumnName("photo_url");
            e.Property(x => x.TotalVisits).HasDefaultValueSql("'0'").HasColumnType("int(11)").HasColumnName("total_visits");
            e.Property(x => x.UpdatedAt).HasColumnType("datetime").HasColumnName("updated_at");
            e.HasOne(x => x.InvitedViaInvitation).WithMany(p => p.Visitors)
                .HasForeignKey(x => x.InvitedViaInvitationId).IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull).HasConstraintName("visitors_ibfk_1");
        });

        modelBuilder.Entity<VisitorInvitation>(e => {
            e.HasKey(x => x.InvitationId).HasName("PRIMARY");
            e.ToTable("visitor_invitations");
            e.HasIndex(x => x.InvitationToken, "invitation_token").IsUnique();
            e.Property(x => x.InvitationId).HasColumnType("int(11)").HasColumnName("invitation_id");
            e.Property(x => x.CreatedAt).HasDefaultValueSql("current_timestamp()").HasColumnType("datetime").HasColumnName("created_at");
            e.Property(x => x.ExpectedDate).HasColumnName("expected_date");
            e.Property(x => x.InvitationToken).HasColumnName("invitation_token");
            e.Property(x => x.PurposeOfVisit).HasMaxLength(255).HasColumnName("purpose_of_visit");
            e.Property(x => x.QrCodePath).HasMaxLength(255).HasColumnName("qr_code_path");
            e.Property(x => x.ResidentId).HasColumnType("int(11)").HasColumnName("resident_id");
            e.Property(x => x.VisitStatus).HasDefaultValueSql("'Pending'")
                .HasColumnType("enum('Pending','Arrived','Expired','Cancelled')").HasColumnName("visit_status");
            e.Property(x => x.VisitorEmail).HasMaxLength(100).HasColumnName("visitor_email");
            e.Property(x => x.VisitorName).HasMaxLength(100).HasColumnName("visitor_name");
            e.Property(x => x.VisitorPhone).HasMaxLength(20).HasColumnName("visitor_phone");
            e.HasOne(x => x.Resident).WithMany(p => p.VisitorInvitations)
                .HasForeignKey(x => x.ResidentId).OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("visitor_invitations_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}