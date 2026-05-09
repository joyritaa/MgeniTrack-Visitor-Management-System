using MgeniTrack.Services;
using MgeniTrack.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MgeniTrack.Controllers
{
 
    [Authorize(Roles = "SuperAdmin,PropertyManager")]
    public class AnalyticsController : Controller
    {
        private readonly VisitAnalyticsService _analytics;

        public AnalyticsController(VisitAnalyticsService analytics)
        {
            _analytics = analytics;
        }

        public async Task<IActionResult> Index(
            string? from = null,
            string? to = null,
            string? block = null,
            string? purpose = null)
        {
            var dateFrom = DateTime.TryParse(from, out var f)
                ? f : DateTime.Today.AddDays(-30);
            var dateTo = DateTime.TryParse(to, out var t)
                ? t : DateTime.Today;

            if (dateTo > DateTime.Today) dateTo = DateTime.Today;
            if (dateFrom > dateTo) dateFrom = dateTo.AddDays(-30);

            var vm = await _analytics.BuildAsync(dateFrom, dateTo, block, purpose);
            return View(vm);
        }

        //Export full analytics as CSV 
        public async Task<IActionResult> ExportCsv(
            string? from = null,
            string? to = null,
            string? block = null,
            string? purpose = null)
        {
            var dateFrom = DateTime.TryParse(from, out var f) ? f : DateTime.Today.AddDays(-30);
            var dateTo = DateTime.TryParse(to, out var t) ? t : DateTime.Today;
            if (dateTo > DateTime.Today) dateTo = DateTime.Today;

            var vm = await _analytics.BuildAsync(dateFrom, dateTo, block, purpose);

            var sb = new StringBuilder();

            // Header 
            sb.AppendLine("MgeniTrack — Visit Duration Analytics Export");
            sb.AppendLine($"Period:,{dateFrom:dd MMM yyyy} – {dateTo:dd MMM yyyy}");
            if (!string.IsNullOrEmpty(block)) sb.AppendLine($"Block Filter:,Block {block}");
            if (!string.IsNullOrEmpty(purpose)) sb.AppendLine($"Purpose Filter:,{purpose}");
            sb.AppendLine($"Generated:,{DateTime.Now:dd MMM yyyy HH:mm}");
            sb.AppendLine();

            //  Section 1: Summary KPIs 
            sb.AppendLine("=== SUMMARY ===");
            sb.AppendLine($"Total Visits,{vm.TotalVisitsInRange}");
            sb.AppendLine($"Unique Visitors,{vm.TotalUniqueVisitorsInRange}");
            sb.AppendLine($"Overall Average Duration (min),{Math.Round(vm.OverallAverageDuration, 1)}");
            sb.AppendLine($"Long-Stay Alerts,{vm.LongVisitAlertCount}");
            sb.AppendLine($"Residential Visits,{vm.ResidentialVisitsInRange}");
            sb.AppendLine($"BnB Visits,{vm.BnbVisitsInRange}");
            sb.AppendLine($"Peak Hour,{vm.DateRangeStats.PeakHour}");
            sb.AppendLine($"Most Visited Unit,{vm.DateRangeStats.MostVisitedUnit} ({vm.DateRangeStats.MostVisitedUnitCount} visits)");
            sb.AppendLine($"Most Common Purpose,{vm.DateRangeStats.MostCommonPurpose}");
            sb.AppendLine();

            // Section 2: Avg Duration by Purpose 
            sb.AppendLine("=== AVERAGE DURATION BY PURPOSE ===");
            sb.AppendLine("Purpose,Average Duration (min)");
            foreach (var kv in vm.AverageDurationByPurpose)
                sb.AppendLine($"\"{kv.Key}\",{kv.Value}");
            sb.AppendLine();

            //Section 3: Top Frequent Visitors 
            sb.AppendLine("=== TOP FREQUENT VISITORS ===");
            sb.AppendLine("Rank,Visitor Name,Contact,Visit Count,Most Visited Unit,Avg Duration (min),Last Visit");
            for (int i = 0; i < vm.TopFrequentVisitors.Count; i++)
            {
                var v = vm.TopFrequentVisitors[i];
                sb.AppendLine($"{i + 1},\"{v.VisitorName}\",\"{v.ContactNumber ?? "—"}\",{v.VisitCount},\"{v.MostVisitedUnit ?? "—"}\",{v.AverageDurationMinutes},{v.LastVisit:dd MMM yyyy}");
            }
            sb.AppendLine();

            //Section 4: Long-Stay Alerts 
            sb.AppendLine("=== LONG-STAY ALERTS ===");
            sb.AppendLine("Visitor,House,Purpose,Check-In,Duration (min),Purpose Avg (min),Multiplier,Flag");
            foreach (var a in vm.LongVisitAlerts)
                sb.AppendLine($"\"{a.VisitorName}\",\"{a.HouseNumber}\",\"{a.Purpose}\",{a.CheckInTime:dd MMM yyyy HH:mm},{a.DurationMinutes},{Math.Round(a.AverageDurationForPurpose, 1)},{a.Multiplier},\"{a.Flag}\"");
            sb.AppendLine();

            //Section 5: Per-Resident Trends 
            sb.AppendLine("=== PER-RESIDENT TRENDS ===");
            sb.AppendLine("Resident,House,Unit Type,Total Visits,Unique Visitors,Avg Duration (min),Top Purpose");
            foreach (var kv in vm.ResidentTrendData.OrderByDescending(r => r.Value.TotalVisits))
            {
                var r = kv.Value;
                sb.AppendLine($"\"{r.ResidentName}\",\"{r.HouseNumber}\",\"{r.UnitType}\",{r.TotalVisits},{r.UniqueVisitors},{r.AverageDurationMinutes},\"{r.TopPurpose ?? "—"}\"");
            }
            sb.AppendLine();

            //Section 6: Daily Check-Ins 
            sb.AppendLine("=== DAILY CHECK-INS ===");
            sb.AppendLine("Date,Total Check-Ins");
            foreach (var d in vm.DateRangeStats.DailyCheckIns)
                sb.AppendLine($"{d.Date:dd MMM yyyy},{d.Count}");

            var fileName = $"MgeniTrack_Analytics_{dateFrom:yyyyMMdd}_{dateTo:yyyyMMdd}.csv";
            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", fileName);
        }

        //Resident drill-down (AJAX, returns partial JSON for modal) 
        [HttpGet]
        public IActionResult ResidentDetail(int residentId,
            string? from = null, string? to = null)
        {
            return RedirectToAction(nameof(Index),
                new { from, to, residentId });
        }

        // JSON endpoint used by SignalR-less live refresh 
        [HttpGet]
        public async Task<IActionResult> LiveStats()
        {
            var vm = await _analytics.BuildAsync(DateTime.Today, DateTime.Today);
            return Json(new
            {
                totalVisitsToday = vm.TotalVisitsInRange,
                currentlyInside = vm.DateRangeStats.TotalVisits - vm.DateRangeStats.TotalCheckOuts,
                longVisitAlerts = vm.LongVisitAlertCount,
                avgDuration = Math.Round(vm.OverallAverageDuration, 1)
            });
        }

        public async Task<IActionResult> Trends(string? from = null, string? to = null)
        {
            var dateFrom = DateTime.TryParse(from, out var f)
                ? f : DateTime.Today.AddDays(-30);

            var dateTo = DateTime.TryParse(to, out var t)
                ? t : DateTime.Today;

            var vm = await _analytics.BuildAsync(dateFrom, dateTo);

            return View(vm);
        }

        public async Task<IActionResult> FrequentVisitors(string? from = null, string? to = null)
        {
            var dateFrom = DateTime.TryParse(from, out var f)
                ? f : DateTime.Today.AddDays(-30);

            var dateTo = DateTime.TryParse(to, out var t)
                ? t : DateTime.Today;

            var vm = await _analytics.BuildAsync(dateFrom, dateTo);

            return View(vm);
        }

        public async Task<IActionResult> Alerts(string? from = null, string? to = null)
        {
            var dateFrom = DateTime.TryParse(from, out var f)
                ? f : DateTime.Today.AddDays(-30);

            var dateTo = DateTime.TryParse(to, out var t)
                ? t : DateTime.Today;

            var vm = await _analytics.BuildAsync(dateFrom, dateTo);

            return View(vm);
        }

        public async Task<IActionResult> Residents(string? from = null, string? to = null)
        {
            var dateFrom = DateTime.TryParse(from, out var f)
                ? f : DateTime.Today.AddDays(-30);

            var dateTo = DateTime.TryParse(to, out var t)
                ? t : DateTime.Today;

            var vm = await _analytics.BuildAsync(dateFrom, dateTo);

            return View(vm);
        }

    }
}
