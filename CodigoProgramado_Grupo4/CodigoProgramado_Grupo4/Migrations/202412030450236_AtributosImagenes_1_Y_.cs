namespace CodigoProgramado_Grupo4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtributosImagenes_1_Y_ : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productoes", "Imagen2", c => c.Binary());
            AddColumn("dbo.Productoes", "Imagen3", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productoes", "Imagen3");
            DropColumn("dbo.Productoes", "Imagen2");
        }
    }
}
