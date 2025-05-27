using GestionaleQuadri.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace GestionaleQuadri.Controllers
{
    public class CommesseController : Controller
    {
        public IActionResult Index()
        {
            List<Commesse> commesse = new List<Commesse>();

            commesse = Commesse.AnnoList();

            var json = JsonSerializer.Serialize(commesse.ToList());

            return View(commesse);
        }


        public IActionResult CommesseAziendaAnno(string? anno,string? azienda)
        {

            List<CommesseTable> commesseTable = Commesse.CommesseAziendaAnno(anno, azienda);

            var json = JsonSerializer.Serialize(commesseTable.ToList());

            return Content(json);
        }


        public IActionResult AziendeAnno(string? id)
        {
            List<AziendeAnno> list = new List<AziendeAnno>();

            list = Commesse.AziendeAnno(id.ToString());

            var json = JsonSerializer.Serialize(list.ToList());

            return Content(json);

            // return View();
        }
    }
}
