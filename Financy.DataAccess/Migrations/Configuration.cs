
namespace Financy.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Financy.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<Financy.DataAccess.FinancyDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(Financy.DataAccess.FinancyDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Tipos.AddOrUpdate(
              p => p.Descripcion,
              new Tipo { Descripcion = "Diario", Cantidad = 30, Legend = "cada día." },
              new Tipo { Descripcion = "Semanal", Cantidad = 20, Legend = "de cada semana." },
              new Tipo { Descripcion = "Mensual", Cantidad = 1, Legend ="de cada mes." },
              new Tipo { Descripcion = "De Lunes a Viernes", Cantidad = 20, Legend = "cada día Laborable."}
            );
            
        }
    }
}
