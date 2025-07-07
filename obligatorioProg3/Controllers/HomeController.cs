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
            
            return View(baseDeDatos.noticia.ToList());

        }
    }
}