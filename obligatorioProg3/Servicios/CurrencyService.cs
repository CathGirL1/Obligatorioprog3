using obligatorioProg3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace obligatorioProg3.Servicios
{
    public class CurrencyService
    {

        private readonly string apiKey = "89109cd01ee348b11f72dfe8e55fcf9c";

        public async Task<List<cotizacion>> ObtenerCotizacionesAsync()
        {
            var urlAccesoMoneda = $"http://apilayer.net/api/live?access_key={apiKey}&currencies=UYU,EUR,BRL&source=USD&format=1";

            using (var client = new HttpClient())
            {
                var formatoJson = await client.GetStringAsync(urlAccesoMoneda);

                var serializar = new JavaScriptSerializer(); 

                var resultado = serializar.Deserialize<CurrencyApiResponse> (formatoJson);

                var lista = new List<cotizacion>();

                foreach (var monedas in resultado.Quotes) 
                {
                    string tipoMoneda = monedas.Key.Substring(3);
                    lista.Add(new cotizacion
                    {
                        tipoMoneda = tipoMoneda,
                        valor = monedas.Value,
                        fecha = DateTime.Now
                    }); 
                }

                return lista; 
            }
        }

    }

    public class CurrencyApiResponse
    {
        public bool Success { get; set; }
        public string Terms { get; set; }
        public string Privacy { get; set; }
        public long Timestamp { get; set; }
        public string Source { get; set; }
        public Dictionary<string, double> Quotes { get; set; }
    }
}