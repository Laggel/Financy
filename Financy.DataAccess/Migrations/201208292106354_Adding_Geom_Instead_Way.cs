namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Spatial;
    
    public partial class Adding_Geom_Instead_Way : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Direccions", "Geom", c => c.Geometry());
            AddColumn("dbo.Solicituds", "Geom", c => c.Geometry());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solicituds", "Geom");
            DropColumn("dbo.Direccions", "Geom");
        }
    }
}
