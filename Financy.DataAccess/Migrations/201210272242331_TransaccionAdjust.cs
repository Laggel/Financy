namespace Financy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransaccionAdjust : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transaccions", "EntradaProyectada_TransaccionProyectada_Id", "dbo.TransaccionProyectadas");
            DropIndex("dbo.Transaccions", new[] { "EntradaProyectada_TransaccionProyectada_Id" });
            AlterColumn("dbo.Transaccions", "TransaccionProyectada_Id", c => c.Int(nullable: false));
            AddForeignKey("dbo.Transaccions", "TransaccionProyectada_Id", "dbo.TransaccionProyectadas", "TransaccionProyectada_Id");
            CreateIndex("dbo.Transaccions", "TransaccionProyectada_Id");
            DropColumn("dbo.Transaccions", "EntradaProyectada_TransaccionProyectada_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transaccions", "EntradaProyectada_TransaccionProyectada_Id", c => c.Int());
            DropIndex("dbo.Transaccions", new[] { "TransaccionProyectada_Id" });
            DropForeignKey("dbo.Transaccions", "TransaccionProyectada_Id", "dbo.TransaccionProyectadas");
            AlterColumn("dbo.Transaccions", "TransaccionProyectada_Id", c => c.String());
            CreateIndex("dbo.Transaccions", "EntradaProyectada_TransaccionProyectada_Id");
            AddForeignKey("dbo.Transaccions", "EntradaProyectada_TransaccionProyectada_Id", "dbo.TransaccionProyectadas", "TransaccionProyectada_Id");
        }
    }
}
