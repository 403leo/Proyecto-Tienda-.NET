using CodigoProgramado_Grupo4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.UI;

namespace CodigoProgramado_Grupo4.Controllers
{
    public class HomeController : Controller
    {
        private UsuarioPedidosDbContext db = new UsuarioPedidosDbContext();

        

        // Controlador del index que gestiona la vista principal de productos con el filtro de busqueda
        // para categorias, disponibilidad y rango de precios.
        public ActionResult Index(string searchString, string categoria, double? precioMin, double? precioMax, bool? disponibilidad, int page = 1, int pageSize = 10)
        {
            
            // Se consulta los productos a la base de datos 
            var productos = db.Productos.AsQueryable();

            // Se busca el producto cuyo nombre contenga el texto de busqueda
            if (!string.IsNullOrEmpty(searchString))
            {
                productos = productos.Where(p => p.NombreProducto.Contains(searchString));
            }
            // Se filtra productos por categoria
            if (!string.IsNullOrEmpty(categoria))
            {
                productos = productos.Where(p => p.Categoria == categoria);
            }

            // Se filtra productos cuyo precio sea mayor o igual al precio minimo especificado
            if (precioMin.HasValue)
            {
                productos = productos.Where(p => p.Precio >= precioMin.Value);
            }

            // Se filtra productos cuyo precio sea menor o igual al precio maximo especificado
            if (precioMax.HasValue)
            {
                productos = productos.Where(p => p.Precio <= precioMax.Value);
            }

            // Se filtra productos por disponibilidad en el inventario
            if (disponibilidad.HasValue)
            {
                productos = productos.Where(p => p.disponibilidadInventario == disponibilidad.Value);
            }

            // Se obtiene el total de elementos despues de aplicar los filtros
            var totalItems = productos.Count();
            
            //Paginacion: Se ordena los productos por nombre, se omite los elementos previos y toma solo los correspondientes a la pagina actual
            var productosPaginados = productos
                .OrderBy(p => p.NombreProducto)// Se ordena alfabeticamente por nombre del producto
                .Skip((page - 1) * pageSize) //  Se omite elementos de las pagina anteriores
                .Take(pageSize) // Se toma solo los elementos de la pagina actual
                .ToList();

            // Se pasa datos relevantes a la vista mediante ViewBag para construir la interfaz del usuario
            ViewBag.CurrentPage = page; // Numero de pagina actual
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize); // Total de paginas basado en el tamano de pagina
            ViewBag.CurrentSearch = searchString; // Texto de busqueda actual
            ViewBag.CurrentCategoria = categoria; // Categoria seleccionada actualmente
            ViewBag.CurrentPrecioMin = precioMin; // Precio minimo actual
            ViewBag.CurrentPrecioMax = precioMax; // Precio maximo actual
            ViewBag.CurrentDisponibilidad = disponibilidad; // Disponibilidad seleccionada actualmente

            // Retorna la vista con la lista de productos paginados
            return View(productosPaginados);
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