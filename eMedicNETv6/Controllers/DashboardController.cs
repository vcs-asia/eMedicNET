using Microsoft.AspNetCore.Mvc;

namespace eMedicNETv6.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
