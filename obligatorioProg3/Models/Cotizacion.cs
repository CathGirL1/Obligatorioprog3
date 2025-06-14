using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace obligatorioProg3.Models
{
    public class cotizacion
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string tipoMoneda { get; set; }
        public double valor { get; set; }
    }
}