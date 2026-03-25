using MgeniTrack.Models;
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

        public UsersController(MgenitrackContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .Include(u => u.Resident)
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
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            // ✅ FIX: Validate HouseNumber if role is Resident
            var role = await _context.Roles.FindAsync(model.SelectedRoleId);
            if (role != null && role.RoleName == "Resident" && string.IsNullOrWhiteSpace(model.HouseNumber))
            {
                ModelState.AddModelError("HouseNumber", "House number is required for Residents.");
            }

            if (role != null && role.RoleName == "Guard" && string.IsNullOrWhiteSpace(model.Shift))
            {
                ModelState.AddModelError("Shift", "Shift is required for Guards.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _context.Roles
                    .Select(r => new SelectListItem { Value = r.RoleId.ToString(), Text = r.RoleName })
                    .ToListAsync();
                return View(model);
            }

            // ✅ FIX: Get the currently logged-in user as the creator
            var creatorEmail = User.FindFirstValue(ClaimTypes.Name);
            var creatorUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == creatorEmail);
            if (creatorUser == null)
                return Unauthorized();

            var hasher = new PasswordHasher<User>();

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

            // If Resident → insert into Residents table
            if (role != null && role.RoleName == "Resident")
            {
                var resident = new Resident
                {
                    UserId = user.UserId,
                    HouseNumber = model.HouseNumber!
                };
                _context.Residents.Add(resident);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = $"User {user.Firstname} created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users
                .Include(u => u.UserRoles)
                .Include(u => u.Resident)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null) return NotFound();

            var currentRole = user.UserRoles.FirstOrDefault(ur => ur.UserStatus == "Active");

            var vm = new UserEditViewModel
            {
                UserId = user.UserId,
                Firstname = user.Firstname,
                Secondname = user.Secondname ?? "",
                Gender = user.Gender ?? "",
                IdNumber = user.IdNumber ?? "",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber ?? "",
                SelectedRoleId = currentRole?.RoleId ?? 0,
                Shift = currentRole?.Shift,
                HouseNumber = user.Resident?.HouseNumber
            };

            ViewBag.Roles = await _context.Roles
                .Select(r => new SelectListItem { Value = r.RoleId.ToString(), Text = r.RoleName })
                .ToListAsync();

            return View(vm);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserEditViewModel model)
        {
            if (id != model.UserId) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _context.Roles
                    .Select(r => new SelectListItem { Value = r.RoleId.ToString(), Text = r.RoleName })
                    .ToListAsync();
                return View(model);
            }

            var user = await _context.Users
                .Include(u => u.UserRoles)
                .Include(u => u.Resident)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null) return NotFound();

            user.Firstname = model.Firstname;
            user.Secondname = model.Secondname;
            user.Gender = model.Gender;
            user.IdNumber = model.IdNumber;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            // ✅ Only update password if a new one was provided
            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                var hasher = new PasswordHasher<User>();
                user.Passwordhash = hasher.HashPassword(user, model.NewPassword);
            }

            // Update role
            var existingRole = user.UserRoles.FirstOrDefault(ur => ur.UserStatus == "Active");
            if (existingRole != null)
            {
                existingRole.RoleId = model.SelectedRoleId;
                existingRole.Shift = model.Shift;
            }

            // Update resident house number if applicable
            if (user.Resident != null && !string.IsNullOrWhiteSpace(model.HouseNumber))
            {
                user.Resident.HouseNumber = model.HouseNumber;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "User updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
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
            if (user != null)
            {
                // ✅ Soft delete — set status to Inactive instead of deleting
                user.UserStatus = "Inactive";
                await _context.SaveChangesAsync();
            }
            TempData["Success"] = "User deactivated successfully.";
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}