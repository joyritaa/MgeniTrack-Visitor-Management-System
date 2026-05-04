using MgeniTrack.Models;
using MgeniTrack.Services;
using MgeniTrack.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MgeniTrack.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly MgenitrackContext _context;
        private readonly DashboardNotifier _notifier;

        public UsersController(MgenitrackContext context, DashboardNotifier notifier)
        {
            _context = context;
            _notifier = notifier;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = _context.Users.Include(u => u.CreatedByNavigation);
            return View(await users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users
                .Include(u => u.CreatedByNavigation)
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (user == null) return NotFound();
            return View(user);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await _context.Roles
                .Select(r => new SelectListItem
                {
                    Value = r.RoleId.ToString(),
                    Text = r.RoleName
                }).ToListAsync();

            // Vacant units for resident assignment
            ViewBag.VacantUnits = await _context.Units
                .Where(u => u.IsOccupied == false)
                .OrderBy(u => u.UnitNumber)
                .Select(u => new SelectListItem
                {
                    Value = u.UnitId.ToString(),
                    Text = u.UnitNumber
                }).ToListAsync();

            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await RepopulateDropdowns();
                return View(model);
            }

            var hasher = new PasswordHasher<User>();
            var creatorUser = await _context.Users.FirstOrDefaultAsync();
            if (creatorUser == null)
                throw new InvalidOperationException("No creator user found.");

            var user = new User
            {
                Firstname = model.Firstname,
                Secondname = model.Secondname,
                Gender = model.Gender,
                IdNumber = model.IdNumber,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserStatus = "Active",
                CreatedAt = DateTime.Now,
                CreatedByNavigation = creatorUser
            };

            user.Passwordhash = hasher.HashPassword(user, model.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Assign role
            var userRole = new UserRole
            {
                UserId = user.UserId,
                RoleId = model.SelectedRoleId,
                Shift = model.Shift,
                UserStatus = "Active",
                AssignedAt = DateTime.Now
            };
            _context.UserRoles.Add(userRole);

            // create Resident record + mark unit occupied
            var role = await _context.Roles.FindAsync(model.SelectedRoleId);

            Resident? resident = null;
            string houseNumber = model.HouseNumber ?? "";

            if (role?.RoleName == "Resident")
            {
                // If a unit was selected from the dropdown, use that unit's number
                if (model.SelectedUnitId.HasValue)
                {
                    var unit = await _context.Units.FindAsync(model.SelectedUnitId.Value);
                    if (unit != null)
                    {
                        houseNumber = unit.UnitNumber;
                        unit.IsOccupied = true; // Mark occupied
                    }
                }

                resident = new Resident
                {
                    UserId = user.UserId,
                    HouseNumber = houseNumber,
                    UnitId = model.SelectedUnitId
                };
                _context.Residents.Add(resident);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = $"User {user.Firstname} created successfully.";

            // SignalR – notify admins
            await _notifier.NotifyUserCreated(new
            {
                userId = user.UserId,
                name = $"{user.Firstname} {user.Secondname}",
                role = role?.RoleName,
                houseNumber = houseNumber
            });

            if (model.SelectedUnitId.HasValue)
            {
                await _notifier.NotifyUnitStatusChanged(new
                {
                    unitId = model.SelectedUnitId,
                    unitNumber = houseNumber,
                    isOccupied = true
                });
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null) return NotFound();

            ViewBag.Roles = await _context.Roles
                .Select(r => new SelectListItem
                {
                    Value = r.RoleId.ToString(),
                    Text = r.RoleName
                }).ToListAsync();

            var model = new UserEditViewModel
            {
                UserId = user.UserId,
                Firstname = user.Firstname,
                Secondname = user.Secondname,
                Gender = user.Gender,
                IdNumber = user.IdNumber,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

                // if user has role
                SelectedRoleId = user.UserRoles.FirstOrDefault()?.RoleId ?? 0,

                // optional fields (if you store them somewhere)
                Shift = null,
                HouseNumber = null
            };

            return View(model);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _context.Roles
                    .Select(r => new SelectListItem
                    {
                        Value = r.RoleId.ToString(),
                        Text = r.RoleName
                    }).ToListAsync();

                return View(model);
            }

            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.UserId == model.UserId);

            if (user == null) return NotFound();

            // ─── UPDATE BASIC FIELDS ─────────────────────
            user.Firstname = model.Firstname;
            user.Secondname = model.Secondname;
            user.Gender = model.Gender;
            user.IdNumber = model.IdNumber;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            //OPTIONAL PASSWORD CHANGE
            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                var hasher = new PasswordHasher<User>();
                user.Passwordhash = hasher.HashPassword(user, model.NewPassword);
            }

            // UPDATE ROLE
            var existingRole = user.UserRoles.FirstOrDefault();
            if (existingRole != null)
            {
                existingRole.RoleId = model.SelectedRoleId;
            }
            else
            {
                user.UserRoles.Add(new UserRole
                {
                    RoleId = model.SelectedRoleId,
                    UserId = user.UserId
                });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.UserId == model.UserId))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var user = await _context.Users
                .Include(u => u.CreatedByNavigation)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null) _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ── helpers 
        private async Task RepopulateDropdowns()
        {
            ViewBag.Roles = await _context.Roles
                .Select(r => new SelectListItem { Value = r.RoleId.ToString(), Text = r.RoleName })
                .ToListAsync();

            ViewBag.VacantUnits = await _context.Units
                .Where(u => u.IsOccupied == false)
                .OrderBy(u => u.UnitNumber)
                .Select(u => new SelectListItem { Value = u.UnitId.ToString(), Text = u.UnitNumber })
                .ToListAsync();
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}