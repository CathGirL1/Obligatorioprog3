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


        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(usuario model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = db.usuario.FirstOrDefault(u => u.email == model.email && u.contrasenia == model.contrasenia);

            if(user != null)
            {
            Session["IdRol"] = user.id_rol;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }


    }
}