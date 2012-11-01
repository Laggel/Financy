namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDistances : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Distances",
                c => new
                    {
                        Distance_Id = c.Int(nullable: false, identity: true),
                        Desde = c.Geometry(),
                        Hasta = c.Geometry(),
                        Distancia = c.Double(nullable: false),
                        Tiempo = c.Time(nullable: false),
                    })
                .PrimaryKey(t => t.Distance_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Distances");
        }
    }
}
