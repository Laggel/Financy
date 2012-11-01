namespace Financy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTipo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tipoes",
                c => new
                    {
                        Tipo_Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Tipo_Id);
            
            AddColumn("dbo.EntradaProyectadas", "Tipo_Id", c => c.Int(nullable: false));
            AddForeignKey("dbo.EntradaProyectadas", "Tipo_Id", "dbo.Tipoes", "Tipo_Id");
            CreateIndex("dbo.EntradaProyectadas", "Tipo_Id");
            DropColumn("dbo.EntradaProyectadas", "Tipo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EntradaProyectadas", "Tipo", c => c.String());
            DropIndex("dbo.EntradaProyectadas", new[] { "Tipo_Id" });
            DropForeignKey("dbo.EntradaProyectadas", "Tipo_Id", "dbo.Tipoes");
            DropColumn("dbo.EntradaProyectadas", "Tipo_Id");
            DropTable("dbo.Tipoes");
        }
    }
}
