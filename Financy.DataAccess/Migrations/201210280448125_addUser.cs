namespace Financy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cuentas", "User", c => c.String());
            DropColumn("dbo.Cuentas", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cuentas", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Cuentas", "User");
        }
    }
}
