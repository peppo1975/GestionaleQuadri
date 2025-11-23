using GestionaleQuadri.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GestionaleQuadri.Controllers
{
    public class CommesseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult AziendeAnno(string id)
        {
            List<Azienda> az = new List<Azienda>();

            string query = $"SELECT gestionale_quadri.commesse.azienda,\r\n       gestionale_quadri.aziende.nome_azienda\r\nFROM   gestionale_quadri.commesse\r\n       INNER JOIN gestionale_quadri.aziende\r\n               ON gestionale_quadri.commesse.azienda =\r\n                  gestionale_quadri.aziende.azienda\r\nWHERE  anno = '{id}'\r\nGROUP  BY gestionale_quadri.commesse.azienda,\r\n          gestionale_quadri.aziende.nome_azienda\r\nORDER  BY gestionale_quadri.aziende.nome_azienda ASC ";

            az = DatabaseController.SELECT_GET_LIST<Azienda>(query);

            //UserModel data = new UserModel() { Age = 12, FirstName = "Lagonigro", LastName = "Giuseppe", Id = 234 };

            // test here!
            Debug.Assert(az != null);

            return Json(az);
        }

        [HttpGet]
        public JsonResult AziendeAnnoCommesse(string anno, string azienda)
        {
            List<Commitente> cm = new List<Commitente>();
            string query = $"SELECT * FROM [gestionale_quadri].[commesse] " +
                                $" WHERE anno = '{anno}' AND azienda = '{azienda}' AND ciclo_lavoro = 'Y' " +
                                $" ORDER BY COMMESSA desc";

            cm = DatabaseController.SELECT_GET_LIST<Commitente>(query);
            return Json(cm);
        }

        [HttpGet]
        public JsonResult CommessaDettaglio(string id)
        {
            List<Quadro> q = new List<Quadro>();
            string query = $"SELECT " +
                $"\"@quadri\".nome_quadro," +
                $"\"@quadri\".odl, *" +
                $"   FROM [gestionale_quadri].[commesse] as \"@commesse\"" +
                $"  inner join gestionale_quadri.quadri as \"@quadri\" on \"@commesse\".commessa = \"@quadri\".commessa\r\n  where \"@commesse\".commessa = {id} " +
                $"  AND" +
                $"  \"@commesse\".ciclo_lavoro = 'Y'\r\n       ORDER BY\r\n                        \"@quadri\".data_inserimento DESC\r\n                        , \"@quadri\".quadro ASC";
            q = DatabaseController.SELECT_GET_LIST<Quadro>(query);
            return Json(q);

        }

    }
}
