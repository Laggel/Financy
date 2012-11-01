namespace Financy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustProyectadas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EntradaProyectadas", "Cuenta_Id", "dbo.Cuentas");
            DropForeignKey("dbo.EntradaProyectadas", "Tipo_Id", "dbo.Tipoes");
            DropForeignKey("dbo.Transaccions", "EntradaProyectada_EntradaProyectada_Id", "dbo.EntradaProyectadas");
            DropIndex("dbo.EntradaProyectadas", new[] { "Cuenta_Id" });
            DropIndex("dbo.EntradaProyectadas", new[] { "Tipo_Id" });
            DropIndex("dbo.Transaccions", new[] { "EntradaProyectada_EntradaProyectada_Id" });
            RenameColumn(table: "dbo.Transaccions", name: "EntradaProyectada_EntradaProyectada_Id", newName: "EntradaProyectada_TransaccionProyectada_Id");
            CreateTable(
                "dbo.TransaccionProyectadas",
                c => new
                    {
                        TransaccionProyectada_Id = c.Int(nullable: false, identity: true),
                        Cuenta_Id = c.Int(nullable: false),
                        Descripcion = c.String(),
                        Monto = c.Double(nullable: false),
                        Tipo_Id = c.Int(nullable: false),
                        Dia = c.Int(),
                    })
                .PrimaryKey(t => t.TransaccionProyectada_Id)
                .ForeignKey("dbo.Cuentas", t => t.Cuenta_Id)
                .ForeignKey("dbo.Tipoes", t => t.Tipo_Id)
                .Index(t => t.Cuenta_Id)
                .Index(t => t.Tipo_Id);
            
            AddColumn("dbo.Transaccions", "TransaccionProyectada_Id", c => c.String());
            AddForeignKey("dbo.Transaccions", "EntradaProyectada_TransaccionProyectada_Id", "dbo.TransaccionProyectadas", "TransaccionProyectada_Id");
            CreateIndex("dbo.Transaccions", "EntradaProyectada_TransaccionProyectada_Id");
            DropColumn("dbo.Transaccions", "EntradaProyectada_Id");
            DropTable("dbo.EntradaProyectadas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EntradaProyectadas",
                c => new
                    {
                        EntradaProyectada_Id = c.Int(nullable: false, identity: true),
                        Cuenta_Id = c.Int(nullable: false),
                        Descripcion = c.String(),
                        Monto = c.Double(nullable: false),
                        Tipo_Id = c.Int(nullable: false),
                        Dia = c.Int(),
                    })
                .PrimaryKey(t => t.EntradaProyectada_Id);
            
            AddColumn("dbo.Transaccions", "EntradaProyectada_Id", c => c.String());
            DropIndex("dbo.TransaccionProyectadas", new[] { "Tipo_Id" });
            DropIndex("dbo.TransaccionProyectadas", new[] { "Cuenta_Id" });
            DropIndex("dbo.Transaccions", new[] { "EntradaProyectada_TransaccionProyectada_Id" });
            DropForeignKey("dbo.TransaccionProyectadas", "Tipo_Id", "dbo.Tipoes");
            DropForeignKey("dbo.TransaccionProyectadas", "Cuenta_Id", "dbo.Cuentas");
            DropForeignKey("dbo.Transaccions", "EntradaProyectada_TransaccionProyectada_Id", "dbo.TransaccionProyectadas");
            DropColumn("dbo.Transaccions", "TransaccionProyectada_Id");
            DropTable("dbo.TransaccionProyectadas");
            RenameColumn(table: "dbo.Transaccions", name: "EntradaProyectada_TransaccionProyectada_Id", newName: "EntradaProyectada_EntradaProyectada_Id");
            CreateIndex("dbo.Transaccions", "EntradaProyectada_EntradaProyectada_Id");
            CreateIndex("dbo.EntradaProyectadas", "Tipo_Id");
            CreateIndex("dbo.EntradaProyectadas", "Cuenta_Id");
            AddForeignKey("dbo.Transaccions", "EntradaProyectada_EntradaProyectada_Id", "dbo.EntradaProyectadas", "EntradaProyectada_Id");
            AddForeignKey("dbo.EntradaProyectadas", "Tipo_Id", "dbo.Tipoes", "Tipo_Id");
            AddForeignKey("dbo.EntradaProyectadas", "Cuenta_Id", "dbo.Cuentas", "Cuenta_Id");
        }
    }
}
