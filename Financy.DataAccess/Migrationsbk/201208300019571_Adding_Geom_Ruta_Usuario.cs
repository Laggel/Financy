namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Spatial;
    
    public partial class Adding_Geom_Ruta_Usuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ruta_Usuario", "Geom", c => c.Geometry());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ruta_Usuario", "Geom");
        }
    }
}
