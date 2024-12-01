using CodigoProgramado_Grupo4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodigoProgramado_Grupo4.Controllers
{
    public class HomeController : Controller
    {
        private UsuarioPedidosDbContext db = new UsuarioPedidosDbContext();
        public ActionResult Index()
        {
            // Recuperar los productos principales (filtrar según criterio)
            var principalesProductos = db.Productos
                .Where(p => p.estado && p.disponibilidadInventario) // Solo productos activos con inventario
                .OrderByDescending(p => p.Precio) // Orden por criterio (puedes cambiarlo)
                .Take(5) // Límite de productos
                .ToList();

            return View(principalesProductos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}