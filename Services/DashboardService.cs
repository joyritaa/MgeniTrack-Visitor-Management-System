using System;
using MgeniTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace MgeniTrack.Services
{
    public class DashboardService
    {
        private readonly MgenitrackContext _context;

        public DashboardService(MgenitrackContext context)
        {
            _context = context;
        }

        public async Task<object> GetStatsAsync()
        {
            var occupied = await _context.Units.CountAsync(u => u.IsOccupied);
            var total = await _context.Units.CountAsync();

            var currentlyInside = await _context.Visits
                .CountAsync(v => v.VisitStatus == "Inside");

            var todayStart = DateTime.Today;
            var tomorrowStart = todayStart.AddDays(1);

            var todayVisits = await _context.Visits
                .CountAsync(v => v.TimeIn >= todayStart && v.TimeIn < tomorrowStart);

            var checkedOut = await _context.Visits
                .CountAsync(v => v.TimeOut >= todayStart && v.TimeOut < tomorrowStart);

            return new
            {
                occupied,
                total,
                currentlyInside,
                todayVisits,
                checkedOut
            };
        }
    }
}