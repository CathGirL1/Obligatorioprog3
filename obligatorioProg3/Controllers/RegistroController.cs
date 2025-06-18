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

            usuario nuevoUsuario = new usuario
            {
                nickname = model.Nickname,
                email = model.Email,
                contrasenia = model.Contraseña,
                id_rol = 1, // Asignar rol cliente defecto
            };

            db.usuario.Add(nuevoUsuario);
            db.SaveChanges();

            cliente nuevoCliente = new cliente
            {
                id = db.usuario.Find(nuevoUsuario.id).id, // Asignar el ID del usuario recién creado
                cedula = model.Cedula,
                usuario = nuevoUsuario
            };

            datosCliente_dependenciaCedula datos = new datosCliente_dependenciaCedula
            {
                cedula = model.Cedula,
                cedulaCliente = model.Cedula,
                nombreReal = model.NombreReal,
                apellido = model.Apellido,
                fechaNacimiento = model.FechaNacimiento,
            };

            db.cliente.Add(nuevoCliente);
            db.datosCliente_dependenciaCedula.Add(datos);
            db.SaveChanges();


            return View(model);
        }
    }
}