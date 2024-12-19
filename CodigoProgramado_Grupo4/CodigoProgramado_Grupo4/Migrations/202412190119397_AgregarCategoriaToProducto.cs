namespace CodigoProgramado_Grupo4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarCategoriaToProducto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productoes", "Categoria", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productoes", "Categoria");
        }
    }
}
