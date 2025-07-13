using obligatorioProg3.CustomValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using obligatorioProg3.Models;

namespace obligatorioProg3.ViewModels
{
    public class InicioViewModel
    {
        //noticias
        public List<noticia> noticias { get; set; }


        //Datos de programa
        public int idPrograma { get; set; }
        public string nombrePrograma { get; set; }
        public string descripcionPrograma { get; set; }
        public string imagenPrograma { get; set; }


        //Datos de horarios de programas
        public string dia { get; set; }
        public System.TimeSpan horaInicio { get; set; }
        public System.TimeSpan horaFinal { get; set; }
    }
}