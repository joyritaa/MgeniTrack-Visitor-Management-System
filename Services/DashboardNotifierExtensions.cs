using MgeniTrack.Models;
using MgeniTrack.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MgeniTrack
{
    public static class DashboardNotifierExtensions
    {
        // Provides a GetStatsAsync extension that fetches real stats from the database
        public static async Task<object> GetStatsAsync(this DashboardNotifier notifier, MgenitrackContext context)
        {
            var today = System.DateTime.Today;

            var totalInside = await context.Visits
                .Where(v => v.VisitStatus == "CheckedIn")
                .CountAsync();

            var todayVisits = await context.Visits
                .Where(v => v.TimeIn.HasValue && v.TimeIn.Value.Date == today)
                .CountAsync();

            var checkedOutToday = await context.Visits
                .Where(v => v.TimeIn.HasValue && v.TimeIn.Value.Date == today && v.VisitStatus == "CheckedOut")
                .CountAsync();

            var occupiedUnits = await context.Units
                .Where(u => u.IsOccupied)
                .CountAsync();

            var stats = new
            {
                TotalInside = totalInside,
                TodayVisits = todayVisits,
                CheckedOutToday = checkedOutToday,
                OccupiedUnits = occupiedUnits,
                Timestamp = System.DateTime.Now
            };

            return stats;
        }
    }
}
