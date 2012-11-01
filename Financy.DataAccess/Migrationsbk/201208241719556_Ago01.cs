namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ago01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "Email", c => c.String(nullable: false));
            DropColumn("dbo.Usuarios", "Correo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "Correo", c => c.String());
            DropColumn("dbo.Usuarios", "Email");
        }
    }
}
