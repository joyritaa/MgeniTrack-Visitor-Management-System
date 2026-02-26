using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MgeniTrack.Controllers
{
    public class GuardController : Controller
    {
        [Authorize(Roles = "Guard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
