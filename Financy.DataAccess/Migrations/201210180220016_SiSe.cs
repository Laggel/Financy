namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Spatial;
    
    public partial class SiSe : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Schedules", new[] { "Ruta_Id" });
            DropForeignKey("dbo.Schedules", "Ruta_Id", "dbo.Rutas");
            DropColumn("dbo.Ruta_Usuario", "Hora_Llegada");
            DropColumn("dbo.Calle_Interseccion", "Way");
            DropColumn("dbo.Ensanches", "Geom");
            DropTable("dbo.Schedules");
        }
    }
}
