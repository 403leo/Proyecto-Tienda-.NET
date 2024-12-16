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
    public class ResenasController : Controller
    {
        private UsuarioPedidosDbContext db = new UsuarioPedidosDbContext();

        // GET: Resenas
        public ActionResult Index()
        {
            var resenas = db.Resenas.Include(r => r.Productos).Include(r => r.Usuarios);
            return View(resenas.ToList());
        }

        // GET: Resenas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resena resena = db.Resenas.Find(id);
            if (resena == null)
            {
                return HttpNotFound();
            }
            return View(resena);
        }

        // GET: Resenas/Create
        public ActionResult Create()
        {
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto");
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Resenas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,comentario,UsuarioId,ProductoId")] Resena resena)
        {

            if (ModelState.IsValid)
            {
                db.Resenas.Add(resena);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto", resena.ProductoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", resena.UsuarioId);
            return View(resena);
        }

        // GET: Resenas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resena resena = db.Resenas.Find(id);
            if (resena == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto", resena.ProductoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", resena.UsuarioId);
            return View(resena);
        }

        // POST: Resenas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,comentario,UsuarioId,ProductoId")] Resena resena)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resena).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "CodigoProducto", resena.ProductoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", resena.UsuarioId);
            return View(resena);
        }

        // GET: Resenas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resena resena = db.Resenas.Find(id);
            if (resena == null)
            {
                return HttpNotFound();
            }
            return View(resena);
        }

        // POST: Resenas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resena resena = db.Resenas.Find(id);
            db.Resenas.Remove(resena);
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
