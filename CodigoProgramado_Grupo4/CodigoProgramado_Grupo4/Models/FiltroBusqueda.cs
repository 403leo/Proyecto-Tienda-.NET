using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodigoProgramado_Grupo4.Models
{
    public class FiltroBusqueda
    {
        public int Id{ get; set; }
        public string Categoria { get; set; }
        public string rango_precios { get; set; }
        public bool disponibilidad { get; set; }
        public int ProductoId { get; set; }
        public virtual Producto Productos { get; set; }
    }
}