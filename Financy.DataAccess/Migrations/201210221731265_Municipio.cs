namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Municipio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Direccions", "Municipio_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Direccions", "Municipio_Id");
        }
    }
}
