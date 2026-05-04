using MgeniTrack.Models;
using MgeniTrack.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MgeniTrack.Services
{
    /// Computes all analytics data for the VisitAnalyticsViewModel.
    public class VisitAnalyticsService
    {
        private readonly MgenitrackContext _context;

        private const double LongVisitMultiplier = 3.0;

        private static readonly string[] ChartColors =
        {
            "#3b82f6","#10b981","#f59e0b","#ef4444",
            "#8b5cf6","#06b6d4","#f97316","#84cc16",
            "#ec4899","#6366f1","#14b8a6","#a78bfa"
        };

        public VisitAnalyticsService(MgenitrackContext context)
        {
            _context = context;
        }

        public async Task<VisitAnalyticsViewModel> BuildAsync(
            DateTime from,
            DateTime to,
            string? filterBlock = null,
            string? filterPurpose = null)
        {
            // Normalization and include the full end day
            var toEnd = to.Date.AddDays(1).AddSeconds(-1);

            // Pull all visits in range (with navigations needed for analytics)
            var visits = await _context.Visits
                .Include(v => v.Visitor)
                .Include(v => v.CheckedInByNavigation)
                .Where(v => v.TimeIn.HasValue
                         && v.TimeIn.Value >= from.Date
                         && v.TimeIn.Value <= toEnd)
                .ToListAsync();

            // Apply optional block filter
            if (!string.IsNullOrWhiteSpace(filterBlock))
                visits = visits
                    .Where(v => v.HouseNumber != null
                             && v.HouseNumber.StartsWith(filterBlock, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            // Apply optional purpose filter
            if (!string.IsNullOrWhiteSpace(filterPurpose))
                visits = visits
                    .Where(v => v.PurposeOfVisit == filterPurpose)
                    .ToList();

            // Visits that have a recorded duration 
            var completedVisits = visits
                .Where(v => v.VisitDuration.HasValue && v.VisitDuration.Value > 0)
                .ToList();

            //Build each section 
            var avgByPurpose = BuildAverageDurationByPurpose(completedVisits);
            var topVisitors = BuildTopFrequentVisitors(visits);
            var longAlerts = BuildLongVisitAlerts(completedVisits, avgByPurpose);
            var residentTrends = await BuildResidentTrendsAsync(visits, from, to);
            var rangeStats = BuildDateRangeStats(visits, from, toEnd);
            var purposeDist = BuildPurposeDistribution(visits);
            var blockDist = BuildBlockDistribution(visits);
            var methodDist = BuildMethodDistribution(visits);
            var (dailyLabels, dailySeries) = BuildDailySeriesByBlock(visits, from.Date, to.Date);
            var hourly = BuildHourlyHistogram(visits);

            return new VisitAnalyticsViewModel
            {
                FilterFrom = from,
                FilterTo = to,
                FilterBlock = filterBlock,
                FilterPurpose = filterPurpose,

                AverageDurationByPurpose = avgByPurpose,
                TopFrequentVisitors = topVisitors,
                LongVisitAlerts = longAlerts,
                ResidentTrendData = residentTrends,
                DateRangeStats = rangeStats,

                PurposeDistribution = purposeDist,
                BlockDistribution = blockDist,
                CheckInMethodDistribution = methodDist,
                DailyLabels = dailyLabels,
                DailySeriesByBlock = dailySeries,
                HourlyHistogram = hourly,

                TotalVisitsInRange = visits.Count,
                TotalUniqueVisitorsInRange = visits.Select(v => v.VisitorId).Distinct().Count(),
                LongVisitAlertCount = longAlerts.Count,
                OverallAverageDuration = completedVisits.Any()
                                               ? completedVisits.Average(v => v.VisitDuration!.Value)
                                               : 0,
                BnbVisitsInRange = visits.Count(v =>
                    v.HouseNumber != null && v.HouseNumber.StartsWith("C")),
                ResidentialVisitsInRange = visits.Count(v =>
                    v.HouseNumber != null && !v.HouseNumber.StartsWith("C"))
            };
        }

        /// avarage duration is builty by grouping visits in purpose
    
        private static Dictionary<string, double> BuildAverageDurationByPurpose(List<Visit> completed)
        {
            return completed
                .Where(v => !string.IsNullOrWhiteSpace(v.PurposeOfVisit))
                .GroupBy(v => v.PurposeOfVisit!)
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => g.Key,
                    g => Math.Round(g.Average(v => v.VisitDuration!.Value), 1));
        }

        ///Top 10 most frequent visitors in the range.
        private static List<VisitorFrequencyDTO> BuildTopFrequentVisitors(List<Visit> visits)
        {
            return visits
                .Where(v => v.Visitor != null)
                .GroupBy(v => v.VisitorId)
                .Select(g =>
                {
                    var visitor = g.First().Visitor!;
                    var completed = g.Where(v => v.VisitDuration.HasValue && v.VisitDuration > 0).ToList();
                    var units = g.Where(v => v.HouseNumber != null)
                                     .GroupBy(v => v.HouseNumber!)
                                     .OrderByDescending(u => u.Count())
                                     .FirstOrDefault()?.Key;
                    return new VisitorFrequencyDTO
                    {
                        VisitorId = visitor.VisitorId,
                        VisitorName = visitor.FullName,
                        ContactNumber = visitor.ContactNumber,
                        PhotoUrl = visitor.PhotoUrl,
                        VisitCount = g.Count(),
                        LastVisit = g.Max(v => v.TimeIn),
                        MostVisitedUnit = units,
                        AverageDurationMinutes = completed.Any()
                            ? Math.Round(completed.Average(v => v.VisitDuration!.Value), 1)
                            : 0
                    };
                })
                .OrderByDescending(x => x.VisitCount)
                .Take(10)
                .ToList();
        }

        /// Visits flagged as unusually long relative to their purpose average.
        private static List<LongVisitDTO> BuildLongVisitAlerts(
            List<Visit> completed,
            Dictionary<string, double> avgByPurpose)
        {
            var alerts = new List<LongVisitDTO>();

            foreach (var v in completed)
            {
                if (v.Visitor == null) continue;

                var purpose = v.PurposeOfVisit ?? "Unknown";
                var avg = avgByPurpose.TryGetValue(purpose, out var a) ? a : 0;
                if (avg <= 0) continue;

                var multiplier = v.VisitDuration!.Value / avg;
                if (multiplier < LongVisitMultiplier) continue;

                alerts.Add(new LongVisitDTO
                {
                    VisitId = v.VisitId,
                    VisitorName = v.Visitor.FullName,
                    HouseNumber = v.HouseNumber,
                    Purpose = purpose,
                    CheckInTime = v.TimeIn!.Value,
                    CheckOutTime = v.TimeOut,
                    DurationMinutes = v.VisitDuration.Value,
                    AverageDurationForPurpose = avg,
                    Multiplier = Math.Round(multiplier, 1),
                    Flag = $"Unusual — {Math.Round(multiplier, 1)}× average for {purpose}"
                });
            }

            return alerts.OrderByDescending(a => a.Multiplier).Take(20).ToList();
        }

        // Per-resident aggregated stats and 7-day daily trend
        private async Task<Dictionary<int, ResidentTrendDTO>> BuildResidentTrendsAsync(
            List<Visit> visits,
            DateTime from,
            DateTime to)
        {
            // Build last 7 days for weekly trend
            var last7 = Enumerable.Range(0, 7)
                .Select(i => DateOnly.FromDateTime(DateTime.Today.AddDays(-6 + i)))
                .ToList();

            var residents = await _context.Residents
                .Include(r => r.User)
                .Include(r => r.Unit)
                .ToListAsync();

            var result = new Dictionary<int, ResidentTrendDTO>();

            foreach (var resident in residents)
            {
                // Visits to this resident's house number
                var resVisits = visits
                    .Where(v => v.HouseNumber == resident.HouseNumber)
                    .ToList();

                if (!resVisits.Any()) continue;

                var completed = resVisits
                    .Where(v => v.VisitDuration.HasValue && v.VisitDuration > 0)
                    .ToList();

                // 7-day trend
                var weeklyTrend = last7.Select(d => new DailyCountPoint
                {
                    Date = d,
                    Count = resVisits.Count(v =>
                        v.TimeIn.HasValue &&
                        DateOnly.FromDateTime(v.TimeIn.Value) == d)
                }).ToList();

                // Top purpose
                var topPurpose = resVisits
                    .Where(v => !string.IsNullOrWhiteSpace(v.PurposeOfVisit))
                    .GroupBy(v => v.PurposeOfVisit!)
                    .OrderByDescending(g => g.Count())
                    .FirstOrDefault()?.Key;

                result[resident.ResidentId] = new ResidentTrendDTO
                {
                    ResidentId = resident.ResidentId,
                    ResidentName = $"{resident.User?.Firstname} {resident.User?.Secondname}".Trim(),
                    HouseNumber = resident.HouseNumber,
                    UnitType = resident.Unit?.UnitType ?? "Residential",
                    TotalVisits = resVisits.Count,
                    AverageDurationMinutes = completed.Any()
                        ? Math.Round(completed.Average(v => v.VisitDuration!.Value), 1)
                        : 0,
                    UniqueVisitors = resVisits.Select(v => v.VisitorId).Distinct().Count(),
                    WeeklyTrend = weeklyTrend,
                    TopPurpose = topPurpose
                };
            }

            return result;
        }

        // Aggregate statistics for the whole date range.
        private static DateRangeStatsDTO BuildDateRangeStats(
            List<Visit> visits, DateTime from, DateTime toEnd)
        {
            var completed = visits
                .Where(v => v.VisitDuration.HasValue && v.VisitDuration > 0)
                .ToList();

            // Peak hour: group by hour of day, pick the busiest
            var hourGroups = visits
                .Where(v => v.TimeIn.HasValue)
                .GroupBy(v => v.TimeIn!.Value.Hour)
                .OrderByDescending(g => g.Count())
                .ToList();

            var peakHourIndex = hourGroups.FirstOrDefault()?.Key ?? 0;
            var peakHourLabel = $"{peakHourIndex:D2}:00 – {peakHourIndex + 1:D2}:00";

            // Most-visited unit
            var topUnit = visits
                .Where(v => !string.IsNullOrWhiteSpace(v.HouseNumber))
                .GroupBy(v => v.HouseNumber!)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            // Most common purpose
            var topPurpose = visits
                .Where(v => !string.IsNullOrWhiteSpace(v.PurposeOfVisit))
                .GroupBy(v => v.PurposeOfVisit!)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            // Daily check-ins
            var rangeStart = from.Date;
            var rangeEnd = toEnd.Date;
            var dayCount = (int)(rangeEnd - rangeStart).TotalDays + 1;

            var dailyCheckIns = Enumerable.Range(0, Math.Min(dayCount, 90))
                .Select(i =>
                {
                    var d = DateOnly.FromDateTime(rangeStart.AddDays(i));
                    return new DailyCountPoint
                    {
                        Date = d,
                        Count = visits.Count(v =>
                            v.TimeIn.HasValue &&
                            DateOnly.FromDateTime(v.TimeIn.Value) == d)
                    };
                })
                .ToList();

            // Hourly histogram
            var hourly = Enumerable.Range(0, 24)
                .Select(h => visits.Count(v => v.TimeIn.HasValue && v.TimeIn.Value.Hour == h))
                .ToList();

            return new DateRangeStatsDTO
            {
                StartDate = from,
                EndDate = toEnd,
                TotalVisits = visits.Count,
                TotalUniqueVisitors = visits.Select(v => v.VisitorId).Distinct().Count(),
                TotalCheckOuts = visits.Count(v => v.VisitStatus == "CheckedOut"),
                AverageDurationMinutes = completed.Any()
                    ? Math.Round(completed.Average(v => v.VisitDuration!.Value), 1)
                    : 0,
                PeakHour = peakHourLabel,
                PeakHourIndex = peakHourIndex,
                MostVisitedUnit = topUnit?.Key ?? "—",
                MostVisitedUnitCount = topUnit?.Count() ?? 0,
                MostCommonPurpose = topPurpose?.Key ?? "—",
                DailyCheckIns = dailyCheckIns,
                HourlyHistogram = hourly
            };
        }

        // Slices for purpose doughnut chart.
        private static List<ChartSlice> BuildPurposeDistribution(List<Visit> visits)
        {
            var groups = visits
                .Where(v => !string.IsNullOrWhiteSpace(v.PurposeOfVisit))
                .GroupBy(v => v.PurposeOfVisit!)
                .OrderByDescending(g => g.Count())
                .ToList();

            return groups.Select((g, i) => new ChartSlice
            {
                Label = g.Key,
                Value = g.Count(),
                Color = ChartColors[i % ChartColors.Length]
            }).ToList();
        }

        //Slices for block doughnut chart
        private static List<ChartSlice> BuildBlockDistribution(List<Visit> visits)
        {
            var blocks = new[] { "A", "B", "C" };
            var colors = new[] { "#3b82f6", "#10b981", "#8b5cf6" };

            return blocks.Select((b, i) => new ChartSlice
            {
                Label = b == "C" ? "Block C (BnB)" : $"Block {b}",
                Value = visits.Count(v => v.HouseNumber != null && v.HouseNumber.StartsWith(b)),
                Color = colors[i]
            }).Where(s => s.Value > 0).ToList();
        }

        //Slices for check-in method bar
        private static List<ChartSlice> BuildMethodDistribution(List<Visit> visits)
        {
            return new List<ChartSlice>
            {
                new() { Label = "Manual",    Value = visits.Count(v => v.CheckInMethod == "Manual" || v.CheckInMethod == null), Color = "#3b82f6" },
                new() { Label = "QR Code",   Value = visits.Count(v => v.CheckInMethod == "QR"),    Color = "#10b981" }
            };
        }

        
        //multi-series daily chart: one series per block (A, B, C)
        private static (List<string> Labels, List<ChartSeries> Series) BuildDailySeriesByBlock(
            List<Visit> visits, DateTime rangeStart, DateTime rangeEnd)
        {
            var days = Enumerable.Range(0, Math.Min((int)(rangeEnd - rangeStart).TotalDays + 1, 90))
                .Select(i => rangeStart.AddDays(i).Date)
                .ToList();

            var labels = days.Select(d => d.ToString("dd MMM")).ToList();
            var blocks = new[] { ("A", "#3b82f6"), ("B", "#10b981"), ("C", "#8b5cf6") };

            var series = blocks.Select(bt => new ChartSeries
            {
                Label = bt.Item1 == "C" ? "Block C (BnB)" : $"Block {bt.Item1}",
                Color = bt.Item2,
                Values = days.Select(d =>
                    (double)visits.Count(v =>
                        v.TimeIn.HasValue &&
                        v.TimeIn.Value.Date == d &&
                        v.HouseNumber != null &&
                        v.HouseNumber.StartsWith(bt.Item1))).ToList()
            }).ToList();

            return (labels, series);
        }

        //24-bucket hourly histogram
        private static List<int> BuildHourlyHistogram(List<Visit> visits)
        {
            return Enumerable.Range(0, 24)
                .Select(h => visits.Count(v => v.TimeIn.HasValue && v.TimeIn.Value.Hour == h))
                .ToList();
        }
    }
}
