using System;
using System.Collections.Generic;

namespace MgeniTrack.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public int? GeneratedBy { get; set; }

    public string? ReportType { get; set; }

    public DateOnly? DateFrom { get; set; }

    public DateOnly? DateTo { get; set; }

    public int? TotalVisitors { get; set; }

    public int? TotalCheckIns { get; set; }

    public int? TotalCheckOuts { get; set; }

    public string? FilePath { get; set; }

    public string? FileFormat { get; set; }

    public DateTime? GeneratedAt { get; set; }

    public virtual User? GeneratedByNavigation { get; set; }
}
