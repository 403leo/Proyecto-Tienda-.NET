namespace CodigoProgramado_Grupo4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtributoRestante2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ListaDeseos", "Productos_Id", "dbo.Productoes");
            DropIndex("dbo.ListaDeseos", new[] { "Productos_Id" });
            RenameColumn(table: "dbo.ListaDeseos", name: "Productos_Id", newName: "ProductoId");
            AlterColumn("dbo.ListaDeseos", "ProductoId", c => c.Int(nullable: false));
            CreateIndex("dbo.ListaDeseos", "ProductoId");
            AddForeignKey("dbo.ListaDeseos", "ProductoId", "dbo.Productoes", "Id", cascadeDelete: true);
            DropColumn("dbo.ListaDeseos", "ProdcutoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ListaDeseos", "ProdcutoId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ListaDeseos", "ProductoId", "dbo.Productoes");
            DropIndex("dbo.ListaDeseos", new[] { "ProductoId" });
            AlterColumn("dbo.ListaDeseos", "ProductoId", c => c.Int());
            RenameColumn(table: "dbo.ListaDeseos", name: "ProductoId", newName: "Productos_Id");
            CreateIndex("dbo.ListaDeseos", "Productos_Id");
            AddForeignKey("dbo.ListaDeseos", "Productos_Id", "dbo.Productoes", "Id");
        }
    }
}
