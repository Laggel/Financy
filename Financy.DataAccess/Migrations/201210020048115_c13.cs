namespace Financy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cuentas", "Balanc", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cuentas", "Balanc");
        }
    }
}
