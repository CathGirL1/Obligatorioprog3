using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using obligatorioProg3.Models;

namespace obligatorioProg3.Controllers
{
    public class usuariosController : Controller
    {
        private vozDelEsteBsdEntities db = new vozDelEsteBsdEntities();

        // GET: usuarios
        public ActionResult Index()
        {
            var usuario = db.usuario.Include(u => u.rol).Include(u => u.cliente);
            ViewBag.Roles = db.rol.ToList();
            return View(usuario.ToList());
        }

        // GET: usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: usuarios/Create
        public ActionResult Create()
        {
            ViewBag.id_rol = new SelectList(db.rol, "id", "nombre");
            ViewBag.id = new SelectList(db.cliente, "id", "id");
            return View();
        }

        // POST: usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nickname,email,contrasenia,id_rol")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_rol = new SelectList(db.rol, "id", "nombre", usuario.id_rol);
            ViewBag.id = new SelectList(db.cliente, "id", "id", usuario.id);
            return View(usuario);
        }

        // POST: usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            usuario.nickname = form["nickname"];
            usuario.email = form["email"];
            usuario.contrasenia = form["contrasenia"];
            usuario.id_rol = int.Parse(form["id_rol"]);

            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var usuario = db.usuario.Find(id);

                    if (usuario == null)
                        return HttpNotFound();

                    if (usuario.id_rol != 1)
                    {
                        db.usuario.Remove(usuario);
                    }
                    else
                    {
                        var cliente = db.cliente.Find(id);
                        if (cliente != null)
                        {
                            var datos = db.datosCliente_dependenciaCedula
                                          .FirstOrDefault(d => d.id == cliente.id);
                            if (datos != null)
                                db.datosCliente_dependenciaCedula.Remove(datos);

                            db.cliente.Remove(cliente);
                        }
                        db.usuario.Remove(usuario);
                    }

                    db.SaveChanges();
                    transaction.Commit();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
