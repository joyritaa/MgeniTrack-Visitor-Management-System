using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MgeniTrack.Models;
using MgeniTrack.ViewModels;
using MgeniTrack.Services;
using MgeniTrack.Helpers;

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

        // RESIDENT: List their own invitations
        [Authorize(Roles = "Resident,PropertyManager,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            IQueryable<VisitorInvitation> query = _context.VisitorInvitations
                .Include(i => i.Resident).ThenInclude(r => r.User)
                .OrderByDescending(i => i.CreatedAt);

            // Residents only see their own invitations
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

        // RESIDENT: Create invitation — GET
        [Authorize(Roles = "Resident")]
        public IActionResult Create()
        {
            return View(new InvitationCreateViewModel
            {
                ExpectedDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1))
            });
        }

        // RESIDENT: Create invitation — POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Resident")]
        public async Task<IActionResult> Create(InvitationCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Get the logged-in resident
            var email = User.FindFirstValue(ClaimTypes.Name);
            var resident = await _context.Residents
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.User.Email == email);

            if (resident == null)
            {
                ModelState.AddModelError("", "Resident profile not found. Contact your administrator.");
                return View(model);
            }

            // Generate a unique invitation token
            var token = Guid.NewGuid().ToString("N").ToUpper().Substring(0, 10);

            var invitation = new VisitorInvitation
            {
                InvitationId = 0, // EF will assign
                ResidentId = resident.ResidentId,
                VisitorName = model.VisitorName,
                VisitorPhone = model.VisitorPhone,
                VisitorEmail = model.VisitorEmail,
                PurposeOfVisit = model.PurposeOfVisit,
                ExpectedDate = model.ExpectedDate,
                InvitationToken = token,
                QrCodePath = $"/qrcodes/{token}.png", // placeholder path
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

        // RESIDENT: Invitation details
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

      // RESIDENT: Cancel invitation
        [Authorize(Roles = "Resident")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var invitation = await _context.VisitorInvitations.FindAsync(id);
            if (invitation == null) return NotFound();

            // Only allow cancel if still pending
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

        // GUARD: View all pending invitations to verify
        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> PendingList(string? search)
        {
            ViewBag.Search = search;

            var today = DateOnly.FromDateTime(DateTime.Today);

            // Auto-expire past invitations
            var expired = await _context.VisitorInvitations
                .Where(i => i.VisitStatus == "Pending" && i.ExpectedDate < today)
                .ToListAsync();

            foreach (var e in expired)
                e.VisitStatus = "Expired";

            if (expired.Any())
                await _context.SaveChangesAsync();

            // Load pending for today or future
            IQueryable<VisitorInvitation> query = _context.VisitorInvitations
                .Include(i => i.Resident).ThenInclude(r => r.User)
                .Where(i => i.VisitStatus == "Pending")
                .OrderBy(i => i.ExpectedDate);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(i =>
                    i.VisitorName.ToLower().Contains(search) ||
                    (i.InvitationToken != null && i.InvitationToken.ToLower().Contains(search)) ||
                    (i.VisitorPhone != null && i.VisitorPhone.Contains(search)));
            }

            return View(await query.ToListAsync());
        }

        // GUARD: Check-in form — GET (pre-filled from invitation)
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

            var vm = new GuardCheckInViewModel
            {
                InvitationId = invitation.InvitationId,
                VisitorName = invitation.VisitorName,
                VisitorPhone = invitation.VisitorPhone,
                PurposeOfVisit = invitation.PurposeOfVisit ?? "",
                HouseNumber = invitation.Resident?.HouseNumber ?? "",
                ResidentName = $"{invitation.Resident?.User?.Firstname} {invitation.Resident?.User?.Secondname}",
                ExpectedDate = invitation.ExpectedDate
            };

            return View(vm);
        }
        // GUARD: Check-in form — POST (creates a Visit record)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> CheckIn(GuardCheckInViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Get logged-in guard's user record
            var guardEmail = User.FindFirstValue(ClaimTypes.Name);
            var guardUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == guardEmail);

            if (guardUser == null) return Unauthorized();

            // Load invitation
            var invitation = await _context.VisitorInvitations
                .Include(i => i.Resident)
                .FirstOrDefaultAsync(i => i.InvitationId == model.InvitationId);

            if (invitation == null) return NotFound();

            // Create or reuse visitor record (match by name + phone)
            var visitor = await _context.Visitors
                .FirstOrDefaultAsync(v =>
                    v.FullName == model.VisitorName &&
                    v.ContactNumber == model.VisitorPhone);

            if (visitor == null)
            {
                visitor = new Visitor
                {
                    FullName = model.VisitorName,
                    IdNumber = model.IdNumber,
                    ContactNumber = model.VisitorPhone,
                    FirstVisitDate = DateTime.Now,
                    TotalVisits = 1,
                    CreatedAt = DateTime.Now,
                    InvitedViaInvitationId = invitation.InvitationId,
                    InvitedViaInvitation = invitation
                };
                _context.Visitors.Add(visitor);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Increment visit count
                visitor.TotalVisits = (visitor.TotalVisits ?? 0) + 1;
                visitor.UpdatedAt = DateTime.Now;
            }

            // Create the Visit record
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
                CreatedAt = DateTime.Now,
                Visitor = visitor,
                CheckedInByNavigation = guardUser
            };

            _context.Visits.Add(visit);

            // Update invitation status → Arrived
            invitation.VisitStatus = "Arrived";

            await _context.SaveChangesAsync();

            await _activityLog.LogAsync("CheckIn",
                $"Pre-registered: {model.VisitorName} checked into {model.HouseNumber} via invitation",
                "Visit", visit.VisitId);

            TempData["Success"] = $"{model.VisitorName} checked in successfully to {model.HouseNumber}.";
            return RedirectToAction("Index", "GuardDashboard");
        }

        // GUARD: Check-out a visitor
        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> CheckOut(int visitId)
        {
            var visit = await _context.Visits
                .Include(v => v.Visitor)
                .FirstOrDefaultAsync(v => v.VisitId == visitId);

            if (visit == null) return NotFound();
            return View(visit);
        }

        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> CheckOutConfirmed(int visitId)
        {
            var visit = await _context.Visits.FindAsync(visitId);
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

            TempData["Success"] = "Visitor checked out successfully.";
            return RedirectToAction("Index", "GuardDashboard");
        }
    }
}