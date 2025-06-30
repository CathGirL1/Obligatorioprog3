using obligatorioProg3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace obligatorioProg3.Controllers
{
    public class HomeController : Controller
    {
        private vozDelEsteBsdEntities baseDeDatos = new vozDelEsteBsdEntities();
        public ActionResult Index()
        {
            var noticiasPrecargo = new List<noticia>();
            noticiasPrecargo.Add(new noticia()
            {
                id = 1000,
                titulo = "Accidente de transito",
                contenido = "se fue y choco",
                fechaPublicacion = new DateTime(2005, 9, 22),
                imagen = "https://imgs.elpais.com.uy/dims4/default/0afc2d7/2147483647/strip/true/crop/1600x1100+0+75/resize/1440x990!/quality/90/?url=https%3A%2F%2Fel-pais-uruguay-production-web.s3.us-east-1.amazonaws.com%2Fbrightspot%2Ff9%2F60%2F8b15e9ae4605a9ade17ef976a279%2Fwhatsapp-image-2024-03-27-at-10-22-37-am.jpeg"
            });

            noticiasPrecargo.Add(new noticia()
            {
                id = 1001, 
                titulo = "La roca gana el torneo de lucha wwf", 
                contenido = "datos de la pelea y estadisticas", 
                fechaPublicacion = new DateTime(2001, 12, 3), 
                imagen = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9e/Therockaswwfchampion.jpg/250px-Therockaswwfchampion.jpg"
            });

            var listaNoticiasDeBsd = baseDeDatos.noticia.ToList();

            var todasLasNotcias = noticiasPrecargo.Concat(listaNoticiasDeBsd).ToList(); 

            return View(todasLasNotcias);
        }
    }
}