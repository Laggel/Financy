namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Hora_Llegada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rutas", "Hora_Llegada", c => c.Time(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rutas", "Hora_Llegada");
        }
    }
}
