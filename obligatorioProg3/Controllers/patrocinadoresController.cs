using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using obligatorioProg3;

namespace obligatorioProg3.Controllers
{
    public class PatrocinadoresController : Controller
    {
        private vozDelEsteBsdEntities db = new vozDelEsteBsdEntities();

        // GET: patrocinadors
        public ActionResult Index()
        {
            return View(db.patrocinador.ToList());
        }

        // GET: patrocinadors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patrocinador patrocinador = db.patrocinador.Find(id);
            if (patrocinador == null)
            {
                return HttpNotFound();
            }
            return View(patrocinador);
        }

        // GET: patrocinadors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: patrocinadors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,descripcion,plann")] patrocinador patrocinador)
        {
            if (ModelState.IsValid)
            {
                db.patrocinador.Add(patrocinador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patrocinador);
        }

        // GET: patrocinadors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patrocinador patrocinador = db.patrocinador.Find(id);
            if (patrocinador == null)
            {
                return HttpNotFound();
            }
            return View(patrocinador);
        }

        // POST: patrocinadors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,plann")] patrocinador patrocinador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patrocinador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patrocinador);
        }

        // GET: patrocinadors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patrocinador patrocinador = db.patrocinador.Find(id);
            if (patrocinador == null)
            {
                return HttpNotFound();
            }
            return View(patrocinador);
        }

        // POST: patrocinadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            patrocinador patrocinador = db.patrocinador.Find(id);
            db.patrocinador.Remove(patrocinador);
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
