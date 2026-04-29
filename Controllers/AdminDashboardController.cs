using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MgeniTrack.Models;
using MgeniTrack.Services;

namespace MgeniTrack.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminDashboardController : Controller
    {
        private readonly MgenitrackContext _context;
        private readonly ActivityLogService _activityLog;

        public AdminDashboardController(MgenitrackContext context,
            ActivityLogService activityLog)
        {
            _context = context;
            _activityLog = activityLog;
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

            ViewBag.OccupiedUnits = await _context.Units.CountAsync(u => u.IsOccupied);
            ViewBag.TotalUnits = await _context.Units.CountAsync();

            // Recent visits (last 8)
            ViewBag.RecentVisits = await _context.Visits
                .Include(v => v.Visitor)
                .Include(v => v.CheckedInByNavigation)
                .OrderByDescending(v => v.TimeIn)
                .Take(8)
                .ToListAsync();

            // Recent users (last 5)
            ViewBag.RecentUsers = await _context.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .OrderByDescending(u => u.CreatedAt)
                .Take(5)
                .ToListAsync();

            // Activity logs
            ViewBag.ActivityLogs = await _context.ActivityLogs
                .Include(a => a.User)
                .OrderByDescending(a => a.TimeStamp)
                .Take(20)
                .ToListAsync();

            // Recent reports
            ViewBag.RecentReports = await _context.Reports
                .Include(r => r.GeneratedByNavigation)
                .OrderByDescending(r => r.GeneratedAt)
                .Take(5)
                .ToListAsync();

            return View();
        }
        // styled user Details 
        public async Task<IActionResult> UserDetails(int id)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .Include(u => u.Resident).ThenInclude(r => r != null ? r.Unit : null)
                .Include(u => u.ActivityLogs)
                .FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null) return NotFound();
            return View(user);
        }

        // Deactivate user (soft delete)
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            user.UserStatus = "Inactive";
            await _context.SaveChangesAsync();
            await _activityLog.LogAsync("Deactivate", $"User {user.Firstname} {user.Secondname} deactivated", "User", id);
            TempData["Success"] = $"{user.Firstname} has been deactivated.";
            return RedirectToAction("Index", "Users");
        }

        // Activity Logs full page
        public async Task<IActionResult> ActivityLogs(string? search, string? actionType, int page = 1)
        {
            ViewBag.Search = search;
            ViewBag.ActionType = actionType;
            int pageSize = 30;

            IQueryable<ActivityLog> query = _context.ActivityLogs
                .Include(a => a.User)
                .OrderByDescending(a => a.TimeStamp);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.ToLower();
                query = query.Where(a =>
                    (a.User != null && a.User.Firstname.ToLower().Contains(s)) ||
                    (a.ActionDetails != null && a.ActionDetails.ToLower().Contains(s)) ||
                    (a.ActionType != null && a.ActionType.ToLower().Contains(s)));
            }
            if (!string.IsNullOrWhiteSpace(actionType))
                query = query.Where(a => a.ActionType == actionType);

            var total = await query.CountAsync();
            ViewBag.TotalPages = (int)Math.Ceiling(total / (double)pageSize);
            ViewBag.CurrentPage = page;

            return View(await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync());
        }
    }
}