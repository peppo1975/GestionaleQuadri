using GestionaleQuadri.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionaleQuadri.Controllers
{
    public class ControlPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Dashboard()
        {
            return View();
        }


        public IActionResult Commesse()
        {
            ViewCommessa vc = new ViewCommessa();

            string query = "SELECT commesse.azienda,\r\n       nome_azienda,\r\n       anno\r\nFROM   gestionale_quadri.commesse\r\n       JOIN gestionale_quadri.aziende\r\n         ON gestionale_quadri.aziende.azienda = gestionale_quadri.commesse.azienda\r\nGROUP  BY nome_azienda,\r\n          anno,\r\n          commesse.azienda\r\nORDER  BY anno DESC,\r\n          nome_azienda ASC";

            List<AnnoAzienda> aa = DatabaseController.SELECT_GET_LIST<AnnoAzienda>(query);

            vc.aa = aa;

            vc.anno = aa.Select(X => X.anno).Distinct().ToList();

            ViewData["ViewCommessa"] = vc;

            return View();
        }


        public IActionResult Color()
        {
            return View();
        }

        //public IActionResult Color(int id)

        public ActionResult ColorPassaggio()
        {
            return View();
        }


        public JsonResult test()
        {
            ViewCommessa vc = new ViewCommessa();

            string query = "SELECT nome_azienda, anno \r\nFROM gestionale_quadri.commesse\r\n    JOIN gestionale_quadri.aziende\r\n        ON gestionale_quadri.aziende.azienda = gestionale_quadri.commesse.azienda\r\ngroup by nome_azienda, anno\r\norder by anno desc, nome_azienda asc";

            List<AnnoAzienda> aa = DatabaseController.SELECT_GET_LIST<AnnoAzienda>(query);
            Debug.Assert(aa != null);
            return Json(aa);
        }

        public JsonResult cazz()
        {

            UserModel data = new UserModel() { Age = 12, FirstName = "Lagonigro", LastName = "Giuseppe", Id = 234 };

            // test here!
            Debug.Assert(data != null);
            return Json(data);
        }

        public string testString(int id, string text)
        {

            return $"{id} {text}";

        }

        [HttpPost]
        public JsonResult PostUser(UserModel data)
        {

            // test here!
            Debug.Assert(data != null);
            return Json(data);
        }
    }


    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
