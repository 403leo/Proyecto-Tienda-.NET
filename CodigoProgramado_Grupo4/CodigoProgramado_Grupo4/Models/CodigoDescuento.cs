using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodigoProgramado_Grupo4.Models
{
    public class CodigoDescuento
    {
        public int Id { get; set; }
        public String codigo { get; set; }
        public float valor_descuento { get; set; }
        public DateTime fecha_expiracion { get; set; }
        public int ProductoId { get; set; }
        public virtual Producto Productos { get; set; }
    }
}