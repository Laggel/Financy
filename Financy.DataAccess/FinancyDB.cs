using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Financy.Data;
using Financy.DataAccess.Migrations;

namespace Financy.DataAccess
{
    public class FinancyDB : DbContext
    {

        public FinancyDB()
            : base("FinancyDB")
        {
            //Database.SetInitializer<FinancyDB>(null);
            Database.SetInitializer(new CreateDatabaseIfNotExists<FinancyDB>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<FinancyDB, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<TransaccionTemp> TransaccionTemps { get; set; }
        public DbSet<TransaccionProyectada> TransaccionProyectadas { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        
    }
}
