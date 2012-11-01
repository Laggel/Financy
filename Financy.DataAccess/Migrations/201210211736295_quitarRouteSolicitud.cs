namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quitarRouteSolicitud : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Solicituds", "Ruta_Id", "dbo.Rutas");
            DropIndex("dbo.Solicituds", new[] { "Ruta_Id" });
            DropColumn("dbo.Solicituds", "Ruta_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Solicituds", "Ruta_Id", c => c.Int());
            CreateIndex("dbo.Solicituds", "Ruta_Id");
            AddForeignKey("dbo.Solicituds", "Ruta_Id", "dbo.Rutas", "Ruta_Id");
        }
    }
}
