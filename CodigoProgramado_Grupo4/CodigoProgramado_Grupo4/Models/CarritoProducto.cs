using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodigoProgramado_Grupo4.Models
{
    public class CarritoProducto
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public virtual Usuario Usuarios { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
        public int CarritoId { get; set; }
        public virtual Carrito Carritos { get; set; }

    }
}