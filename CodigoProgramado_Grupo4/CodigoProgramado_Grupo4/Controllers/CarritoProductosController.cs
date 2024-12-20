﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodigoProgramado_Grupo4.Filters;
using CodigoProgramado_Grupo4.Models;

namespace CodigoProgramado_Grupo4.Controllers
{
    [AllowAnonymous]
    public class CarritoProductosController : Controller
    {
        private UsuarioPedidosDbContext db = new UsuarioPedidosDbContext();

        // GET: CarritoProductos
        public ActionResult Index()
        {
            try
            {
                var user = Session["User"] as Usuario;

                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                var carritoProductos = db.CarritoProductos.Include(cp => cp.Productos).ToList();

                // Asegurarse de inicializar ViewBag.Total aunque no haya productos
                var total = carritoProductos.Any()
                    ? carritoProductos.Sum(cp => cp.Productos.Sum(p => p.Precio) * cp.Cantidad)
                    : 0;

                ViewBag.Total = total;

                return View(carritoProductos);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en ListaDeseosController.Index: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
            
        }

        public ActionResult ErrorDescuento()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CanjearCodigo(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return RedirectToAction("ErrorDescuento");
            }

            var verifyCode = db.CodigoDescuentos.FirstOrDefault(c => c.codigo == code);
            if (verifyCode != null)
            {
                // Obtener el usuario actual desde la sesión
                var userId = ((Usuario)Session["User"]).Id; // Obtén el ID del usuario actual desde la sesión
                var user = db.Usuarios.Find(userId); // Carga el usuario desde el contexto actual

                if (user == null)
                {
                    return RedirectToAction("~/Account/RegisterError");
                }

                
                // Obtener el carrito relacionado con el usuario actual
                var carrito = db.Carritos
                    .Include(c => c.CarritoProductos.Select(cp => cp.Productos))
                    .FirstOrDefault(c => c.Pedidos.UsuarioId == user.Id);

                if (carrito == null)
                {
                    return RedirectToAction("ErrorDescuento");
                }

                // Calcular el total del carrito
                var totalCarrito = carrito.CarritoProductos.Sum(cp =>
                    cp.Productos.Sum(p => p.Precio * cp.Cantidad));

                // Calcular el descuento
                var descuento = verifyCode.valor_descuento / 100.0;
                var totalConDescuento = totalCarrito - (totalCarrito * descuento);

                // Pasar el total con descuento a la vista
                ViewBag.TotalConDescuento = totalConDescuento;

                return View("Index", carrito.CarritoProductos.ToList());
            }

            return RedirectToAction("ErrorDescuento");
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
            ViewBag.Productos = new SelectList(db.Productos.Where(p => p.disponibilidadInventario), "Id", "NombreProducto");
            ViewBag.Carritos = new SelectList(db.Carritos, "Id", "Id");

            return View();
        }

        // POST: CarritoProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cantidad,CarritoId,ProductoId")] CarritoProducto carritoProducto, int ProductoId)
        {
            if (ModelState.IsValid)
            {
                // Obtener el producto seleccionado
                var producto = db.Productos.Find(ProductoId);

                if (producto != null && producto.disponibilidadInventario)
                {
                    carritoProducto.Productos = new List<Producto> { producto };
                    var userId = ((Usuario)Session["User"]).Id; // Obtén el ID del usuario actual desde la sesión
                    var user = db.Usuarios.Find(userId); // Carga el usuario desde el contexto actual

                    if (user == null)
                    {
                        return RedirectToAction("~/Account/RegisterError");
                    }
                    
                    db.Pedidos.Add(new Pedido
                    {
                        fecha = DateTime.Today, // Fecha del pedido
                        estado = true,          // Por ejemplo, indicando que el pedido está activo
                        UsuarioId = user.Id,        // Reemplaza con el ID de un usuario existente
                    });

                    carritoProducto.Usuarios = user;

                    db.CarritoProductos.Add(carritoProducto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "El producto seleccionado no está disponible.");
            }

            // Re-llenar listas en caso de error
            ViewBag.Productos = new SelectList(db.Productos.Where(p => p.disponibilidadInventario), "Id", "NombreProducto");
            ViewBag.Carritos = new SelectList(db.Carritos, "Id", "Id");

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

        [HttpPost]
        public ActionResult AgregarProducto(int productoId, int cantidad, int carritoId)
        {
            if (cantidad <= 0)
            {
                return RedirectToAction("Index", new { error = "La cantidad debe ser mayor a cero." });
            }

            var producto = db.Productos.Find(productoId);
            if (producto == null || !producto.disponibilidadInventario || !producto.estado)
            {
                return RedirectToAction("Index", new { error = "Producto no disponible." });
            }

            //var carrito = db.Carritos.Find(productoId);


            // Crear o actualizar un CarritoProducto
            var carritoProducto = db.CarritoProductos
                .FirstOrDefault(cp => cp.Productos.Any(p => p.Id == producto.Id) && cp.CarritoId == 1); // Reemplaza "1" por el ID del carrito actual

            if (carritoProducto == null)
            {
                carritoProducto = new CarritoProducto
                {
                    CarritoId = 1, // Reemplazar con la lógica adecuada para obtener el carrito del usuario actual
                    Cantidad = cantidad,
                    Productos = new List<Producto> { producto }
                };
                db.CarritoProductos.Add(carritoProducto);
            }
            else
            {
                carritoProducto.Cantidad += cantidad;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult MostrarCarrito()
        {
            var carritoProductos = db.CarritoProductos
                .Where(cp => cp.CarritoId == 1) // Reemplazar con la lógica para identificar el carrito actual
                .Include(cp => cp.Productos)
                .ToList();

            var total = carritoProductos.Sum(cp => cp.Productos.Sum(p => p.Precio) * cp.Cantidad);

            ViewBag.Total = total;

            return View(carritoProductos);
        }

        [HttpPost]
        public ActionResult EliminarProducto(int productoId)
        {
            var carritoProducto = db.CarritoProductos
                .FirstOrDefault(cp => cp.Productos.Any(p => p.Id == productoId) && cp.CarritoId == 1);

            if (carritoProducto != null)
            {
                db.CarritoProductos.Remove(carritoProducto);
                db.SaveChanges();
            }

            return RedirectToAction("MostrarCarrito");
        }

        public ActionResult PagoRealizado()
        {
           
            var carritoProductos = db.CarritoProductos;
            var userId = ((Usuario)Session["User"]).Id; // Obtén el ID del usuario actual desde la sesión
            var user = db.Usuarios.Find(userId); // Carga el usuario desde el contexto actual

            foreach (var CP in carritoProductos) {
                if (CP.Usuarios.Id == user.Id)
                {
                    db.CarritoProductos.Remove(CP);
                    
                }

            }

            db.SaveChanges();

            ViewBag.Mensaje = "El pago se realizó con éxito.";
            return View();
        }



    }
}