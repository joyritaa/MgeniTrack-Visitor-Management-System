using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MgeniTrack.Models;
using System.Security.Claims;

namespace MgeniTrack.Controllers
{
    [Authorize(Roles = "Guard")]
    public class GuardDashboardController : Controller
    {
        private readonly MgenitrackContext _context;

        public GuardDashboardController(MgenitrackContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;

            // Active visits (checked in, not yet out)
            var activeVisits = await _context.Visits
                .Include(v => v.Visitor)
                .Where(v => v.VisitStatus == "CheckedIn")
                .OrderByDescending(v => v.TimeIn)
                .ToListAsync();

            // Today's completed visits
            var todayVisits = await _context.Visits
                .Include(v => v.Visitor)
                .Where(v => v.TimeIn.HasValue && v.TimeIn.Value.Date == today)
                .OrderByDescending(v => v.TimeIn)
                .Take(20)
                .ToListAsync();

            ViewBag.ActiveVisits = activeVisits;
            ViewBag.TodayVisitCount = todayVisits.Count;
            ViewBag.CheckedOutToday = todayVisits.Count(v => v.VisitStatus == "CheckedOut");
            ViewBag.CurrentlyInside = activeVisits.Count;

            return View();
        }
    }
}