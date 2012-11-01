namespace Financy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustTipo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tipoes", "Descripcion", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tipoes", "Descripcion", c => c.Int(nullable: false));
        }
    }
}
