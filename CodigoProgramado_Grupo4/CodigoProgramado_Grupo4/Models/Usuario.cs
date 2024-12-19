using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Threading;
using System.Web;

namespace CodigoProgramado_Grupo4.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime ultimaFechaConexion {  get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Estado { get; set; }
        public string Role { get; set; }
        public bool isAuthenticated { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
        public virtual ICollection<Suscripcion> Suscripciones { get; set; }
        public virtual ICollection<Resena> Resenas { get; set; }
        public virtual ICollection<ListaDeseos> ListaDeseos { get; set; }
        public virtual ICollection<CarritoProducto> CarritoProductos { get; set; }
    }
}