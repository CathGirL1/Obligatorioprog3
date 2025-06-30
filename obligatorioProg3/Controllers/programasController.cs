using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using obligatorioProg3;
using obligatorioProg3.Models;

namespace obligatorioProg3.Controllers
{
    public class ProgramasController : Controller
    {
        private vozDelEsteBsdEntities db = new vozDelEsteBsdEntities();

        // GET: programas
        public ActionResult Index()
        {
            return View(db.programa.ToList());
        }

        // GET: programas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            programa programa = db.programa.Find(id);
            if (programa == null)
            {
                return HttpNotFound();
            }
            return View(programa);
        }

        // GET: programas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: programas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            var programa = new programa
            {
                nombre = form["nombre"],
                descripcion = form["descripcion"],
                imagen = form["imagen"]
            };

            if (ModelState.IsValid)
            {
                db.programa.Add(programa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(programa);
        }

        // GET: programas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            programa programa = db.programa.Find(id);
            if (programa == null)
            {
                return HttpNotFound();
            }
            return View(programa);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var programa = db.programa.Find(id);
            if (programa == null)
            {
                return HttpNotFound();
            }

            programa.nombre = form["nombre"];
            programa.descripcion = form["descripcion"];
            programa.imagen = form["imagen"];

            db.Entry(programa).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }






        // GET: programas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            programa programa = db.programa.Find(id);
            if (programa == null)
            {
                return HttpNotFound();
            }
            return View(programa);
        }

        // POST: programas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            programa programa = db.programa.Find(id);

            if (programa == null)
            {
                return HttpNotFound();
            }

            List<horario_programa> horariosProgramaEliminado = db.horario_programa.Where(h => h.idPrograma == id).ToList();

            if(horariosProgramaEliminado.Count != 0) db.horario_programa.RemoveRange(horariosProgramaEliminado);

            db.programa.Remove(programa);
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
