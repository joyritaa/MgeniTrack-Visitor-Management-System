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
                ViewBag.ResidentId = 0;
                ViewBag.HouseNumber = "N/A";
                ViewBag.PendingInvitations = new List<VisitorInvitation>();
                ViewBag.RecentVisits = new List<VisitorInvitation>();
                ViewBag.TotalInvitations = 0;
                ViewBag.ActiveVisitors = 0;
                return View();
            }

            var residentId = user.Resident.ResidentId;
            ViewBag.HouseNumber = user.Resident.HouseNumber;

            var invitations = await _context.VisitorInvitations
                .Where(i => i.ResidentId == residentId)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();

            ViewBag.PendingInvitations = invitations.Where(i => i.VisitStatus == "Pending").Take(5).ToList();
            ViewBag.RecentVisits = invitations.Take(8).ToList();
            ViewBag.TotalInvitations = invitations.Count;
            ViewBag.ActiveVisitors = invitations.Count(i => i.VisitStatus == "Arrived");

            return View();
        }
    }
}