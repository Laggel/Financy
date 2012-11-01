namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeDeviation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ruta_Usuario", "TimeDeviation", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ruta_Usuario", "TimeDeviation");
        }
    }
}
