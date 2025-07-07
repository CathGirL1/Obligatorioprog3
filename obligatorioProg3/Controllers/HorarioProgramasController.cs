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
    public class HorarioProgramasController : Controller
    {
        private vozDelEsteBsdEntities db = new vozDelEsteBsdEntities();

        // GET: HorarioProgramas
        public ActionResult Index()
        {
            var horario_programa = db.horario_programa.Include(h => h.programa);
            ViewBag.programas = db.programa.ToList();

            ViewBag.CrearHorarioPrograma = db.permiso.Where(p => p.id == 9).FirstOrDefault();
            ViewBag.EditarHorarioPrograma = db.permiso.Where(p => p.id == 10).FirstOrDefault();
            ViewBag.BorrarHorarioPrograma = db.permiso.Where(p => p.id == 11).FirstOrDefault();
            return View(horario_programa.ToList());
        }

        // GET: HorarioProgramas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            horario_programa horario_programa = db.horario_programa.Find(id);
            if (horario_programa == null)
            {
                return HttpNotFound();
            }
            return View(horario_programa);
        }

        // GET: HorarioProgramas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HorarioProgramas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            var horario_programa = new horario_programa
            {
                idPrograma = int.Parse(form["idPrograma"]),
                dia = form["dia"],
                horaInicio = TimeSpan.Parse(form["horaInicio"]),
                horaFinal = TimeSpan.Parse(form["horaFinal"])
            };

            if (ModelState.IsValid)
            {
                db.horario_programa.Add(horario_programa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(horario_programa);
        }

        // GET: HorarioProgramas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            horario_programa horario_programa = db.horario_programa.Find(id);
            if (horario_programa == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPrograma = new SelectList(db.programa, "id", "nombre", horario_programa.idPrograma);
            return View(horario_programa);
        }

        // POST: HorarioProgramas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int idPrograma, string dia, TimeSpan horaInicio, TimeSpan horaFinal, FormCollection form)
        {
            var horarioPrograma = db.horario_programa.Find(idPrograma, dia, horaInicio, horaFinal);

            if (horarioPrograma == null)
            {
                return HttpNotFound();
            }

            horarioPrograma.idPrograma = int.Parse(form["idPrograma"]);
            horarioPrograma.dia = form["dia"];
            horarioPrograma.horaInicio = TimeSpan.Parse(form["horaInicio"]);
            horarioPrograma.horaFinal = TimeSpan.Parse(form["horaFinal"]);

            db.Entry(horarioPrograma).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: HorarioProgramas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            horario_programa horario_programa = db.horario_programa.Find(id);
            if (horario_programa == null)
            {
                return HttpNotFound();
            }
            return View(horario_programa);
        }

        // POST: HorarioProgramas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idPrograma, string dia, TimeSpan horaInicio, TimeSpan horaFinal)
        {
            horario_programa horario_programa = db.horario_programa.Find(idPrograma, dia, horaInicio, horaFinal);
            db.horario_programa.Remove(horario_programa);
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
