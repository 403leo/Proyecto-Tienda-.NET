namespace CodigoProgramado_Grupo4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtributoRestante : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carritoes", "Pedidos_Id", "dbo.Pedidoes");
            DropIndex("dbo.Carritoes", new[] { "Pedidos_Id" });
            RenameColumn(table: "dbo.Carritoes", name: "Pedidos_Id", newName: "PedidoId");
            AlterColumn("dbo.Carritoes", "PedidoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carritoes", "PedidoId");
            AddForeignKey("dbo.Carritoes", "PedidoId", "dbo.Pedidoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carritoes", "PedidoId", "dbo.Pedidoes");
            DropIndex("dbo.Carritoes", new[] { "PedidoId" });
            AlterColumn("dbo.Carritoes", "PedidoId", c => c.Int());
            RenameColumn(table: "dbo.Carritoes", name: "PedidoId", newName: "Pedidos_Id");
            CreateIndex("dbo.Carritoes", "Pedidos_Id");
            AddForeignKey("dbo.Carritoes", "Pedidos_Id", "dbo.Pedidoes", "Id");
        }
    }
}
