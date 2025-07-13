using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using obligatorioProg3;
using obligatorioProg3.CustomValidations;
using obligatorioProg3.Models;

namespace obligatorioProg3.Controllers
{
    public class ConductoresController : Controller
    {
        private vozDelEsteBsdEntities db = new vozDelEsteBsdEntities();

        // GET: conductores
        public ActionResult Index()
        {
            if (!Utilidades.usuarioTieneAccesoACrudSiONo(Session))
            {
                return RedirectToAction("Login", "Login"); 
            }
            ViewBag.CrearConductor = db.permiso.Where(p => p.id == 16).FirstOrDefault(); 
            ViewBag.EditarConductor = db.permiso.Where(p => p.id == 17).FirstOrDefault();
            ViewBag.BorrarConductor = db.permiso.Where(p => p.id == 18).FirstOrDefault();

            return View(db.conductor.ToList());
        }

        // GET: conductores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            conductor conductor = db.conductor.Find(id);
            if (conductor == null)
            {
                ViewBag.ErrorMessage = "El conductor que buscás no existe.";
                return View(new conductor()); 
            }
            return View(conductor);
        }

        // GET: conductores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: conductores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cedula,nombre,biografia")] conductor conductor)
        {
            if (ModelState.IsValid)
            {
                db.conductor.Add(conductor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(conductor);
        }

        // GET: conductores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            conductor conductor = db.conductor.Find(id);
            if (conductor == null)
            {
                ViewBag.ErrorMessage = "El conductor que quieres editar no existe.";
                return View(new conductor());
            }
            return View(conductor);
        }

        // POST: conductores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cedula,nombre,biografia")] conductor conductor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conductor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(conductor);
        }

        // GET: conductores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            conductor conductor = db.conductor.Find(id);
            if (conductor == null)
            {
                ViewBag.ErrorMessage = "El conductor que quieres eliminar no existe.";
                return View(new conductor());
            }
            return View(conductor);
        }

        // POST: conductores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            conductor conductor = db.conductor.Find(id);
            db.conductor.Remove(conductor);
            db.SaveChanges();
            return RedirectToAction("Index");
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
