using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MgeniTrack.Models;
using System.Security.Claims;
using System.Text;

namespace MgeniTrack.Controllers
{
    [Authorize(Roles = "SuperAdmin,PropertyManager")]
    public class ReportsController : Controller
    {
        private readonly MgenitrackContext _context;

        public ReportsController(MgenitrackContext context)
        {
            _context = context;
        }

        //List all generated reports 
        public async Task<IActionResult> Index()
        {
            var reports = await _context.Reports
                .Include(r => r.GeneratedByNavigation)
                .OrderByDescending(r => r.GeneratedAt)
                .ToListAsync();

            return View(reports);
        }

        //Report generation form
        public IActionResult Generate()
        {
            ViewBag.DateFrom = DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd");
            ViewBag.DateTo = DateTime.Today.ToString("yyyy-MM-dd");
            return View();
        }

        // Generate report POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Generate(string reportType, string dateFrom, string dateTo, string format)
        {
            if (!DateTime.TryParse(dateFrom, out var from) ||
                !DateTime.TryParse(dateTo, out var to))
            {
                TempData["Error"] = "Invalid date range.";
                return RedirectToAction(nameof(Generate));
            }

            to = to.AddDays(1).AddSeconds(-1); // include full end day

            var dateFromOnly = DateOnly.FromDateTime(from);
            var dateToOnly = DateOnly.FromDateTime(to);

            // Gather data based on date range
            var visits = await _context.Visits
                .Include(v => v.Visitor)
                .Include(v => v.CheckedInByNavigation)
                .Where(v => v.TimeIn.HasValue &&
                            v.TimeIn.Value >= from &&
                            v.TimeIn.Value <= to)
                .OrderBy(v => v.TimeIn)
                .ToListAsync();

            var totalVisitors = visits.Select(v => v.VisitorId).Distinct().Count();
            var totalCheckIns = visits.Count;
            var totalCheckOuts = visits.Count(v => v.VisitStatus == "CheckedOut");

            // Get logged-in user
            var email = User.FindFirstValue(ClaimTypes.Name);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            // Save report record to DB
            var report = new Report
            {
                GeneratedBy = user?.UserId,
                ReportType = reportType,
                DateFrom = dateFromOnly,
                DateTo = dateToOnly,
                TotalVisitors = totalVisitors,
                TotalCheckIns = totalCheckIns,
                TotalCheckOuts = totalCheckOuts,
                FileFormat = format.ToUpper(),
                GeneratedAt = DateTime.Now,
                FilePath = $"/reports/{reportType}_{DateTime.Now:yyyyMMdd_HHmmss}.{format.ToLower()}"
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            // Generate and return CSV file
            if (format.ToLower() == "csv")
            {
                var csv = BuildCsv(visits, reportType, from, to,
                                   totalVisitors, totalCheckIns, totalCheckOuts);
                var fileName = $"MgeniTrack_{reportType}_{DateTime.Now:yyyyMMdd}.csv";
                return File(Encoding.UTF8.GetBytes(csv), "text/csv", fileName);
            }

            // For non-CSV, redirect to preview
            TempData["Success"] = $"Report generated: {totalCheckIns} check-ins, {totalCheckOuts} check-outs.";
            return RedirectToAction(nameof(Preview), new { id = report.ReportId });
        }

        // ── Report preview (HTML view) ───────────────────────────
        public async Task<IActionResult> Preview(int id)
        {
            var report = await _context.Reports
                .Include(r => r.GeneratedByNavigation)
                .FirstOrDefaultAsync(r => r.ReportId == id);

            if (report == null) return NotFound();

            // Re-query visit data for this report's date range
            DateTime? from = report.DateFrom.HasValue
                ? report.DateFrom.Value.ToDateTime(TimeOnly.MinValue) : null;
            DateTime? to = report.DateTo.HasValue
                ? report.DateTo.Value.ToDateTime(TimeOnly.MaxValue) : null;

            var visits = await _context.Visits
                .Include(v => v.Visitor)
                .Include(v => v.CheckedInByNavigation)
                .Where(v => v.TimeIn.HasValue &&
                            (!from.HasValue || v.TimeIn.Value >= from.Value) &&
                            (!to.HasValue || v.TimeIn.Value <= to.Value))
                .OrderBy(v => v.TimeIn)
                .ToListAsync();

            ViewBag.Report = report;
            ViewBag.Visits = visits;
            return View();
        }

        // ── Download CSV for existing report ────────────────────
        public async Task<IActionResult> Download(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) return NotFound();

            DateTime? from = report.DateFrom.HasValue
                ? report.DateFrom.Value.ToDateTime(TimeOnly.MinValue) : null;
            DateTime? to = report.DateTo.HasValue
                ? report.DateTo.Value.ToDateTime(TimeOnly.MaxValue) : null;

            var visits = await _context.Visits
                .Include(v => v.Visitor)
                .Include(v => v.CheckedInByNavigation)
                .Where(v => v.TimeIn.HasValue &&
                            (!from.HasValue || v.TimeIn.Value >= from.Value) &&
                            (!to.HasValue || v.TimeIn.Value <= to.Value))
                .OrderBy(v => v.TimeIn)
                .ToListAsync();

            var csv = BuildCsv(visits, report.ReportType ?? "Visits",
                                    from ?? DateTime.MinValue, to ?? DateTime.MaxValue,
                                    report.TotalVisitors ?? 0,
                                    report.TotalCheckIns ?? 0,
                                    report.TotalCheckOuts ?? 0);
            var fileName = $"MgeniTrack_{report.ReportType}_{report.GeneratedAt:yyyyMMdd}.csv";
            return File(Encoding.UTF8.GetBytes(csv), "text/csv", fileName);
        }

        // CSV builder 
        private static string BuildCsv(
            List<Visit> visits, string reportType,
            DateTime from, DateTime to,
            int totalVisitors, int totalCheckIns, int totalCheckOuts)
        {
            var sb = new StringBuilder();

            sb.AppendLine("MgeniTrack Digital Visitor Management System");
            sb.AppendLine($"Report Type:,{reportType}");
            sb.AppendLine($"Date Range:,{from:dd MMM yyyy} - {to:dd MMM yyyy}");
            sb.AppendLine($"Generated:,{DateTime.Now:dd MMM yyyy HH:mm}");
            sb.AppendLine();
            sb.AppendLine($"Summary");
            sb.AppendLine($"Total Unique Visitors:,{totalVisitors}");
            sb.AppendLine($"Total Check-Ins:,{totalCheckIns}");
            sb.AppendLine($"Total Check-Outs:,{totalCheckOuts}");
            sb.AppendLine();
            sb.AppendLine("Visitor Name,ID Number,Contact,House,Purpose,Check-In Method,Time In,Time Out,Duration (mins),Status");

            foreach (var v in visits)
            {
                sb.AppendLine(string.Join(",",
                    $"\"{v.Visitor?.FullName ?? "—"}\"",
                    $"\"{v.Visitor?.IdNumber ?? "—"}\"",
                    $"\"{v.Visitor?.ContactNumber ?? "—"}\"",
                    $"\"{v.HouseNumber ?? "—"}\"",
                    $"\"{v.PurposeOfVisit ?? "—"}\"",
                    $"\"{v.CheckInMethod ?? "Manual"}\"",
                    $"\"{v.TimeIn?.ToString("dd MMM yyyy HH:mm") ?? "—"}\"",
                    $"\"{v.TimeOut?.ToString("dd MMM yyyy HH:mm") ?? "—"}\"",
                    $"\"{v.VisitDuration?.ToString() ?? "—"}\"",
                    $"\"{v.VisitStatus ?? "—"}\""
                ));
            }

            return sb.ToString();
        }
    }
}