using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;

namespace CodigoProgramado_Grupo4.Models
{
    public class UsuarioPedidosDbContext : DbContext
    {
        public UsuarioPedidosDbContext() : base("ProyectoTiendaConexion") { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoProducto> CarritoProductos { get; set; }
        public DbSet<Catalogo> Catalogos { get; set; }
        public DbSet<CodigoDescuento> CodigoDescuentos { get; set; }
        public DbSet<FiltroBusqueda> FiltroBusquedas { get; set; }
        public DbSet<ListaDeseos> ListaDeseos { get; set; }
        public DbSet<Resena> Resenas { get; set; }
        public DbSet<Suscripcion> Suscripciones { get; set; }


    }
}