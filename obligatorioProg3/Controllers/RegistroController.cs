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
                id_rol = 1 // Se asigna el rol de cliente por defecto por cada usuario que se registra
            };

            db.usuario.Add(usuario);
            db.SaveChanges();

            var cliente = new cliente
            {
                id = usuario.id, // Asignar el id del usuario al cliente
                cedula = model.Cedula
            };

            datosCliente_dependenciaCedula datos = new datosCliente_dependenciaCedula
            {
                cedula = model.Cedula,
                cedulaCliente = model.Cedula,
                nombreReal = model.NombreReal,
                apellido = model.Apellido,
                fechaNacimiento = model.FechaNacimiento,
                cedula = model.Cedula, // Asignar la cedula del cliente a los datos del cliente
                cedulaCliente = model.Cedula, // Asignar la cedula del cliente (como la clave foranea de datosCliente_dependenciaCedula a cedula)
                cliente = cliente
            };

            cliente.datosCliente_dependenciaCedula = datos;
            usuario.cliente = cliente;
            usuario.rol = db.rol.Find(1); // Asignar el rol de cliente por defecto

            db.SaveChanges();


            return View(model);
        }
    }
}