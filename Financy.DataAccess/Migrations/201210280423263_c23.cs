namespace Financy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c23 : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.UserProfile",
            //    c => new
            //        {
            //            UserId = c.Int(nullable: false, identity: true),
            //            UserName = c.String(),
            //        })
            //    .PrimaryKey(t => t.UserId);
            
            //AddColumn("dbo.Cuentas", "Balanc", c => c.Double(nullable: false));
            AddColumn("dbo.Cuentas", "UserId", c => c.Int(nullable: false));
            //AddColumn("dbo.Tipoes", "Cantidad", c => c.Int(nullable: false));
            //AddColumn("dbo.Tipoes", "Legend", c => c.String());
            //AddForeignKey("dbo.Cuentas", "UserId", "dbo.UserProfile", "UserId");
            CreateIndex("dbo.Cuentas", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Cuentas", new[] { "UserId" });
            DropForeignKey("dbo.Cuentas", "UserId", "dbo.UserProfile");
            DropColumn("dbo.Tipoes", "Legend");
            DropColumn("dbo.Tipoes", "Cantidad");
            DropColumn("dbo.Cuentas", "UserId");
            DropColumn("dbo.Cuentas", "Balanc");
            DropTable("dbo.UserProfile");
        }
    }
}
