namespace Pilot.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class horapartidaForce : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solicituds", "HoraPartidaForce", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solicituds", "HoraPartidaForce");
        }
    }
}
