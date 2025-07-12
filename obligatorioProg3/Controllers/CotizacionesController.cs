using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using obligatorioProg3.Servicios;

namespace obligatorioProg3.Controllers
{
    public class CotizacionesController : Controller
    { 
        private readonly CurrencyService _currencyService;


        public CotizacionesController() 
        {

            _currencyService = new CurrencyService(); 
        
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerCotizaciones()
        {
            var cotizaciones = await _currencyService.ObtenerCotizacionesAsync();
            return Json(cotizaciones, JsonRequestBehavior.AllowGet);
        }

    }
}