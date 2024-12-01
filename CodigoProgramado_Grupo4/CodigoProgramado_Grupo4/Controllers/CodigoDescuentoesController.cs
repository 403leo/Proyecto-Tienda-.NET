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
    public class CodigoDescuentoesController : Controller
    {
        private UsuarioPedidosDbContext db = new UsuarioPedidosDbContext();

        // GET: CodigoDescuentoes
        public ActionResult Index()
        {
            var codigoDescuentos = db.CodigoDescuentos.Include(c => c.Productos);
            return View(codigoDescuentos.ToList());
        }

        // GET: CodigoDescuentoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodigoDescuento codigoDescuento = db.CodigoDescuentos.Find(id);
            if (codigoDescuento == null)
            {
                return HttpNotFound();
            }
            return View(codigoDescuento);
        }

        // GET: CodigoDescuentoes/Create
        public ActionResult Create()
        {
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto");
            return View();
        }

        // POST: CodigoDescuentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,codigo,valor_descuento,fecha_expiracion,ProductoId")] CodigoDescuento codigoDescuento)
        {
            if (ModelState.IsValid)
            {
                db.CodigoDescuentos.Add(codigoDescuento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto", codigoDescuento.ProductoId);
            return View(codigoDescuento);
        }

        // GET: CodigoDescuentoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodigoDescuento codigoDescuento = db.CodigoDescuentos.Find(id);
            if (codigoDescuento == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto", codigoDescuento.ProductoId);
            return View(codigoDescuento);
        }

        // POST: CodigoDescuentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,codigo,valor_descuento,fecha_expiracion,ProductoId")] CodigoDescuento codigoDescuento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codigoDescuento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto", codigoDescuento.ProductoId);
            return View(codigoDescuento);
        }

        // GET: CodigoDescuentoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodigoDescuento codigoDescuento = db.CodigoDescuentos.Find(id);
            if (codigoDescuento == null)
            {
                return HttpNotFound();
            }
            return View(codigoDescuento);
        }

        // POST: CodigoDescuentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodigoDescuento codigoDescuento = db.CodigoDescuentos.Find(id);
            db.CodigoDescuentos.Remove(codigoDescuento);
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
