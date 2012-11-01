namespace Financy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTransTemp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransaccionTemps",
                c => new
                    {
                        Transaccion_Id = c.Int(nullable: false, identity: true),
                        Cuenta_Id = c.Int(nullable: false),
                        Descripcion = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        Monto = c.Double(nullable: false),
                        TransaccionProyectada_Id = c.Int(nullable: false),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Transaccion_Id)
                .ForeignKey("dbo.Cuentas", t => t.Cuenta_Id)
                .ForeignKey("dbo.TransaccionProyectadas", t => t.TransaccionProyectada_Id)
                .Index(t => t.Cuenta_Id)
                .Index(t => t.TransaccionProyectada_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TransaccionTemps", new[] { "TransaccionProyectada_Id" });
            DropIndex("dbo.TransaccionTemps", new[] { "Cuenta_Id" });
            DropForeignKey("dbo.TransaccionTemps", "TransaccionProyectada_Id", "dbo.TransaccionProyectadas");
            DropForeignKey("dbo.TransaccionTemps", "Cuenta_Id", "dbo.Cuentas");
            DropTable("dbo.TransaccionTemps");
        }
    }
}
