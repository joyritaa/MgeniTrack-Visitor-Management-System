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

            // BnB stays vs house visits today
            ViewBag.BnbVisitsToday = await _context.Visits.CountAsync(v => v.TimeIn.HasValue && v.TimeIn.Value.Date == today && v.HouseNumber != null && v.HouseNumber.StartsWith("C"));
            ViewBag.HouseVisitsToday = await _context.Visits.CountAsync(v => v.TimeIn.HasValue && v.TimeIn.Value.Date == today && v.HouseNumber != null && !v.HouseNumber.StartsWith("C"));

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
        //Residents Index 
        public async Task<IActionResult> Residents(string? search, string? block)
        {
            ViewBag.Search = search;
            ViewBag.Block = block;

            IQueryable<Resident> query = _context.Residents
                .Include(r => r.User).Include(r => r.Unit)
                .Include(r => r.VisitorInvitations)
                .OrderBy(r => r.HouseNumber);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.ToLower();
                query = query.Where(r =>
                    r.User.Firstname.ToLower().Contains(s) ||
                    r.User.Secondname != null && r.User.Secondname.ToLower().Contains(s) ||
                    r.HouseNumber.ToLower().Contains(s) ||
                    r.User.Email.ToLower().Contains(s));
            }
            if (!string.IsNullOrWhiteSpace(block) && block != "All")
                query = query.Where(r => r.HouseNumber.StartsWith(block));

            return View(await query.ToListAsync());
        }

        // Resident Details 
        public async Task<IActionResult> ResidentDetails(int id)
        {
            var resident = await _context.Residents
                .Include(r => r.User)
                .Include(r => r.Unit)
                .Include(r => r.VisitorInvitations)
                .Include(r => r.Notifications)
                .FirstOrDefaultAsync(r => r.ResidentId == id);
            if (resident == null) return NotFound();
            return View(resident);
        }

        // BnB Stays
        public async Task<IActionResult> BnbStays(string? search)
        {
            ViewBag.Search = search;
            IQueryable<Visit> query = _context.Visits
                .Include(v => v.Visitor)
                .Include(v => v.CheckedInByNavigation)
                .Where(v => v.HouseNumber != null && v.HouseNumber.StartsWith("C"))
                .OrderByDescending(v => v.TimeIn);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.ToLower();
                query = query.Where(v =>
                    (v.Visitor != null && v.Visitor.FullName.ToLower().Contains(s)) ||
                    (v.HouseNumber != null && v.HouseNumber.ToLower().Contains(s)));
            }
            ViewBag.TotalBnb = await _context.Visits.CountAsync(v => v.HouseNumber != null && v.HouseNumber.StartsWith("C"));
            ViewBag.ActiveBnb = await _context.Visits.CountAsync(v => v.HouseNumber != null && v.HouseNumber.StartsWith("C") && v.VisitStatus == "CheckedIn");
            return View(await query.Take(100).ToListAsync());
        }

        //House Visits
        public async Task<IActionResult> HouseVisits(string? search, string? block)
        {
            ViewBag.Search = search;
            ViewBag.Block = block;
            IQueryable<Visit> query = _context.Visits
                .Include(v => v.Visitor)
                .Include(v => v.CheckedInByNavigation)
                .Where(v => v.HouseNumber != null && !v.HouseNumber.StartsWith("C"))
                .OrderByDescending(v => v.TimeIn);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.ToLower();
                query = query.Where(v =>
                    (v.Visitor != null && v.Visitor.FullName.ToLower().Contains(s)) ||
                    (v.HouseNumber != null && v.HouseNumber.ToLower().Contains(s)));
            }
            if (!string.IsNullOrWhiteSpace(block) && block != "All")
                query = query.Where(v => v.HouseNumber != null && v.HouseNumber.StartsWith(block));

            return View(await query.Take(100).ToListAsync());
        }
    }
}