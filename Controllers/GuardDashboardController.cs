using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MgeniTrack.Models;
using MgeniTrack.ViewModels;
using MgeniTrack.Services;
using MgeniTrack.Helpers;
using System.Security.Claims;

namespace MgeniTrack.Controllers
{
    [Authorize(Roles = "Guard")]
    public class GuardDashboardController : Controller
    {
        private readonly MgenitrackContext _context;
        private readonly ActivityLogService _activityLog;

        public GuardDashboardController(MgenitrackContext context, ActivityLogService activityLog)
        {
            _context = context;
            _activityLog = activityLog;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;

            var email = User.FindFirstValue(ClaimTypes.Name);
            var guardUser = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Email == email);

            var shift = guardUser?.UserRoles
                .FirstOrDefault(ur => ur.UserStatus == "Active")?.Shift ?? "—";

            ViewBag.GuardName = $"{guardUser?.Firstname} {guardUser?.Secondname}";
            ViewBag.Shift = shift;

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

        //  Active Visits for search 
        public async Task<IActionResult> ActiveVisits(string? search)
        {
            ViewBag.Search = search;
            IQueryable<Visit> query = _context.Visits
                .Include(v => v.Visitor)
                .Include(v => v.CheckedInByNavigation)
                .Where(v => v.VisitStatus == "CheckedIn")
                .OrderByDescending(v => v.TimeIn);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(v =>
                    (v.Visitor != null && v.Visitor.FullName.ToLower().Contains(s)) ||
                    (v.HouseNumber != null && v.HouseNumber.ToLower().Contains(s)) ||
                    (v.Visitor != null && v.Visitor.ContactNumber != null && v.Visitor.ContactNumber.Contains(s)));
            }
            return View(await query.ToListAsync());
        }

        //All Visits viewbag
        public async Task<IActionResult> AllVisits(string? search, string? status, string? block)
        {
            ViewBag.Search = search;
            ViewBag.Status = status;
            ViewBag.Block = block;

            IQueryable<Visit> query = _context.Visits
                .Include(v => v.Visitor)
                .Include(v => v.CheckedInByNavigation)
                .Include(v => v.CheckedOutByNavigation)
                .OrderByDescending(v => v.TimeIn);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(v =>
                    (v.Visitor != null && v.Visitor.FullName.ToLower().Contains(s)) ||
                    (v.HouseNumber != null && v.HouseNumber.ToLower().Contains(s)) ||
                    (v.Visitor != null && v.Visitor.IdNumber != null && v.Visitor.IdNumber.Contains(s)));
            }
            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(v => v.VisitStatus == status);
            if (!string.IsNullOrWhiteSpace(block) && block != "All")
                query = query.Where(v => v.HouseNumber != null && v.HouseNumber.StartsWith(block));

            return View(await query.Take(100).ToListAsync());
        }

        // Walk-In GET 
        public async Task<IActionResult> WalkIn()
        {
            // Only show OCCUPIED units have a resident
            var occupiedUnits = await _context.Units
                .Where(u => u.IsOccupied)
                .OrderBy(u => u.UnitNumber)
                .ToListAsync();

            ViewBag.OccupiedUnits = occupiedUnits;
            return View(new WalkInViewModel());
        }


        // Walk-In POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WalkIn(WalkInViewModel model, IFormFile? visitorPhoto)
        {
            // Validate the unit is occupied
            var unit = await _context.Units
                .Include(u => u.Resident).ThenInclude(r => r != null ? r.User : null)
                .FirstOrDefaultAsync(u => u.UnitNumber == model.HouseNumber);

            if (unit == null)
            {
                ModelState.AddModelError("HouseNumber", "Unit not found.");
            }
            else if (!unit.IsOccupied)
            {
                ModelState.AddModelError("HouseNumber",
                    $"Unit {model.HouseNumber} is currently vacant. Visitors cannot be checked in to an unoccupied unit.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.OccupiedUnits = await _context.Units
                    .Where(u => u.IsOccupied).OrderBy(u => u.UnitNumber).ToListAsync();
                return View(model);
            }

            var guardEmail = User.FindFirstValue(ClaimTypes.Name);
            var guardUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == guardEmail);
            if (guardUser == null) return Unauthorized();

            // Auto-detect BnB purpose if Block C unit
            var purpose = model.PurposeOfVisit;
            if (unit?.UnitType == "BnB" && purpose != "BnB Stay")
                purpose = "BnB Stay";

            // Handle photo upload
            string? photoPath = null;
            if (visitorPhoto != null && visitorPhoto.Length > 0)
            {
                var uploadDir = Path.Combine("wwwroot", "uploads", "visitors");
                Directory.CreateDirectory(uploadDir);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(visitorPhoto.FileName)}";
                var filePath = Path.Combine(uploadDir, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await visitorPhoto.CopyToAsync(stream);
                photoPath = $"/uploads/visitors/{fileName}";
            }



            //to find or create a visitor
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
                    PhotoUrl = photoPath,
                    FirstVisitDate = DateTime.Now,
                    TotalVisits = 1,
                    CreatedAt = DateTime.Now,
                    InvitedViaInvitationId = null
                };
                _context.Visitors.Add(visitor);
                
            }
            else
            {
                visitor.TotalVisits = (visitor.TotalVisits ?? 0) + 1;
                visitor.UpdatedAt = DateTime.Now;
                if (photoPath != null) visitor.PhotoUrl = photoPath; // update photo if new one uploaded
               
            }
            await _context.SaveChangesAsync();

            var visit = new Visit
            {
                VisitorId = visitor.VisitorId,
                CheckedInBy = guardUser.UserId,
                HouseNumber = model.HouseNumber,
                PurposeOfVisit = purpose,
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



            //notify the resident
            if (unit?.Resident != null)
            {
                _context.Notifications.Add(new Notification
                {
                    ResidentId = unit.Resident.ResidentId,
                    VisitId = visit.VisitId,
                    Title = "Visitor Arrived",
                    Message = $"{model.VisitorName} has arrived at your unit {model.HouseNumber}.",
                    NotificationType = "VisitorArrived",
                    IsRead = false,
                    CreatedAt = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();

            //log check in activity

            await _activityLog.LogAsync("CheckIn",
                $"Walk-in: {model.VisitorName} → {model.HouseNumber} ({purpose}). Guard: {guardUser.Firstname}",
                "Visit", visit.VisitId);

            TempData["Success"] = $"{model.VisitorName} checked in to {model.HouseNumber}.";
            return RedirectToAction(nameof(Index));
        }

        //check-Out GET
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

        //check-Out POST 
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
                ? (int)(DateTime.Now - visit.TimeIn.Value).TotalMinutes : 0;

            await _context.SaveChangesAsync();

            await _activityLog.LogAsync("CheckOut",
                $"{visit.Visitor?.FullName} checked out from {visit.HouseNumber}. Duration: {visit.VisitDuration} min",
                "Visit", visit.VisitId);

            TempData["Success"] = $"{visit.Visitor?.FullName ?? "Visitor"} checked out. Duration: {visit.VisitDuration} min.";
            return RedirectToAction(nameof(Index));
        }
    }
}