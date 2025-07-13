using obligatorioProg3.Models;
using obligatorioProg3.Servicios;
using System.Collections.Generic;
using System;
using System.Web.Mvc;

namespace obligatorioProg3.Filtros
{
    public class CotizacionesFilter : ActionFilterAttribute
    {
        private static List<cotizacion> _cacheCotizaciones = null;
        private static DateTime _cacheTime = DateTime.MinValue;
        private static readonly object _lock = new object();

        private readonly CurrencyService _currencyService = new CurrencyService();


        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.Controller is Controller controller)
            {
                lock (_lock)
                {
                    if (_cacheCotizaciones == null || (DateTime.Now - _cacheTime).TotalMinutes > 30)
                    {
                        try
                        {
                            _cacheCotizaciones = _currencyService.ObtenerCotizacionesAsync().GetAwaiter().GetResult();
                            _cacheTime = DateTime.Now;
                        }
                        catch (Exception ex)
                        {
                            // En caso de error, podemos seguir mostrando lo último que teníamos
                            if (_cacheCotizaciones == null)
                                _cacheCotizaciones = new List<cotizacion>();
                        }
                    }
                }

                controller.ViewBag.Cotizaciones = _cacheCotizaciones;
            }

            base.OnResultExecuting(filterContext);
        }

    }
}
