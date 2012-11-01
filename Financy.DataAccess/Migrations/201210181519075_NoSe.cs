namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Spatial;
    
    public partial class NoSe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rutas", "Geom", c => c.Geometry());
            DropColumn("dbo.Rutas", "Way");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rutas", "Way", c => c.String());
            DropColumn("dbo.Rutas", "Geom");
        }
    }
}
