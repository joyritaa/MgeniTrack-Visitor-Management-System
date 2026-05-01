using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MgeniTrack.Models;
using MgeniTrack.ViewModels;
using MgeniTrack.Services;

namespace MgeniTrack.Controllers
{
    public class VisitorInvitationsController : Controller
    {
        private readonly MgenitrackContext _context;
        private readonly ActivityLogService _activityLog;

        public VisitorInvitationsController(MgenitrackContext context, ActivityLogService activityLog)
        {
            _context = context;
            _activityLog = activityLog;
        }

        // List invitations for residents, managers, and admins
        [Authorize(Roles = "Resident,PropertyManager,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            IQueryable<VisitorInvitation> query = _context.VisitorInvitations
                .Include(i => i.Resident).ThenInclude(r => r.User)
                .OrderByDescending(i => i.CreatedAt);

            if (User.IsInRole("Resident"))
            {
                var email = User.FindFirstValue(ClaimTypes.Name);
                var resident = await _context.Residents
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.User.Email == email);
                if (resident == null)
                {
                    TempData["Error"] = "No resident profile found for your account.";
                    return View(new List<VisitorInvitation>());
                }
                query = query.Where(i => i.ResidentId == resident.ResidentId);
            }
            return View(await query.ToListAsync());
        }

        // Resident: Create invitation GET 
        [Authorize(Roles = "Resident")]
        public IActionResult Create() => View(new InvitationCreateViewModel
        {
            ExpectedDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1))
        });

        //Resident: Create invitation POST
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Resident")]
        public async Task<IActionResult> Create(InvitationCreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var email = User.FindFirstValue(ClaimTypes.Name);
            var resident = await _context.Residents
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.User.Email == email);

            if (resident == null)
            {
                ModelState.AddModelError("", "Resident profile not found. Contact your administrator.");
                return View(model);
            }

            var token = Guid.NewGuid().ToString("N").ToUpper()[..10];

            var invitation = new VisitorInvitation
            {
                InvitationId = 0,
                ResidentId = resident.ResidentId,
                VisitorName = model.VisitorName,
                VisitorPhone = model.VisitorPhone,
                VisitorEmail = model.VisitorEmail,
                PurposeOfVisit = model.PurposeOfVisit,
                ExpectedDate = model.ExpectedDate,
                InvitationToken = token,
                QrCodePath = $"/qrcodes/{token}.png",
                VisitStatus = "Pending",
                CreatedAt = DateTime.Now
            };

            _context.VisitorInvitations.Add(invitation);
            await _context.SaveChangesAsync();

            await _activityLog.LogAsync("Create",
                $"Invitation created for {model.VisitorName} (token: {token})",
                "VisitorInvitation", invitation.InvitationId);

            TempData["Success"] = $"Invitation created for {model.VisitorName}. Token: {token}";
            return RedirectToAction(nameof(Index));
        }

        //  Resident: Invitation details 
        [Authorize(Roles = "Resident,PropertyManager,SuperAdmin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var invitation = await _context.VisitorInvitations
                .Include(i => i.Resident).ThenInclude(r => r.User)
                .FirstOrDefaultAsync(i => i.InvitationId == id);
            if (invitation == null) return NotFound();
            return View(invitation);
        }

        // Resident: Cancel invitation 
        [Authorize(Roles = "Resident")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var invitation = await _context.VisitorInvitations.FindAsync(id);
            if (invitation == null) return NotFound();
            if (invitation.VisitStatus == "Pending")
            {
                invitation.VisitStatus = "Cancelled";
                await _context.SaveChangesAsync();
                TempData["Success"] = "Invitation cancelled.";
            }
            else
            {
                TempData["Error"] = "Only pending invitations can be cancelled.";
            }
            return RedirectToAction(nameof(Index));
        }

        //  Guard: Pending list with auto-expiry
        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> PendingList(string? search)
        {
            ViewBag.Search = search;
            var today = DateOnly.FromDateTime(DateTime.Today);

            // Auto-expire past invitations
            var expired = await _context.VisitorInvitations
                .Where(i => i.VisitStatus == "Pending" && i.ExpectedDate < today)
                .ToListAsync();
            foreach (var e in expired) e.VisitStatus = "Expired";
            if (expired.Any()) await _context.SaveChangesAsync();

            IQueryable<VisitorInvitation> query = _context.VisitorInvitations
                .Include(i => i.Resident).ThenInclude(r => r.User)
                .Where(i => i.VisitStatus == "Pending")
                .OrderBy(i => i.ExpectedDate);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(i =>
                    i.VisitorName.ToLower().Contains(s) ||
                    (i.InvitationToken != null && i.InvitationToken.ToLower().Contains(s)) ||
                    (i.VisitorPhone != null && i.VisitorPhone.Contains(s)));
            }
            return View(await query.ToListAsync());
        }

        //Guard: Check-In form GET 
        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> CheckIn(int id)
        {
            var invitation = await _context.VisitorInvitations
                .Include(i => i.Resident).ThenInclude(r => r.User)
                .FirstOrDefaultAsync(i => i.InvitationId == id);
            if (invitation == null) return NotFound();

            if (invitation.VisitStatus != "Pending")
            {
                TempData["Error"] = $"This invitation is already {invitation.VisitStatus}.";
                return RedirectToAction(nameof(PendingList));
            }

            return View(new GuardCheckInViewModel
            {
                InvitationId = invitation.InvitationId,
                VisitorName = invitation.VisitorName,
                VisitorPhone = invitation.VisitorPhone,
                PurposeOfVisit = invitation.PurposeOfVisit ?? "",
                HouseNumber = invitation.Resident?.HouseNumber ?? "",
                ResidentName = $"{invitation.Resident?.User?.Firstname} {invitation.Resident?.User?.Secondname}",
                ExpectedDate = invitation.ExpectedDate
            });
        }

        //Guard: Check-In POST 
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> CheckIn(GuardCheckInViewModel model, IFormFile? visitorPhoto)
        {
            if (!ModelState.IsValid) return View(model);

            var guardEmail = User.FindFirstValue(ClaimTypes.Name);
            var guardUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == guardEmail);
            if (guardUser == null) return Unauthorized();

            var invitation = await _context.VisitorInvitations
                .Include(i => i.Resident)
                .FirstOrDefaultAsync(i => i.InvitationId == model.InvitationId);
            if (invitation == null) return NotFound();

            // Handle photo upload
            string? photoPath = null;
            if (visitorPhoto != null && visitorPhoto.Length > 0)
            {
                var uploadDir = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot", "uploads", "visitors");
                Directory.CreateDirectory(uploadDir);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(visitorPhoto.FileName)}";
                using var stream = new FileStream(
                    Path.Combine(uploadDir, fileName), FileMode.Create);
                await visitorPhoto.CopyToAsync(stream);
                photoPath = $"/uploads/visitors/{fileName}";
            }

            //  Find or create visitor, save first
            var visitor = await _context.Visitors.FirstOrDefaultAsync(v =>
                v.FullName == model.VisitorName && v.ContactNumber == model.VisitorPhone);

            if (visitor == null)
            {
                visitor = new Visitor
                {
                    FullName = model.VisitorName,
                    IdNumber = model.IdNumber,
                    ContactNumber = model.VisitorPhone,
                    PhotoUrl = photoPath,
                    FirstVisitDate = DateTime.Now,
                    TotalVisits = 1,
                    CreatedAt = DateTime.Now,
                    InvitedViaInvitationId = invitation.InvitationId
                };
                _context.Visitors.Add(visitor);
            }
            else
            {
                visitor.TotalVisits = (visitor.TotalVisits ?? 0) + 1;
                visitor.UpdatedAt = DateTime.Now;
                if (photoPath != null) visitor.PhotoUrl = photoPath;
            }
            await _context.SaveChangesAsync();  // visitor saved, has ID now

            //  Create and save visit
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
                CheckInMethod = model.CheckInMethod,
                InvitationId = invitation.InvitationId,
                CreatedAt = DateTime.Now
            };
            _context.Visits.Add(visit);
            invitation.VisitStatus = "Arrived";
            await _context.SaveChangesAsync();  // visit saved, has ID now

            //  Notification- visit.VisitId is now valid
            if (invitation.Resident != null)
            {
                _context.Notifications.Add(new Notification
                {
                    ResidentId = invitation.Resident.ResidentId,
                    VisitId = visit.VisitId,
                    Title = "Visitor Arrived",
                    Message = $"{model.VisitorName} has arrived at your unit {model.HouseNumber}.",
                    NotificationType = "VisitorArrived",
                    IsRead = false,
                    CreatedAt = DateTime.Now
                });
                await _context.SaveChangesAsync();
            }

            await _activityLog.LogAsync("CheckIn",
                $"Pre-registered: {model.VisitorName} → {model.HouseNumber} via invitation #{invitation.InvitationId}",
                "Visit", visit.VisitId);

            TempData["Success"] = $"{model.VisitorName} checked in to {model.HouseNumber}.";
            return RedirectToAction("Index", "GuardDashboard");
        }

        //  Guard: Check-Out GET
        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> CheckOut(int visitId)
        {
            var visit = await _context.Visits
                .Include(v => v.Visitor)
                .FirstOrDefaultAsync(v => v.VisitId == visitId);
            if (visit == null) return NotFound();
            return View(visit);
        }

        //Guard: Check-Out POST 
        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Guard")]
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

            TempData["Success"] = $"{visit.Visitor?.FullName} checked out. Duration: {visit.VisitDuration} min.";
            return RedirectToAction("Index", "GuardDashboard");
        }
    }
}