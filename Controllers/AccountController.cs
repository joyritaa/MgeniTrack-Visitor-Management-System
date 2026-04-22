using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MgeniTrack.Models;
using MgeniTrack.ViewModels;
using MgeniTrack.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace MgeniTrack.Controllers;

public class AccountController : Controller
{
    private readonly MgenitrackContext _context;
    private readonly ActivityLogService _logger;

    public AccountController(MgenitrackContext context, ActivityLogService logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _context.Users
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
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
            // Log failed attempt
            await _logger.LogAsync("LoginFailed",
                $"Failed login attempt for {model.Email}",
                "User", user.UserId, user.UserId);
            return View(model);
        }

        if (user.UserStatus != "Active")
        {
            ModelState.AddModelError("", "Account is inactive");
            return View(model);
        }

        var activeRoles = user.UserRoles
            .Where(ur => ur.UserStatus == "Active")
            .Select(ur => ur.Role.RoleName)
            .ToList();

        if (!activeRoles.Any())
        {
            ModelState.AddModelError("", "User has no active role.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email)
        };
        foreach (var role in activeRoles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

        // Update last login
        user.LastLogin = DateTime.Now;
        await _context.SaveChangesAsync();

        // log successful login
        await _logger.LogAsync("Login",
            $"{user.Firstname} logged in as {string.Join(", ", activeRoles)}",
            "User", user.UserId, user.UserId);

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

    public async Task<IActionResult> Logout()
    {
        // Log logout before signing out
        await _logger.LogAsync("Logout", "User signed out", "User");
        await HttpContext.SignOutAsync("MyCookieAuth");
        return RedirectToAction("Login");
    }
}