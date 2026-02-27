using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MgeniTrack.Controllers
{
    [Authorize(Roles ="PropertyManager")]
    public class ManagerDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
