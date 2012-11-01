namespace Financy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c20 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Cuentas", "Balanc", c => c.Double(nullable: false));
            AddColumn("dbo.Tipoes", "Cantidad", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tipoes", "Cantidad");
            //DropColumn("dbo.Cuentas", "Balanc");
        }
    }
}
