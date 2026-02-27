using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MgeniTrack.Controllers
{
    [Authorize(Roles ="Guard")]
    public class GuardDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
