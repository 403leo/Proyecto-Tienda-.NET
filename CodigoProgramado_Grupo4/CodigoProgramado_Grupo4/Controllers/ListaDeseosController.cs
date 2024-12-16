using System;
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
    //[CustomAuthorizationFilter("Admin")]
    [Authorize]
    public class ListaDeseosController : Controller
    {
        private UsuarioPedidosDbContext db = new UsuarioPedidosDbContext();

        // GET: ListaDeseos
        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                var user = Session["User"] as Usuario;

                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Verificar el rol directamente desde la sesión
                string rolUsuario = user.Role == "Admin" ? "Admin" : "User";

                ViewBag.RolUsuario = rolUsuario;

                var listaDeseos = db.ListaDeseos.Include(l => l.Productos).Include(l => l.Usuarios);

                // Si es usuario normal, filtrar su lista de deseos
                if (rolUsuario == "User")
                {
                    int usuarioActualId = user.Id;
                    listaDeseos = listaDeseos.Where(l => l.UsuarioId == usuarioActualId);
                }

                // Si es admin y la sesión tiene "VerTodas"
                if (Session["VerTodas"] != null && (bool)Session["VerTodas"])
                {
                    ViewBag.VerTodas = true;
                }
                else
                {
                    ViewBag.VerTodas = false;

                    // Mostrar solo sus listas si no se ha activado "VerTodas"
                    if (rolUsuario == "Admin")
                    {
                        int usuarioActualId = user.Id;
                        listaDeseos = listaDeseos.Where(l => l.UsuarioId == usuarioActualId);
                    }
                }

                return View(listaDeseos.ToList());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en ListaDeseosController.Index: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }


        // Acción para cambiar el modo del administrador
        [AllowAnonymous]
        public ActionResult CambiarModo()
        {
            bool verTodas = (bool)(Session["VerTodas"] ?? false);
            Session["VerTodas"] = !verTodas;

            return RedirectToAction("Index");
        }





        // GET: ListaDeseos/Details/5
        [AllowAnonymous]
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
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "NombreProducto");
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: ListaDeseos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "Id,UsuarioId,ProductoId")] ListaDeseos listaDeseos)
        {
            if (ModelState.IsValid)
            {
                db.ListaDeseos.Add(listaDeseos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "NombreProducto", listaDeseos.ProductoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", listaDeseos.UsuarioId);
            return View(listaDeseos);
        }

        // GET: ListaDeseos/Edit/5
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
