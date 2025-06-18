using obligatorioProg3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace obligatorioProg3.Controllers
{
    public class LoginController : Controller
    {
        private vozDelEsteBsdEntities db = new vozDelEsteBsdEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(usuario model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            


            return View(model);
        }

        public ActionResult Logout()
        {
            // Aquí puedes implementar la lógica de cierre de sesión, como limpiar la sesión del usuario.
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}