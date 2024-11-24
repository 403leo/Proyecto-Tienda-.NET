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
    }
}