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
using obligatorioProg3.Models.Cotizacion;

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
                var cotizacionApi = JsonConvert.DeserializeObject<CotizacionAdo>(json);

                var fechaCotizacion = DateTimeOffset.FromUnixTimeSeconds(cotizacionApi.Timestamp).Date; // para obtener la fecha exact de la ultima cotizacion

                using (var baseDeDatos = new ConexionAdoCotizacion())
                {
                    var cotizaciones = new List<cotizacion>()
                    {
                        new cotizacion
                        {
                            fecha = fechaCotizacion,
                            tipoMoneda = "Peso Uruguayo",
                            valor = cotizacionApi.Quotes["USDUYU"]
                        },
                        new cotizacion
                        {
                            fecha = fechaCotizacion, 
                            tipoMoneda = "Real", 
                            valor = cotizacionApi.Quotes["USDBRL"]
                        },
                        new cotizacion
                        {
                            fecha = fechaCotizacion,
                            tipoMoneda = "Euro",
                            valor = cotizacionApi.Quotes["USDEUR"]
                        }

                        // para aclarar: Quotes es una propiedad de json que contiene los tipos de moneda con sus valores, este mismo trae los valores de cada moneda
                    };

                    foreach (var monetizacion in cotizaciones)
                    {
                        bool existeFecha = baseDeDatos.cotizacion.Any(cot => cot.fecha == monetizacion.fecha && cot.tipoMoneda == monetizacion.tipoMoneda);

                        if (!existeFecha)
                        {
                            baseDeDatos.cotizacion.Add(monetizacion); 
                        }

                        // explicacion: para evitar un registro diario de actualizaciones duplicadas por dia, se pregunta si la fecha del tipo de moneda no existe en la base de datos, si es asi se guarda la ultima actualizacion de ese tipo de moneda
                    }

                    baseDeDatos.SaveChanges(); 
                   
                }


                return View(cotizacionApi);
            }
        }
    }
}