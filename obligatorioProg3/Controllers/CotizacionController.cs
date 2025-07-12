using Newtonsoft.Json;
using obligatorioProg3.Models;
using QuickType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace obligatorioProg3.Controllers
{
    public class CotizacionController : Controller
    {
        // GET: Cotizacion
        public async Task<ActionResult> Index()
        {
            string url = "http://apilayer.net/api/live?access_key=89109cd01ee348b11f72dfe8e55fcf9c&currencies=UYU,EUR,BRL&source=USD&format=1";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "No se pudo obtener la cotizacion.";
                    return View();
                }

                var json = await response.Content.ReadAsStringAsync();
                var cotizacion = JsonConvert.DeserializeObject<CotizacionAdo>(json);

                return View(cotizacion);
            }
        }
    }
}