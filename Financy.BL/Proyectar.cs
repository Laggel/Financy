using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financy.Data;
using Financy.DataAccess;

namespace Financy.BL
{
    public static class Proyectar
    {
        public static void Proyecta(FinancyDB db, TransaccionProyectada trans, DateTime lastDate, string user)
        {
            var transacciones = db.Transacciones
                                .Where(w => w.Estado == "Proyección"
                                    && w.TransaccionProyectada_Id == trans.TransaccionProyectada_Id
                                    && w.Cuenta.User == user);

            if (transacciones != null)
            {
                foreach (var transaccion in transacciones)
                {
                    db.Transacciones.Remove(transaccion);
                }
                db.SaveChanges();
            }

            trans = (from x in db.TransaccionProyectadas.Include(x => x.Tipo).Include(x => x.Cuenta)
                     where x.TransaccionProyectada_Id == trans.TransaccionProyectada_Id
                       && x.Cuenta.User == user
                     select x).First();

            var diaActual = lastDate.Day;
            var mesActual = lastDate.Month;
            var anoActual = lastDate.Year;
            var diasEnMes = DateTime.DaysInMonth(anoActual, mesActual);

            if (trans.Tipo.Descripcion == "Diario")
            {

                lastDate = lastDate.AddDays(1);

                var transaccion = new Transaccion()
                {
                    Cuenta = trans.Cuenta,
                    Descripcion = trans.Descripcion,
                    TransaccionProyectada = trans,
                    Estado = "Proyección",
                    Fecha = lastDate,
                    Monto = trans.Monto
                };
                db.Transacciones.Add(transaccion);

            }
            else if (trans.Tipo.Descripcion == "De Lunes a Viernes")
            {

                for (int i = diaActual; i <= diasEnMes; i++)
                {
                    lastDate = lastDate.AddDays(1);

                    if (lastDate.DayOfWeek != DayOfWeek.Saturday && lastDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        var transaccion = new Transaccion()
                        {
                            Cuenta = trans.Cuenta,
                            Descripcion = trans.Descripcion,
                            TransaccionProyectada = trans,
                            Estado = "Proyección",
                            Fecha = lastDate,
                            Monto = trans.Monto
                        };
                        db.Transacciones.Add(transaccion);
                        break;
                    }
                }
            }
            else if (trans.Tipo.Descripcion == "Semanal")
            {
                lastDate = GetNextWeekday(lastDate, (int)trans.Dia);

                var transaccion = new Transaccion()
                {
                    Cuenta = trans.Cuenta,
                    Descripcion = trans.Descripcion,
                    TransaccionProyectada = trans,
                    Estado = "Proyección",
                    Fecha = lastDate,
                    Monto = trans.Monto
                };

                db.Transacciones.Add(transaccion);

            } if (trans.Tipo.Descripcion == "Mensual")
            {
                var transaccion = new Transaccion()
                {
                    Cuenta = trans.Cuenta,
                    Descripcion = trans.Descripcion,
                    TransaccionProyectada = trans,
                    Estado = "Proyección",
                    Monto = trans.Monto
                };

                transaccion.Fecha = new DateTime(anoActual, mesActual, (int)trans.Dia);

                if (transaccion.Fecha <= lastDate)
                    transaccion.Fecha = new DateTime(anoActual, mesActual + 1, (int)trans.Dia);

                db.Transacciones.Add(transaccion);

            }

        }

        public static List<Cuenta> Proyeccion(FinancyDB db, DateTime date, string user)
        {

            var currentDate = DateTime.Now;
            var curDate = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1);
            
            
            while (currentDate < date)
            {
                foreach (var r in db.TransaccionProyectadas.Where(r => r.Cuenta.User == user).ToList())
                {
                    try
                    {
                        ProyectaMes(db, r, currentDate, user);
                    }
                    catch
                    {
                        var z = 9;
                    }
                }

                currentDate = curDate;
                curDate = curDate.AddMonths(1);
            }
            db.SaveChanges();
            CuentaUtil.ActualizaMontos(db, user);
            return CuentaUtil.ActualizaMontosTemp(db, user);
        }

        public static void ProyectaTemp(FinancyDB db, TransaccionProyectada trans, DateTime lastDate, string user)
        {
            var transacciones = db.TransaccionTemps
                                .Where(w => w.Estado == "Proyección"
                                    && w.TransaccionProyectada_Id == trans.TransaccionProyectada_Id
                                    && w.Cuenta.User == user);

            if (transacciones != null)
            {
                foreach (var transaccion in transacciones)
                {
                    db.TransaccionTemps.Remove(transaccion);
                }
                db.SaveChanges();
            }

            trans = (from x in db.TransaccionProyectadas.Include(x => x.Tipo).Include(x => x.Cuenta)
                     where x.TransaccionProyectada_Id == trans.TransaccionProyectada_Id
                       && x.Cuenta.User == user
                     select x).First();

            var diaActual = lastDate.Day;
            var mesActual = lastDate.Month;
            var anoActual = lastDate.Year;
            var diasEnMes = DateTime.DaysInMonth(anoActual, mesActual);

            if (trans.Tipo.Descripcion == "Diario")
            {

                lastDate = lastDate.AddDays(1);

                var transaccion = new TransaccionTemp()
                {
                    Cuenta = trans.Cuenta,
                    Descripcion = trans.Descripcion,
                    TransaccionProyectada = trans,
                    Estado = "Proyección",
                    Fecha = lastDate,
                    Monto = trans.Monto
                };
                db.TransaccionTemps.Add(transaccion);

            }
            else if (trans.Tipo.Descripcion == "De Lunes a Viernes")
            {

                for (int i = diaActual; i <= diasEnMes; i++)
                {
                    lastDate = lastDate.AddDays(1);

                    if (lastDate.DayOfWeek != DayOfWeek.Saturday && lastDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        var transaccion = new TransaccionTemp()
                        {
                            Cuenta = trans.Cuenta,
                            Descripcion = trans.Descripcion,
                            TransaccionProyectada = trans,
                            Estado = "Proyección",
                            Fecha = lastDate,
                            Monto = trans.Monto
                        };
                        db.TransaccionTemps.Add(transaccion);
                        break;
                    }
                }
            }
            else if (trans.Tipo.Descripcion == "Semanal")
            {
                lastDate = GetNextWeekday(lastDate, (int)trans.Dia);

                var transaccion = new TransaccionTemp()
                {
                    Cuenta = trans.Cuenta,
                    Descripcion = trans.Descripcion,
                    TransaccionProyectada = trans,
                    Estado = "Proyección",
                    Fecha = lastDate,
                    Monto = trans.Monto
                };

                db.TransaccionTemps.Add(transaccion);

            } if (trans.Tipo.Descripcion == "Mensual")
            {
                var transaccion = new TransaccionTemp()
                {
                    Cuenta = trans.Cuenta,
                    Descripcion = trans.Descripcion,
                    TransaccionProyectada = trans,
                    Estado = "Proyección",
                    Monto = trans.Monto
                };

                transaccion.Fecha = new DateTime(anoActual, mesActual, (int)trans.Dia);

                if (transaccion.Fecha <= lastDate)
                    transaccion.Fecha = new DateTime(anoActual, mesActual + 1, (int)trans.Dia);

                db.TransaccionTemps.Add(transaccion);

            }

        }

        public static bool ProyectaMes(FinancyDB db, TransaccionProyectada trans, DateTime date, string user)
        {
            var transacciones = db.TransaccionTemps
                                .Where(w => w.Estado == "Proyección"
                                    && w.TransaccionProyectada_Id == trans.TransaccionProyectada_Id);

            if (transacciones != null)
            {
                foreach (var transaccion in transacciones)
                {
                    db.TransaccionTemps.Remove(transaccion);
                }
                db.SaveChanges();
            }

            trans = (from x in db.TransaccionProyectadas.Include(x => x.Tipo).Include(x => x.Cuenta)
                     where x.TransaccionProyectada_Id == trans.TransaccionProyectada_Id
                     && x.Cuenta.User == user
                     select x).First();

            var lastDate = date;
            var diaActual = lastDate.Day;
            var mesActual = lastDate.Month;
            var anoActual = lastDate.Year;
            var diasEnMes = DateTime.DaysInMonth(anoActual, mesActual);

            if (trans.Tipo.Descripcion == "Diario")
            {

                for (int i = diaActual; i <= diasEnMes; i++)
                {
                    var transaccion = new TransaccionTemp()
                    {
                        Cuenta = trans.Cuenta,
                        Descripcion = trans.Descripcion,
                        TransaccionProyectada = trans,
                        Estado = "Proyección",
                        Fecha = lastDate,
                        Monto = trans.Monto
                    };
                    db.TransaccionTemps.Add(transaccion);
                    lastDate = lastDate.AddDays(1);
                }
            }
            else if (trans.Tipo.Descripcion == "De Lunes a Viernes")
            {

                for (int i = diaActual; i <= diasEnMes; i++)
                {
                    if (lastDate.DayOfWeek != DayOfWeek.Saturday && lastDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        var transaccion = new TransaccionTemp()
                        {
                            Cuenta = trans.Cuenta,
                            Descripcion = trans.Descripcion,
                            TransaccionProyectada = trans,
                            Estado = "Proyección",
                            Fecha = lastDate,
                            Monto = trans.Monto
                        };
                        db.TransaccionTemps.Add(transaccion);
                    }

                    lastDate = lastDate.AddDays(1);

                }
            }
            else if (trans.Tipo.Descripcion == "Semanal")
            {
                lastDate = GetNextWeekday(lastDate, (int)trans.Dia);

                while (mesActual == lastDate.Month)
                {

                    var transaccion = new TransaccionTemp()
                    {
                        Cuenta = trans.Cuenta,
                        Descripcion = trans.Descripcion,
                        TransaccionProyectada = trans,
                        Estado = "Proyección",
                        Fecha = lastDate,
                        Monto = trans.Monto
                    };

                    db.TransaccionTemps.Add(transaccion);

                    lastDate = GetNextWeekday(lastDate, (int)trans.Dia);

                }

            } if (trans.Tipo.Descripcion == "Mensual")
            {
                var fecha = new DateTime(anoActual, mesActual, (int)trans.Dia);

                if (fecha > lastDate)
                {
                    var transaccion = new TransaccionTemp()
                    {
                        Cuenta = trans.Cuenta,
                        Descripcion = trans.Descripcion,
                        TransaccionProyectada = trans,
                        Fecha = fecha,
                        Estado = "Proyección",
                        Monto = trans.Monto
                    };

                    db.TransaccionTemps.Add(transaccion);
                }
                
            }
            db.SaveChanges();
            return true;

        }

        public static DateTime GetNextWeekday(DateTime start, int day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7);
            return start.AddDays(daysToAdd);
        }
    }
}
