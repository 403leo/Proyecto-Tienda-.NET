using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodigoProgramado_Grupo4.Filters;
using CodigoProgramado_Grupo4.Models;

namespace CodigoProgramado_Grupo4.Controllers
{
    [CustomAuthorizationFilter("Admin")]
    public class SuscripcionController : Controller
    {
        private UsuarioPedidosDbContext db = new UsuarioPedidosDbContext();

        // GET: Suscripcion
        public ActionResult Index()
        {
            var suscripciones = db.Suscripciones.Include(s => s.Usuarios);
            return View(suscripciones.ToList());
        }

        // GET: Suscripcion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suscripcion suscripcion = db.Suscripciones.Find(id);
            if (suscripcion == null)
            {
                return HttpNotFound();
            }
            return View(suscripcion);
        }

        // GET: Suscripcion/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Suscripcion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo_subs,Tipo_subs,UsuarioId")] Suscripcion suscripcion)
        {
            if (ModelState.IsValid)
            {
                db.Suscripciones.Add(suscripcion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", suscripcion.UsuarioId);
            return View(suscripcion);
        }

        // GET: Suscripcion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suscripcion suscripcion = db.Suscripciones.Find(id);
            if (suscripcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", suscripcion.UsuarioId);
            return View(suscripcion);
        }

        // POST: Suscripcion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo_subs,Tipo_subs,UsuarioId")] Suscripcion suscripcion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suscripcion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", suscripcion.UsuarioId);
            return View(suscripcion);
        }

        // GET: Suscripcion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suscripcion suscripcion = db.Suscripciones.Find(id);
            if (suscripcion == null)
            {
                return HttpNotFound();
            }
            return View(suscripcion);
        }

        // POST: Suscripcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Suscripcion suscripcion = db.Suscripciones.Find(id);
            db.Suscripciones.Remove(suscripcion);
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
