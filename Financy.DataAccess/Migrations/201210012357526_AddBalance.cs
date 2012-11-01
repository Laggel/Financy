namespace Financy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBalance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cuentas", "Balance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cuentas", "Balance");
        }
    }
}
