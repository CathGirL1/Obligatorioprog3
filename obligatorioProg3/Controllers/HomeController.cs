using obligatorioProg3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;
using obligatorioProg3.ViewModels;

namespace obligatorioProg3.Controllers
{
    public class HomeController : Controller
    {
        private vozDelEsteBsdEntities db = new vozDelEsteBsdEntities();
        public ActionResult Index()
        {
            //Obtener fechas y horas actuales por separado para validar el programa que se tiene que mostrar

            var tiempoActual = DateTime.Now;
            var horaActual = new TimeSpan(tiempoActual.Hour, tiempoActual.Minute, 0);
            var diaActual = tiempoActual.ToString("dddd", new System.Globalization.CultureInfo("es-ES")).ToLower();

            InicioViewModel datosInicio = new InicioViewModel();

            //se cargan las noticias
            datosInicio.noticias = db.noticia.ToList();


            //se cargan los horarios del programa a mostrar
            datosInicio.idPrograma = db.horario_programa.Where(hp => hp.dia == diaActual
                    && hp.horaInicio <= horaActual && hp.horaFinal >= horaActual).Select(hp => hp.idPrograma).FirstOrDefault(); //aca se valida que el idPrograma que se carugue cumpla con que su dia sea el dia actual y que la hora actual se encuentre entre la hora de inicio y la hora final del programa

            var progActual = datosInicio.idPrograma; //se guarda el id del programa obtenido para obtener los datos restantes de ese programa

            datosInicio.dia = db.horario_programa.Where(hp => hp.idPrograma == progActual).Select(hp => hp.dia).FirstOrDefault();

            datosInicio.horaInicio = db.horario_programa.Where(hp => hp.idPrograma == progActual).Select(hp => hp.horaInicio).FirstOrDefault();

            datosInicio.horaFinal = db.horario_programa.Where(hp => hp.idPrograma == progActual).Select(hp => hp.horaFinal).FirstOrDefault();


            //se cargan los datos del programa
            datosInicio.nombrePrograma = db.programa.Where(p => p.id == progActual).Select(p => p.nombre).FirstOrDefault();

            datosInicio.descripcionPrograma = db.programa.Where(p => p.id == progActual).Select(p => p.descripcion).FirstOrDefault();

            datosInicio.imagenPrograma = db.programa.Where(p => p.id == progActual).Select(p => p.imagen).FirstOrDefault();


            return View(datosInicio);

        }
    }
}