using Microsoft.AspNetCore.Mvc;

namespace eMedicNETv7.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
