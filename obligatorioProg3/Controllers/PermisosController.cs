using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Net;
using obligatorioProg3;
using obligatorioProg3.Models;

namespace obligatorioProg3.Controllers
{
    public class PermisosController : Controller
    {
        private vozDelEsteBsdEntities db = new vozDelEsteBsdEntities();

        // GET: Permisos
        public ActionResult Index()
        {
            return View(db.permiso.ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var permiso = db.permiso.Find(id);
            if (permiso == null)
            {
                return HttpNotFound();
            }

            permiso.estado = bool.Parse(form["estado"]);

            db.Entry(permiso).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}