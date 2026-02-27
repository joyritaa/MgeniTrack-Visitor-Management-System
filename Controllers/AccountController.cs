using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MgeniTrack.Models;
using MgeniTrack.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace MgeniTrack.Controllers;
public class AccountController : Controller
{
    private readonly MgenitrackContext _context;

    public AccountController(MgenitrackContext context)
    {
        _context = context;
    }

    // GET: Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email == model.Email);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid email or password");
            return View(model);
        }

        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(user, user.Passwordhash, model.Password);

        if (result != PasswordVerificationResult.Success)
        {
            ModelState.AddModelError("", "Invalid email or password");
            return View(model);
        }

        if (user.UserStatus != "Active")
        {
            ModelState.AddModelError("", "Account is inactive");
            return View(model);
        }

        // Get active roles
        var activeRoles = user.UserRoles
            .Where(ur => ur.UserStatus == "Active")
            .Select(ur => ur.Role.RoleName)
            .ToList();

        if (!activeRoles.Any())
        {
            ModelState.AddModelError("", "User has no active role.");
            return View(model);
        }

        // Create claims for all active roles
        var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, user.Email)
};
        foreach (var role in activeRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

        // Redirect based on role priority
        if (activeRoles.Contains("SuperAdmin"))
            return RedirectToAction("Index", "AdminDashboard");

        if (activeRoles.Contains("PropertyManager"))
            return RedirectToAction("Index", "ManagerDashboard");

        if (activeRoles.Contains("Guard"))
            return RedirectToAction("Index", "GuardDashboard");

        if (activeRoles.Contains("Resident"))
            return RedirectToAction("Index", "ResidentDashboard");

        return RedirectToAction("Login");
    }
}
