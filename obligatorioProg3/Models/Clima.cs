using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace obligatorioProg3.Models
{
    public class clima
    {
        public int id {  get; set; }
        public DateTime fecha { get; set; }
        public int temperatura { get; set; }
        public string descripcion {  get; set; }
        public string icono { get; set; }
    }
}