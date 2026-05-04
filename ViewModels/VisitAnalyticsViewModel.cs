using System;
using System.Collections.Generic;

namespace MgeniTrack.ViewModels
{
    /// data transfer object

    public class VisitorFrequencyDTO
    {
        public int VisitorId { get; set; }
        public string VisitorName { get; set; } = null!;
        public string? ContactNumber { get; set; }
        public string? PhotoUrl { get; set; }
        public int VisitCount { get; set; }
        public DateTime? LastVisit { get; set; }
        public string? MostVisitedUnit { get; set; }
        public double AverageDurationMinutes { get; set; }
    }

    /// Represents a single visit that is flagged as unusually long.
    public class LongVisitDTO
    {
        public int VisitId { get; set; }
        public string VisitorName { get; set; } = null!;
        public string? HouseNumber { get; set; }
        public string? Purpose { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public int DurationMinutes { get; set; }
        public double AverageDurationForPurpose { get; set; }
        public string Flag { get; set; } = null!;
        public double Multiplier { get; set; }
    }

    /// One data-point in a weekly trend list: a date and a visit count.
    public class DailyCountPoint
    {
        public DateOnly Date { get; set; }
        public int Count { get; set; }
        public string DateLabel => Date.ToString("yyyy-MM-dd");
        public string ShortLabel => Date.ToString("ddd d");
    }


    /// Per-resident aggregated trend and visit statistics.
    public class ResidentTrendDTO
    {
        public int ResidentId { get; set; }
        public string ResidentName { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
        public string UnitType { get; set; } = "Residential";
        public int TotalVisits { get; set; }
        public double AverageDurationMinutes { get; set; }
        public int UniqueVisitors { get; set; }
   
        public List<DailyCountPoint> WeeklyTrend { get; set; } = new();
        public string? TopPurpose { get; set; }
    }

    /// Aggregate statistics for the selected date range.
    public class DateRangeStatsDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalVisits { get; set; }
        public int TotalUniqueVisitors { get; set; }
        public int TotalCheckOuts { get; set; }
        public double AverageDurationMinutes { get; set; }
        public string PeakHour { get; set; } = "—";
        public int PeakHourIndex { get; set; }
        public string MostVisitedUnit { get; set; } = "—";
        public int MostVisitedUnitCount { get; set; }
        public string MostCommonPurpose { get; set; } = "—";
        public List<DailyCountPoint> DailyCheckIns { get; set; } = new();
        public List<int> HourlyHistogram { get; set; } = Enumerable.Repeat(0, 24).ToList();
    }

    /// One slice in a pie/doughnut chart.

    public class ChartSlice
    {
        public string Label { get; set; } = null!;
        public double Value { get; set; }
        public string Color { get; set; } = "#3b82f6";
    }

    /// One data-series for a multi-line or multi-bar chart.
    public class ChartSeries
    {
        public string Label { get; set; } = null!;
        public string Color { get; set; } = "#3b82f6";
        public List<double> Values { get; set; } = new();
    }

    //the view model that includesall the dtos above and any additional properties needed for the view
    public class VisitAnalyticsViewModel
    {
        public DateTime FilterFrom { get; set; } = DateTime.Today.AddDays(-30);
        public DateTime FilterTo { get; set; } = DateTime.Today;
        public string? FilterBlock { get; set; }   
        public string? FilterPurpose { get; set; } 
       
        public Dictionary<string, double> AverageDurationByPurpose { get; set; } = new();   
        public List<VisitorFrequencyDTO> TopFrequentVisitors { get; set; } = new();
        public List<LongVisitDTO> LongVisitAlerts { get; set; } = new();
        public Dictionary<int, ResidentTrendDTO> ResidentTrendData { get; set; } = new();
        public DateRangeStatsDTO DateRangeStats { get; set; } = new();
        public List<ChartSlice> PurposeDistribution { get; set; } = new();
        public List<ChartSlice> BlockDistribution { get; set; } = new();
        public List<ChartSlice> CheckInMethodDistribution { get; set; } = new();
        public List<string> DailyLabels { get; set; } = new();
        public List<ChartSeries> DailySeriesByBlock { get; set; } = new();
        public List<int> HourlyHistogram { get; set; } = Enumerable.Repeat(0, 24).ToList();
        public int TotalVisitsInRange { get; set; }
        public int TotalUniqueVisitorsInRange { get; set; }
        public int LongVisitAlertCount { get; set; }
        public double OverallAverageDuration { get; set; }
        public int BnbVisitsInRange { get; set; }
        public int ResidentialVisitsInRange { get; set; }
    }
}
