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
    public class CarritoesController : Controller
    {
        private UsuarioPedidosDbContext db = new UsuarioPedidosDbContext();

        // GET: Carritoes
        public ActionResult Index()
        {
            var carritos = db.Carritos.Include(c => c.Pedidos);
            return View(carritos.ToList());
        }

        // GET: Carritoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrito carrito = db.Carritos.Find(id);
            if (carrito == null)
            {
                return HttpNotFound();
            }
            return View(carrito);
        }

        // GET: Carritoes/Create
        public ActionResult Create()
        {
            ViewBag.PedidoId = new SelectList(db.Pedidos, "Id", "Id");
            return View();
        }

        // POST: Carritoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PedidoId")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                db.Carritos.Add(carrito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PedidoId = new SelectList(db.Pedidos, "Id", "Id", carrito.PedidoId);
            return View(carrito);
        }

        // GET: Carritoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrito carrito = db.Carritos.Find(id);
            if (carrito == null)
            {
                return HttpNotFound();
            }
            ViewBag.PedidoId = new SelectList(db.Pedidos, "Id", "Id", carrito.PedidoId);
            return View(carrito);
        }

        // POST: Carritoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PedidoId")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carrito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PedidoId = new SelectList(db.Pedidos, "Id", "Id", carrito.PedidoId);
            return View(carrito);
        }

        // GET: Carritoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrito carrito = db.Carritos.Find(id);
            if (carrito == null)
            {
                return HttpNotFound();
            }
            return View(carrito);
        }

        // POST: Carritoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carrito carrito = db.Carritos.Find(id);
            db.Carritos.Remove(carrito);
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
