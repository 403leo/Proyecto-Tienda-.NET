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
using System.Diagnostics;

namespace CodigoProgramado_Grupo4.Controllers
{
    
    public class ProductosController : Controller
    {
        private UsuarioPedidosDbContext db = new UsuarioPedidosDbContext();

        // GET: Productos
        [CustomAuthorizationFilter("Admin")]
        public ActionResult Index()
        {
            return View(db.Productos.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                Producto producto = db.Productos.Include(p => p.Resenas).FirstOrDefault(p => p.Id == id);

                var user = Session["User"] as Usuario;

                if (user == null) 
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    if (producto == null)
                    {
                        return HttpNotFound();
                    }

                    // Se pasa el id del producto para que este disponible en la vistas parciales ( es decir en el _Resenas)
                    ViewBag.ProductoId = id;
                    return View(producto);
                }

                // Se Verificaa el rol directamente desde la sesión
                string rolUsuario = user.Role == "Admin" ? "Admin" : "User";


                ViewBag.RolUsuario = rolUsuario;
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (producto == null)
                {
                    return HttpNotFound();
                }

                // Se pasa el id del producto para que este disponible en la vistas parciales ( es decir en el _Resenas)
                ViewBag.ProductoId = id;
                return View(producto);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en ListaDeseosController.Index: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }

            
        }

        // GET: Productos/Create
        [CustomAuthorizationFilter("Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizationFilter("Admin")]
        public ActionResult Create([Bind(Include = "Id,CodigoProducto,NombreProducto,Precio,disponibilidadInventario,estado,Categoria")] Producto producto, HttpPostedFileBase Imagen, HttpPostedFileBase Imagen2, HttpPostedFileBase Imagen3)
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
        [CustomAuthorizationFilter("Admin")]
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
        [CustomAuthorizationFilter("Admin")]
        public ActionResult Edit([Bind(Include = "Id,CodigoProducto,NombreProducto,Precio,disponibilidadInventario,estado,Categoria")] Producto producto, HttpPostedFileBase Imagen, HttpPostedFileBase Imagen2, HttpPostedFileBase Imagen3)
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
        [CustomAuthorizationFilter("Admin")]
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
        [CustomAuthorizationFilter("Admin")]
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
            var usuarioActual = (Usuario)Session["User"];

            if (usuarioActual == null || !usuarioActual.isAuthenticated)
            {
                // Redirige a la página de registro
                TempData["MensajeError"] = "Debes registrarte para agregar productos a favoritos.";
                return RedirectToAction("Login", "Account");
            }

            var productoActual = db.Productos.Find(productId);
            if (productoActual == null)
            {
                return HttpNotFound("Producto no encontrado.");
            }

            var existingWishlistItem = db.ListaDeseos
                .FirstOrDefault(w => w.UsuarioId == usuarioActual.Id && w.ProductoId == productId);

            if (existingWishlistItem != null)
            {
                db.ListaDeseos.Remove(existingWishlistItem);
            }
            else
            {
                var listaDeseos = new ListaDeseos
                {
                    UsuarioId = usuarioActual.Id,
                    ProductoId = productoActual.Id
                };
                db.ListaDeseos.Add(listaDeseos);
            }

            db.SaveChanges();
            return RedirectToAction("Details", "Productos", new { id = productId });
        }

    }
}
