using Microsoft.AspNetCore.Mvc;

namespace GestionaleQuadri.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
