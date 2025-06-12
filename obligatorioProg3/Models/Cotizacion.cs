using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;

namespace obligatorioProg3.Models
{
    public class Cotizacion
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string tipoMoneda { get; set; }
        public float valor { get; set; }
    }
}