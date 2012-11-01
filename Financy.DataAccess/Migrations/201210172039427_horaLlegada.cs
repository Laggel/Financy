namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class horaLlegada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ruta_Usuario", "Hora_Llegada", c => c.Time(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ruta_Usuario", "Hora_Llegada");
        }
    }
}
