using MgeniTrack.Helpers;
using MgeniTrack.Models;
using MgeniTrack.Services;
using MgeniTrack.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MgeniTrack.Controllers
{
    [Authorize(Roles = "Guard")]
    public class GuardDashboardController : Controller
    {
        private readonly MgenitrackContext _context;
        private readonly ActivityLogService _activityLog;
        private readonly IHubContext<MgeniTrack.Hubs.DashboardHub> _hub;
        private readonly DashboardService _dashboardService;

        public GuardDashboardController(MgenitrackContext context, ActivityLogService activityLog, IHubContext<MgeniTrack.Hubs.DashboardHub> hub,
    DashboardService dashboardService)
        {
            _context = context;
            _activityLog = activityLog;
            _hub = hub;
            _dashboardService = dashboardService;
        }

        //Dashboard 
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

            if (TempData["Success"] != null) ViewBag.SuccessMsg = TempData["Success"];
            if (TempData["Error"] != null) ViewBag.ErrorMsg = TempData["Error"];

            return View();
        }

        // searchable active visits
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
                    (v.Visitor != null && v.Visitor.ContactNumber != null
                        && v.Visitor.ContactNumber.Contains(s)));
            }
            return View(await query.ToListAsync());
        }

        // all visits
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
                    (v.Visitor != null && v.Visitor.IdNumber != null
                        && v.Visitor.IdNumber.Contains(s)));
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
            var occupiedUnits = await _context.Units
                .Where(u => u.IsOccupied)
                .OrderBy(u => u.UnitNumber)
                .ToListAsync();

            ViewBag.OccupiedUnits = occupiedUnits;
            return View(new WalkInViewModel());
        }

        // walk-in POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WalkIn(WalkInViewModel model, IFormFile? visitorPhoto)
        {
            // Validate unit is occupied
            var unit = await _context.Units
                .Include(u => u.Resident)
                .FirstOrDefaultAsync(u => u.UnitNumber == model.HouseNumber);

            if (unit == null)
                ModelState.AddModelError("HouseNumber", "Unit not found.");
            else if (!unit.IsOccupied)
                ModelState.AddModelError("HouseNumber",
                    $"Unit {model.HouseNumber} is currently vacant. No visitor can be checked in here.");

            if (!ModelState.IsValid)
            {
                ViewBag.OccupiedUnits = await _context.Units
                    .Where(u => u.IsOccupied).OrderBy(u => u.UnitNumber).ToListAsync();
                return View(model);
            }

            var guardEmail = User.FindFirstValue(ClaimTypes.Name);
            var guardUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == guardEmail);
            if (guardUser == null) return Unauthorized();

            // Auto-set BnB purpose for Block C
            var purpose = model.PurposeOfVisit;
            if (unit!.UnitType == "BnB") purpose = "BnB Stay";

            // Handle photo upload
            string? photoPath = null;
            if (visitorPhoto != null && visitorPhoto.Length > 0)
            {
                var uploadDir = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot", "uploads", "visitors");
                Directory.CreateDirectory(uploadDir);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(visitorPhoto.FileName)}";
                var filePath = Path.Combine(uploadDir, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await visitorPhoto.CopyToAsync(stream);
                photoPath = $"/uploads/visitors/{fileName}";
            }

            // Find or create visitor
            var visitor = await _context.Visitors.FirstOrDefaultAsync(v =>
                v.FullName == model.VisitorName &&
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
                if (photoPath != null) visitor.PhotoUrl = photoPath;
            }

            //Save visitor first to get VisitorId
            await _context.SaveChangesAsync();

            //Create and save the visit
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
                CreatedAt = DateTime.Now
            };
            _context.Visits.Add(visit);

            // SaveChanges to get the VisitId before creating notification
            await _context.SaveChangesAsync();

            //dynamic change after saving to db
            var stats = await _dashboardService.GetStatsAsync();

            await _hub.Clients.All.SendAsync("ReceiveDashboardUpdate", stats);


            // create notification — visit.VisitId is valid
            if (unit.Resident != null)
            {
                _context.Notifications.Add(new Notification
                {
                    ResidentId = unit.Resident.ResidentId,
                    VisitId = visit.VisitId,   // valid now
                    Title = "Visitor Arrived",
                    Message = $"{model.VisitorName} has arrived at your unit {model.HouseNumber}.",
                    NotificationType = "VisitorArrived",
                    IsRead = false,
                    CreatedAt = DateTime.Now
                });
                await _context.SaveChangesAsync();
                
            }

            // Log
            await _activityLog.LogAsync("CheckIn",
                $"Walk-in: {model.VisitorName} → {model.HouseNumber} ({purpose}). Guard: {guardUser.Firstname}",
                "Visit", visit.VisitId);

            TempData["Success"] = $"{model.VisitorName} checked in to {model.HouseNumber} successfully.";
            return RedirectToAction(nameof(Index));
        }

        // check-Out GET 
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

        // Check-Out POST
        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOutConfirmed(int visitId)
        {
            var visit = await _context.Visits
                .Include(v => v.Visitor)
                .FirstOrDefaultAsync(v => v.VisitId == visitId);
            if (visit == null) return NotFound();

            var guardEmail = User.FindFirstValue(ClaimTypes.Name);
            var guardUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == guardEmail);

            visit.TimeOut = DateTime.Now;
            visit.VisitStatus = "CheckedOut";
            visit.CheckedOutBy = guardUser?.UserId;
            visit.VisitDuration = visit.TimeIn.HasValue
                ? (int)(DateTime.Now - visit.TimeIn.Value).TotalMinutes : 0;

            await _context.SaveChangesAsync();

            // Notify resident of checkout
            var unit = await _context.Units
                .Include(u => u.Resident)
                .FirstOrDefaultAsync(u => u.UnitNumber == visit.HouseNumber);

            if (unit?.Resident != null)
            {
                _context.Notifications.Add(new Notification
                {
                    ResidentId = unit.Resident.ResidentId,
                    VisitId = visit.VisitId,
                    Title = "Visitor Checked Out",
                    Message = $"{visit.Visitor?.FullName} has left your unit {visit.HouseNumber}. Duration: {visit.VisitDuration} min.",
                    NotificationType = "CheckedOut",
                    IsRead = false,
                    CreatedAt = DateTime.Now
                });
                await _context.SaveChangesAsync();
            }

            await _activityLog.LogAsync("CheckOut",
                $"{visit.Visitor?.FullName} checked out of {visit.HouseNumber}. Duration: {visit.VisitDuration} min",
                "Visit", visit.VisitId);

            TempData["Success"] = $"{visit.Visitor?.FullName ?? "Visitor"} checked out. Duration: {visit.VisitDuration} min.";
            return RedirectToAction(nameof(Index));
        }
    }
}