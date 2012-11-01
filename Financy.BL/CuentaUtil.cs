using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financy.Data;
using Financy.DataAccess;

namespace Financy.BL
{
    public class CuentaUtil
    {
        public static void ActualizaMontos(FinancyDB db, string user)
        {
            var list = db.Cuentas.Where(r => r.User == user).ToList();
            foreach (var cuenta in list)
            {
                var monto = cuenta.Monto;

                var transacciones = (from r in db.Transacciones
                                     where r.Cuenta_Id == cuenta.Cuenta_Id
                                     && r.Fecha < DateTime.Now
                                     select (double?)r.Monto).Sum() ?? 0;

                cuenta.Balanc = monto + transacciones;
            }

            db.SaveChanges();
        }

        public static List<Cuenta> ActualizaMontosTemp(FinancyDB db, string user)
        {
            var list = db.Cuentas.AsNoTracking().Where(r => r.User == user).ToList();
            foreach (var cuenta in list)
            {
                var monto = cuenta.Monto;

                var transacciones = (from r in db.TransaccionTemps
                                     where r.Cuenta_Id == cuenta.Cuenta_Id
                                     select (double?)r.Monto).Sum() ?? 0;

                cuenta.Balanc = monto + transacciones;
            }

            return list;
            //db.SaveChanges();
        }
    }
}
