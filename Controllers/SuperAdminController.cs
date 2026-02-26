using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MgeniTrack.Controllers
{
    public class SuperAdminController : Controller
    {
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
