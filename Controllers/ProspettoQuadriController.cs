using Microsoft.AspNetCore.Mvc;

namespace GestionaleQuadri.Controllers
{
    public class ProspettoQuadriController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

      public  IActionResult NewProspect() {
            return View();
        }
    }
}
