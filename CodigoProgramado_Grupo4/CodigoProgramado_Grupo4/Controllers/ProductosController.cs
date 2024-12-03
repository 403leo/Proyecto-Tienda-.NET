using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodigoProgramado_Grupo4.Filters;
using CodigoProgramado_Grupo4.Models;

namespace CodigoProgramado_Grupo4.Controllers
{
    [CustomAuthorizationFilter("Admin")]
    public class ProductosController : Controller
    {
        private UsuarioPedidosDbContext db = new UsuarioPedidosDbContext();

        // GET: Productos
        public ActionResult Index()
        {
            return View(db.Productos.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodigoProducto,NombreProducto,Precio,disponibilidadInventario,estado")] Producto producto, HttpPostedFileBase Imagen, HttpPostedFileBase Imagen2, HttpPostedFileBase Imagen3)
        {
            if (ModelState.IsValid)
            {
                if (Imagen != null && Imagen.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(Imagen.InputStream))
                    {
                        producto.Imagen = binaryReader.ReadBytes(Imagen.ContentLength);
                    }
                }

                if (Imagen2 != null && Imagen2.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(Imagen2.InputStream))
                    {
                        producto.Imagen2 = binaryReader.ReadBytes(Imagen2.ContentLength);
                    }
                }

                if (Imagen3 != null && Imagen3.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(Imagen3.InputStream))
                    {
                        producto.Imagen3 = binaryReader.ReadBytes(Imagen3.ContentLength);
                    }
                }

                db.Productos.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodigoProducto,NombreProducto,Precio,disponibilidadInventario,estado")] Producto producto, HttpPostedFileBase Imagen, HttpPostedFileBase Imagen2, HttpPostedFileBase Imagen3)
        {
            if (ModelState.IsValid)
            {
                if (Imagen != null && Imagen.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(Imagen.InputStream))
                    {
                        producto.Imagen = binaryReader.ReadBytes(Imagen.ContentLength);
                    }
                }

                if (Imagen2 != null && Imagen2.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(Imagen2.InputStream))
                    {
                        producto.Imagen2 = binaryReader.ReadBytes(Imagen2.ContentLength);
                    }
                }

                if (Imagen3 != null && Imagen3.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(Imagen3.InputStream))
                    {
                        producto.Imagen3 = binaryReader.ReadBytes(Imagen3.ContentLength);
                    }
                }

                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);
            db.Productos.Remove(producto);
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
