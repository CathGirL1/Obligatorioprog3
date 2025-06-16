using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using obligatorioProg3;
using obligatorioProg3.Models;
using System.Data;
using System.Data.Entity;
using System.Net;
using obligatorioProg3.ViewModels;

namespace obligatorioProg3.Controllers
{
    public class RegistroController : Controller
    {
        private vozDelEsteBsdEntities db = new vozDelEsteBsdEntities();

        // GET: Registro
        public ActionResult CrearUsuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearUsuario(RegistroUsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = new usuario
            {
                nickname = model.Nickname,
                email = model.Email,
                contrasenia = model.Contraseña
            };

            var cliente = new cliente
            {
                cedula = model.Cedula
            };

            var datos = new datosCliente_dependenciaCedula
            {
                nombreReal = model.NombreReal,
                apellido = model.Apellido,
                fechaNacimiento = model.FechaNacimiento,
                cliente = cliente
            };

            cliente.datosCliente_dependenciaCedula = datos;
            usuario.cliente = cliente;

            db.usuario.Add(usuario);
            db.SaveChanges();

            return View(model);
        }
    }
}