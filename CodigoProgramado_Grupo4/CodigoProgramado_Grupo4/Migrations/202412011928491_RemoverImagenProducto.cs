namespace CodigoProgramado_Grupo4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoverImagenProducto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarritoProductoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        CarritoId = c.Int(nullable: false),
                        Usuarios_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carritoes", t => t.CarritoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.Usuarios_Id)
                .Index(t => t.CarritoId)
                .Index(t => t.Usuarios_Id);
            
            CreateTable(
                "dbo.Carritoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pedidoes", t => t.PedidoId, cascadeDelete: true)
                .Index(t => t.PedidoId);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        fecha = c.DateTime(nullable: false),
                        estado = c.Boolean(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellidos = c.String(),
                        ultimaFechaConexion = c.DateTime(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        Estado = c.Boolean(nullable: false),
                        Role = c.String(),
                        isAuthenticated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ListaDeseos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Productoes", t => t.ProductoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodigoProducto = c.String(),
                        NombreProducto = c.String(),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        disponibilidadInventario = c.Boolean(nullable: false),
                        estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Catalogoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Productoes", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.CodigoDescuentoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        codigo = c.String(),
                        valor_descuento = c.Single(nullable: false),
                        fecha_expiracion = c.DateTime(nullable: false),
                        ProductoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Productoes", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.FiltroBusquedas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Categoria = c.String(),
                        rango_precios = c.String(),
                        disponibilidad = c.Boolean(nullable: false),
                        ProductoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Productoes", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.Resenas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        comentario = c.String(),
                        UsuarioId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Productoes", t => t.ProductoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.Suscripcions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo_subs = c.String(),
                        Tipo_subs = c.String(),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.ProductoCarritoProductoes",
                c => new
                    {
                        Producto_Id = c.Int(nullable: false),
                        CarritoProducto_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Producto_Id, t.CarritoProducto_Id })
                .ForeignKey("dbo.Productoes", t => t.Producto_Id, cascadeDelete: true)
                .ForeignKey("dbo.CarritoProductoes", t => t.CarritoProducto_Id, cascadeDelete: true)
                .Index(t => t.Producto_Id)
                .Index(t => t.CarritoProducto_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarritoProductoes", "Usuarios_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Suscripcions", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Pedidoes", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.ListaDeseos", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Resenas", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Resenas", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.ListaDeseos", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.FiltroBusquedas", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.CodigoDescuentoes", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.Catalogoes", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.ProductoCarritoProductoes", "CarritoProducto_Id", "dbo.CarritoProductoes");
            DropForeignKey("dbo.ProductoCarritoProductoes", "Producto_Id", "dbo.Productoes");
            DropForeignKey("dbo.Carritoes", "PedidoId", "dbo.Pedidoes");
            DropForeignKey("dbo.CarritoProductoes", "CarritoId", "dbo.Carritoes");
            DropIndex("dbo.ProductoCarritoProductoes", new[] { "CarritoProducto_Id" });
            DropIndex("dbo.ProductoCarritoProductoes", new[] { "Producto_Id" });
            DropIndex("dbo.Suscripcions", new[] { "UsuarioId" });
            DropIndex("dbo.Resenas", new[] { "ProductoId" });
            DropIndex("dbo.Resenas", new[] { "UsuarioId" });
            DropIndex("dbo.FiltroBusquedas", new[] { "ProductoId" });
            DropIndex("dbo.CodigoDescuentoes", new[] { "ProductoId" });
            DropIndex("dbo.Catalogoes", new[] { "ProductoId" });
            DropIndex("dbo.ListaDeseos", new[] { "ProductoId" });
            DropIndex("dbo.ListaDeseos", new[] { "UsuarioId" });
            DropIndex("dbo.Pedidoes", new[] { "UsuarioId" });
            DropIndex("dbo.Carritoes", new[] { "PedidoId" });
            DropIndex("dbo.CarritoProductoes", new[] { "Usuarios_Id" });
            DropIndex("dbo.CarritoProductoes", new[] { "CarritoId" });
            DropTable("dbo.ProductoCarritoProductoes");
            DropTable("dbo.Suscripcions");
            DropTable("dbo.Resenas");
            DropTable("dbo.FiltroBusquedas");
            DropTable("dbo.CodigoDescuentoes");
            DropTable("dbo.Catalogoes");
            DropTable("dbo.Productoes");
            DropTable("dbo.ListaDeseos");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.Carritoes");
            DropTable("dbo.CarritoProductoes");
        }
    }
}
