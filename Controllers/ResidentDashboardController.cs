using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MgeniTrack.Models;
using System.Security.Claims;

namespace MgeniTrack.Controllers
{
    [Authorize(Roles = "Resident")]
    public class ResidentDashboardController : Controller
    {
        private readonly MgenitrackContext _context;

        public ResidentDashboardController(MgenitrackContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get logged-in user's resident record
            var email = User.FindFirstValue(ClaimTypes.Name);
            var user = await _context.Users
                .Include(u => u.Resident)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user?.Resident == null)
            {
                ViewBag.HouseNumber = "Not Assigned";
                ViewBag.UnitType = "—";
                ViewBag.TotalInvitations = 0;
                ViewBag.ActiveVisitors = 0;
                ViewBag.UnreadNotifications = 0;
                ViewBag.RecentVisits = new List<VisitorInvitation>();
                ViewBag.Notifications = new List<Notification>();
                return View();
            }

            var residentId = user.Resident.ResidentId;
            ViewBag.HouseNumber = user.Resident.HouseNumber;

            ViewBag.UnitType = user.Resident.Unit?.UnitType ?? "Residential";
            ViewBag.FloorInfo = user.Resident.Unit != null
                ? $"Floor {user.Resident.Unit.FloorNumber}, Unit {user.Resident.Unit.UnitPosition}"
                : "";

            var invitations = await _context.VisitorInvitations
                .Where(i => i.ResidentId == residentId)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();

            var notifications = await _context.Notifications
              .Where(n => n.ResidentId == residentId)
              .OrderByDescending(n => n.CreatedAt)
              .Take(10)
              .ToListAsync();


            ViewBag.PendingInvitations = invitations.Where(i => i.VisitStatus == "Pending").Take(5).ToList();
            ViewBag.RecentVisits = invitations.Take(8).ToList();
            ViewBag.TotalInvitations = invitations.Count;
            ViewBag.ActiveVisitors = invitations.Count(i => i.VisitStatus == "Arrived");

            ViewBag.Notifications = notifications;
            ViewBag.UnreadNotifications = notifications.Count(n => n.IsRead == false);

            // Mark notifications as read
            foreach (var n in notifications.Where(n => n.IsRead == false))
                n.IsRead = true;
            await _context.SaveChangesAsync();

            return View();
        }

        // Mark notification read via AJAX
        [HttpPost]
        public async Task<IActionResult> MarkRead(int id)
        {
            var n = await _context.Notifications.FindAsync(id);
            if (n != null) { n.IsRead = true; await _context.SaveChangesAsync(); }
            return Ok();
        }
    }
}