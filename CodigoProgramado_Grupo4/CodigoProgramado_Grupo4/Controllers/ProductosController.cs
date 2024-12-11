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
using System.Web.Mvc.Filters;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListaDeseosPorProducto(int productId)
        {
            // Se obtiene el usuaria actual 

            var usuarioActual = (Usuario)Session["User"];

            // Se verifica que el usuario actual esté autenticado
            if (usuarioActual == null || !usuarioActual.isAuthenticated)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Usuario no autenticado.");
            }

            // Se verifica que el producto existe en la base de datos 
            var productoActual = db.Productos.Find(productId);
            if(productoActual == null) 
            {
                return HttpNotFound("Producto no encontrado.");
            }

            // Se busca si el producto ya existe en la lista de deseos del usuario 

            var existingWishlistItem = db.ListaDeseos.
                FirstOrDefault(w => w.UsuarioId == usuarioActual.Id && w.ProductoId == productId);

            if (existingWishlistItem != null) 
            { 
                // En caso de que el usuario ya haya colocado el producto en su lista de favoritos,
                // al apretar nuevamente el boton de la estrella, se eliminará el producto
                db.ListaDeseos.Remove(existingWishlistItem);
            }
            else 
            {
                // Si no es el caso, el producto se agrega a favoritos.
                var listaDeseos = new ListaDeseos
                {
                    // En esta parte se asigna el usuario y el producto actuales a lista de deseos automaticamente
                    UsuarioId = usuarioActual.Id,
                    ProductoId = productoActual.Id

                };
                db.ListaDeseos.Add(listaDeseos);
            }

            // Se guardan los cambios
            db.SaveChanges();

            // Se redirige a la vista de producto
            return RedirectToAction("Details", "Productos", new { id = productId });

        }



    }
}
