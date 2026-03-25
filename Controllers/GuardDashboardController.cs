using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MgeniTrack.Models;
using MgeniTrack.ViewModels;
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

        // Dashboard
        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;

            var activeVisits = await _context.Visits
                .Include(v => v.Visitor)
                .Where(v => v.VisitStatus == "CheckedIn")
                .OrderByDescending(v => v.TimeIn)
                .ToListAsync();

            var todayVisits = await _context.Visits
                .Where(v => v.TimeIn.HasValue && v.TimeIn.Value.Date == today)
                .ToListAsync();

            ViewBag.ActiveVisits = activeVisits;
            ViewBag.TodayVisitCount = todayVisits.Count;
            ViewBag.CheckedOutToday = todayVisits.Count(v => v.VisitStatus == "CheckedOut");
            ViewBag.CurrentlyInside = activeVisits.Count;

            return View();
        }

        //Walk-in Check-In GET 
        public IActionResult WalkIn()
        {
            return View(new WalkInViewModel());
        }

        // Walk-in Check-In POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WalkIn(WalkInViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var guardEmail = User.FindFirstValue(ClaimTypes.Name);
            var guardUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == guardEmail);
            if (guardUser == null) return Unauthorized();

            // Find existing visitor or create new one
            var visitor = await _context.Visitors
                .FirstOrDefaultAsync(v => v.FullName == model.VisitorName &&
                                          v.ContactNumber == model.ContactNumber);

            if (visitor == null)
            {
                visitor = new Visitor
                {
                    FullName = model.VisitorName,
                    IdNumber = model.IdNumber,
                    ContactNumber = model.ContactNumber,
                    FirstVisitDate = DateTime.Now,
                    TotalVisits = 1,
                    CreatedAt = DateTime.Now,
                    InvitedViaInvitationId = 0  // walk-in: no invitation
                };
                _context.Visitors.Add(visitor);
                await _context.SaveChangesAsync();
            }
            else
            {
                visitor.TotalVisits = (visitor.TotalVisits ?? 0) + 1;
                visitor.UpdatedAt = DateTime.Now;
            }

            var visit = new Visit
            {
                VisitorId = visitor.VisitorId,
                CheckedInBy = guardUser.UserId,
                HouseNumber = model.HouseNumber,
                PurposeOfVisit = model.PurposeOfVisit,
                CarRegistration = model.CarRegistration,
                NumberOfOccupants = model.NumberOfOccupants,
                TimeIn = DateTime.Now,
                VisitStatus = "CheckedIn",
                CheckInMethod = "Manual",
                CreatedAt = DateTime.Now,
                Visitor = visitor,
                CheckedInByNavigation = guardUser
            };

            _context.Visits.Add(visit);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"{model.VisitorName} checked in manually to {model.HouseNumber}.";
            return RedirectToAction(nameof(Index));
        }

        //Check-Out GET 
        public async Task<IActionResult> CheckOut(int visitId)
        {
            var visit = await _context.Visits
                .Include(v => v.Visitor)
                .FirstOrDefaultAsync(v => v.VisitId == visitId);

            if (visit == null) return NotFound();

            if (visit.VisitStatus == "CheckedOut")
            {
                TempData["Error"] = "This visitor has already been checked out.";
                return RedirectToAction(nameof(Index));
            }

            return View(visit);
        }

        //Check-Out POST
        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOutConfirmed(int visitId)
        {
            var visit = await _context.Visits
                .Include(v => v.Visitor)
                .FirstOrDefaultAsync(v => v.VisitId == visitId);

            if (visit == null) return NotFound();

            var guardEmail = User.FindFirstValue(ClaimTypes.Name);
            var guardUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == guardEmail);

            visit.TimeOut = DateTime.Now;
            visit.VisitStatus = "CheckedOut";
            visit.CheckedOutBy = guardUser?.UserId;
            visit.VisitDuration = visit.TimeIn.HasValue
                ? (int)(DateTime.Now - visit.TimeIn.Value).TotalMinutes
                : 0;

            await _context.SaveChangesAsync();

            TempData["Success"] = $"{visit.Visitor?.FullName ?? "Visitor"} checked out. " +
                                  $"Duration: {visit.VisitDuration} min.";
            return RedirectToAction(nameof(Index));
        }
    }
}