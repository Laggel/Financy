using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Financy.DataAccess;
using Financy.Data;

namespace Financy.BL
{
    public static class Balance
    {
        public static List<Presupuesto> GetPresupuesto(FinancyDB db, DateTime date, string user)
        {
            var presupuestos = new List<Presupuesto>();

            foreach (var pres in db.TransaccionProyectadas.Include(x => x.Tipo).Where(x => x.Monto <= 0 && x.Cuenta.User == user).ToList())
            {
                var consumido = (from r in db.Transacciones
                                 where r.TransaccionProyectada_Id == pres.TransaccionProyectada_Id
                                 && r.Fecha.Month == date.Month
                                 && r.Cuenta.User == user
                                 select (double?)r.Monto).Sum() ?? 0;
                
                var limite = pres.Monto * pres.Tipo.Cantidad;

                presupuestos.Add(new Presupuesto()
                                                 {Presupuesto_Id = pres.TransaccionProyectada_Id,
                                                  Descripcion = pres.Descripcion,
                                                  Limite = Math.Abs(limite),
                                                  Gastado = Math.Abs(consumido)});
            }

            return presupuestos;
        }
    }
}
