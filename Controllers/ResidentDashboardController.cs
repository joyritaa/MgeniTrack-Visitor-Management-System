using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MgeniTrack.Controllers
{
    [Authorize(Roles ="Resident")]
    public class ResidentDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
