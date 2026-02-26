using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MgeniTrack.Controllers
{
    public class PropertyManagerController : Controller
    {
        [Authorize(Roles = "PropertyManager")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
