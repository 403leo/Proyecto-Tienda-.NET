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
    public class ListaDeseosController : Controller
    {
        private UsuarioPedidosDbContext db = new UsuarioPedidosDbContext();

        // GET: ListaDeseos
        public ActionResult Index()
        {
            var listaDeseos = db.ListaDeseos.Include(l => l.Productos).Include(l => l.Usuarios);
            return View(listaDeseos.ToList());
        }

        // GET: ListaDeseos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaDeseos listaDeseos = db.ListaDeseos.Find(id);
            if (listaDeseos == null)
            {
                return HttpNotFound();
            }
            return View(listaDeseos);
        }

        // GET: ListaDeseos/Create
        public ActionResult Create()
        {
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto");
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: ListaDeseos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UsuarioId,ProductoId")] ListaDeseos listaDeseos)
        {
            if (ModelState.IsValid)
            {
                db.ListaDeseos.Add(listaDeseos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto", listaDeseos.ProductoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", listaDeseos.UsuarioId);
            return View(listaDeseos);
        }

        // GET: ListaDeseos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaDeseos listaDeseos = db.ListaDeseos.Find(id);
            if (listaDeseos == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto", listaDeseos.ProductoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", listaDeseos.UsuarioId);
            return View(listaDeseos);
        }

        // POST: ListaDeseos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UsuarioId,ProductoId")] ListaDeseos listaDeseos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listaDeseos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto", listaDeseos.ProductoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", listaDeseos.UsuarioId);
            return View(listaDeseos);
        }

        // GET: ListaDeseos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaDeseos listaDeseos = db.ListaDeseos.Find(id);
            if (listaDeseos == null)
            {
                return HttpNotFound();
            }
            return View(listaDeseos);
        }

        // POST: ListaDeseos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListaDeseos listaDeseos = db.ListaDeseos.Find(id);
            db.ListaDeseos.Remove(listaDeseos);
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
