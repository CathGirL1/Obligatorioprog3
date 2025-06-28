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
    public class NoticiasController : Controller
    {
        private vozDelEsteBsdEntities db = new vozDelEsteBsdEntities();

        private bool usuarioTieneAccesoACrudNoticiasSiONo()
        {
            if (Session["IdRol"] == null)
                return false;

            int id_Rol = (int)Session["IdRol"];

            return id_Rol == 2 || id_Rol == 3;

        }

        // GET: Noticias
        public ActionResult Index()
        {
            if (!usuarioTieneAccesoACrudNoticiasSiONo())
            {
                return RedirectToAction("Login", "Login"); 
            }
            return View(db.noticia.ToList());
        }

        // GET: Noticias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            noticia noticia = db.noticia.Find(id);
            if (noticia == null)
            {
                // se pasa mensaje al ViewBag para mostrar en la vista Edit
                ViewBag.ErrorMessage = "La noticia que buscás no existe.";
                return View(new noticia()); // se paasa un modelo vacío para que no falle la vista
            }
            return View(noticia);
        }

        // GET: Noticias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Noticias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,titulo,contenido,fechaPublicacion,imagen")] noticia noticia)
        {

            if (ModelState.IsValid)
            {
                db.noticia.Add(noticia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(noticia);
        }

        // GET: Noticias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            noticia noticia = db.noticia.Find(id);
            if (noticia == null)
            {
                // se pasa mensaje al ViewBag para mostrar en la vista Edit
                ViewBag.ErrorMessage = "La noticia que quieres editar no existe.";
                return View(new noticia()); // se paasa un modelo vacío para que no falle la vista
            }
            return View(noticia);
        }

        // POST: Noticias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,titulo,contenido,fechaPublicacion,imagen")] noticia noticia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noticia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            return View(noticia);
        }

        // GET: Noticias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            noticia noticia = db.noticia.Find(id);
            if (noticia == null)
            {
                // se pasa mensaje al ViewBag para mostrar en la vista Edit
                ViewBag.ErrorMessage = "La noticia que quieres eliminar no existe.";
                return View(new noticia()); // se paasa un modelo vacío para que no falle la vista
            }
            return View(noticia);
        }

        // POST: Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            noticia noticia = db.noticia.Find(id);
            db.noticia.Remove(noticia);
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
