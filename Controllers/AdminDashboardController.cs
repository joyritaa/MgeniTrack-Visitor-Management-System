using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MgeniTrack.Models;

namespace MgeniTrack.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminDashboardController : Controller
    {
        private readonly MgenitrackContext _context;

        public AdminDashboardController(MgenitrackContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;

            ViewBag.TotalUsers = await _context.Users.CountAsync(u => u.UserStatus == "Active");
            ViewBag.TotalVisitors = await _context.Visitors.CountAsync();
            ViewBag.TotalVisitsToday = await _context.Visits.CountAsync(v => v.TimeIn.HasValue && v.TimeIn.Value.Date == today);
            ViewBag.ActiveVisits = await _context.Visits.CountAsync(v => v.VisitStatus == "CheckedIn");
            ViewBag.TotalResidents = await _context.Residents.CountAsync();
            ViewBag.TotalGuards = await _context.UserRoles
                                            .Include(ur => ur.Role)
                                            .CountAsync(ur => ur.Role.RoleName == "Guard" && ur.UserStatus == "Active");

            // Recent visits (last 10)
            ViewBag.RecentVisits = await _context.Visits
                .Include(v => v.Visitor)
                .Include(v => v.CheckedInByNavigation)
                .OrderByDescending(v => v.TimeIn)
                .Take(10)
                .ToListAsync();

            // Recent users (last 5)
            ViewBag.RecentUsers = await _context.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .OrderByDescending(u => u.CreatedAt)
                .Take(5)
                .ToListAsync();

            // Activity logs — recent system actions
            ViewBag.ActivityLogs = await _context.ActivityLogs
                .Include(a => a.User)
                .OrderByDescending(a => a.TimeStamp)
                .Take(15)
                .ToListAsync();

            // Recent reports
            ViewBag.RecentReports = await _context.Reports
                .Include(r => r.GeneratedByNavigation)
                .OrderByDescending(r => r.GeneratedAt)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }
}