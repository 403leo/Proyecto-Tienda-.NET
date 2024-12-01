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
    public class CarritoProductosController : Controller
    {
        private UsuarioPedidosDbContext db = new UsuarioPedidosDbContext();

        // GET: CarritoProductos
        public ActionResult Index()
        {
            var carritoProductos = db.CarritoProductos.Include(c => c.Carritos);
            return View(carritoProductos.ToList());
        }

        // GET: CarritoProductos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarritoProducto carritoProducto = db.CarritoProductos.Find(id);
            if (carritoProducto == null)
            {
                return HttpNotFound();
            }
            return View(carritoProducto);
        }

        // GET: CarritoProductos/Create
        public ActionResult Create()
        {
            ViewBag.CarritoId = new SelectList(db.Carritos, "Id", "Id");
            return View();
        }

        // POST: CarritoProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cantidad,CarritoId")] CarritoProducto carritoProducto)
        {
            if (ModelState.IsValid)
            {
                db.CarritoProductos.Add(carritoProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarritoId = new SelectList(db.Carritos, "Id", "Id", carritoProducto.CarritoId);
            return View(carritoProducto);
        }

        // GET: CarritoProductos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarritoProducto carritoProducto = db.CarritoProductos.Find(id);
            if (carritoProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarritoId = new SelectList(db.Carritos, "Id", "Id", carritoProducto.CarritoId);
            return View(carritoProducto);
        }

        // POST: CarritoProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cantidad,CarritoId")] CarritoProducto carritoProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carritoProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarritoId = new SelectList(db.Carritos, "Id", "Id", carritoProducto.CarritoId);
            return View(carritoProducto);
        }

        // GET: CarritoProductos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarritoProducto carritoProducto = db.CarritoProductos.Find(id);
            if (carritoProducto == null)
            {
                return HttpNotFound();
            }
            return View(carritoProducto);
        }

        // POST: CarritoProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarritoProducto carritoProducto = db.CarritoProductos.Find(id);
            db.CarritoProductos.Remove(carritoProducto);
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
