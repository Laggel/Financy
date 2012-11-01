namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schedule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Schedule_Id = c.Int(nullable: false, identity: true),
                        Geom = c.Geometry(),
                        Hora = c.Time(nullable: false),
                    })
                .PrimaryKey(t => t.Schedule_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Schedules");
        }
    }
}
