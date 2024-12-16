namespace CodigoProgramado_Grupo4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtributoImagenProductoAgregado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productoes", "Imagen", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productoes", "Imagen");
        }
    }
}
