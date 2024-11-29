using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodigoProgramado_Grupo4.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public virtual Pedido Pedidos { get; set; }
        public virtual ICollection<CarritoProducto> CarritoProductos { get; set; }
    }
}