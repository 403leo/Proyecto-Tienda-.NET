namespace CodigoProgramado_Grupo4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tareas_Pedidos_Y_Productos : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Productoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodigoProducto = c.String(),
                        NombreProducto = c.String(),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        disponibilidadInventario = c.Boolean(nullable: false),
                        estado = c.Boolean(nullable: false),
                        rutaImagen = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedidoes", "UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.Pedidoes", new[] { "UsuarioId" });
            DropTable("dbo.Productoes");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Pedidoes");
        }
    }
}
