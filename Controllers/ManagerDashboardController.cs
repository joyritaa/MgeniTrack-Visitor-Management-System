using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MgeniTrack.Models;

namespace MgeniTrack.Controllers
{
    [Authorize(Roles = "PropertyManager")]
    public class ManagerDashboardController : Controller
    {
        private readonly MgenitrackContext _context;

        public ManagerDashboardController(MgenitrackContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;

            ViewBag.TotalResidents = await _context.Residents.CountAsync();
            ViewBag.TotalInvitations = await _context.VisitorInvitations.CountAsync();
            ViewBag.PendingInvitations = await _context.VisitorInvitations.CountAsync(i => i.VisitStatus == "Pending");
            ViewBag.ArrivedToday = await _context.VisitorInvitations
                                            .CountAsync(i => i.VisitStatus == "Arrived" &&
                                                            i.CreatedAt.HasValue &&
                                                            i.CreatedAt.Value.Date == today);

            // Recent invitations
            ViewBag.RecentInvitations = await _context.VisitorInvitations
                .Include(i => i.Resident).ThenInclude(r => r.User)
                .OrderByDescending(i => i.CreatedAt)
                .Take(10)
                .ToListAsync();

            // Residents list
            ViewBag.Residents = await _context.Residents
                .Include(r => r.User)
                .Include(r => r.VisitorInvitations)
                .OrderBy(r => r.HouseNumber)
                .Take(10)
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