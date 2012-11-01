namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForceFit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ruta_Usuario", "Duracion", c => c.Time(nullable: false));
            AddColumn("dbo.Ruta_Usuario", "Fit", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ruta_Usuario", "Fit");
            DropColumn("dbo.Ruta_Usuario", "Duracion");
        }
    }
}
