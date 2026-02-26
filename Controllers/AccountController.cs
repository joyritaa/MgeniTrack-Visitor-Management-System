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
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefault(u => u.Email == model.Email);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid email or password");
            return View(model);
        }

        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(user, user.Passwordhash, model.Password);

        if (result == PasswordVerificationResult.Success)
        {
            // Check if user is active
            if (user.UserStatus != "Active")
            {
                ModelState.AddModelError("", "Account is inactive");
                return View(model);
            }

            // Create Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("UserId", user.UserId.ToString())
            };

            foreach (var role in user.UserRoles)
            {
                if (role.UserStatus == "Active")
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Role.RoleName));
                }
            }

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            HttpContext.SignInAsync("MyCookieAuth", principal).Wait();

            if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "SuperAdmin"))
            {
                return RedirectToAction("Dashboard", "SuperAdmin");
            }

            if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "PropertyManager"))
            {
                return RedirectToAction("Dashboard", "PropertyManager");
            }

            if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Guard"))
            {
                return RedirectToAction("Dashboard", "Guard");
            }

            return RedirectToAction("Login");
        }

        ModelState.AddModelError("", "Invalid email or password");
        return View(model);
    }
}
