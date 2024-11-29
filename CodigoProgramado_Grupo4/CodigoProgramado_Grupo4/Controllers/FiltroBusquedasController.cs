using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodigoProgramado_Grupo4.Models;

namespace CodigoProgramado_Grupo4.Controllers
{
    public class FiltroBusquedasController : Controller
    {
        private UsuarioPedidosDbContext db = new UsuarioPedidosDbContext();

        // GET: FiltroBusquedas
        public ActionResult Index()
        {
            var filtroBusquedas = db.FiltroBusquedas.Include(f => f.Productos);
            return View(filtroBusquedas.ToList());
        }

        // GET: FiltroBusquedas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiltroBusqueda filtroBusqueda = db.FiltroBusquedas.Find(id);
            if (filtroBusqueda == null)
            {
                return HttpNotFound();
            }
            return View(filtroBusqueda);
        }

        // GET: FiltroBusquedas/Create
        public ActionResult Create()
        {
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto");
            return View();
        }

        // POST: FiltroBusquedas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Categoria,rango_precios,disponibilidad,ProductoId")] FiltroBusqueda filtroBusqueda)
        {
            if (ModelState.IsValid)
            {
                db.FiltroBusquedas.Add(filtroBusqueda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto", filtroBusqueda.ProductoId);
            return View(filtroBusqueda);
        }

        // GET: FiltroBusquedas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiltroBusqueda filtroBusqueda = db.FiltroBusquedas.Find(id);
            if (filtroBusqueda == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto", filtroBusqueda.ProductoId);
            return View(filtroBusqueda);
        }

        // POST: FiltroBusquedas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Categoria,rango_precios,disponibilidad,ProductoId")] FiltroBusqueda filtroBusqueda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filtroBusqueda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto", filtroBusqueda.ProductoId);
            return View(filtroBusqueda);
        }

        // GET: FiltroBusquedas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiltroBusqueda filtroBusqueda = db.FiltroBusquedas.Find(id);
            if (filtroBusqueda == null)
            {
                return HttpNotFound();
            }
            return View(filtroBusqueda);
        }

        // POST: FiltroBusquedas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FiltroBusqueda filtroBusqueda = db.FiltroBusquedas.Find(id);
            db.FiltroBusquedas.Remove(filtroBusqueda);
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
