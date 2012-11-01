namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ago02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ruta_Usuario", "Hora_Partida", c => c.Time(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ruta_Usuario", "Hora_Partida");
        }
    }
}
