using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using obligatorioProg3.Models;
using Newtonsoft.Json;

namespace obligatorioProg3.Controllers
{
    public class ClimaController : Controller
    {
        // GET: Clima
        public async Task<ActionResult> Index()
        {
            string url = "https://api.openweathermap.org/data/2.5/forecast?lat=-34.9&lon=-54.95&appid=12a3592ad8edcaf303644d8568f11054&units=metric&lang=es";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "No se pudo obtener el clima.";
                    return View();
                }

                var json = await response.Content.ReadAsStringAsync();
                var clima = JsonConvert.DeserializeObject<ClimaAdo>(json);

                return View(clima);
            }
        }
    }
}