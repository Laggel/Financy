using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Financy.Data;
using Financy.DataAccess;
using Financy.BL;
using Financy.Models;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DData = DotNet.Highcharts.Helpers.Data;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using System.Drawing;

namespace Financy.Controllers
{
    public class ProyeccionController : Controller
    {
        private FinancyDB db = new FinancyDB();

        //
        // GET: /Proyeccion/

        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(ProyeccionModel model)
        {
            var fec = model.Fecha.Subtract(DateTime.Now).TotalDays;
            var mes = (int)Math.Ceiling(model.Fecha.Subtract(DateTime.Now).TotalDays / (365.25 / 12));

            if (model.Fecha > DateTime.Now)
            {
                model.Cuentas = Proyectar.Proyeccion(db, DateTime.Now.AddMonths(mes), User.Identity.Name);

                model.Chart = GraphBudget(model.Fecha);
            }
            else
            {
                ModelState.AddModelError("Model.Fecha", "Favor elegir una fecha mayor al día de hoy.");
            }
            return View(model);
        }

        public Highcharts Graph()
        {
            var startDate = DateTime.Now;

            var datos = (from q in db.Cuentas
                         where q.User == User.Identity.Name
                         select new { fecha = startDate, monto = q.Monto })
                        .Union
                        (from q in db.Transacciones
                         where q.Cuenta.User == User.Identity.Name
                         select new { fecha = q.Fecha, monto = q.Monto })
                         .Union
                         (from q in db.TransaccionTemps
                          where q.Cuenta.User == User.Identity.Name
                          select new { fecha = q.Fecha, monto = q.Monto })
                          .OrderBy(q => q.fecha);

            //var fechas = new List<string>();
            //foreach (var dato in datos)
            //{
            //    fechas.Add(dato.fecha.ToShortDateString()); 
            //}

            //var Categories = fechas.ToArray(); 
            //new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            var valores = new List<object>();
            var balance = 0.0;
            var fecha = startDate;
            foreach (var dato in datos)
            {
                if (fecha == dato.fecha)
                {
                    balance += (double)dato.monto;
                }
                else
                {
                    valores.Add(balance);
                    fecha = dato.fecha;
                    balance += (double)dato.monto;
                }
            }
            var Data = new DData(valores.ToArray());
            

            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
                .SetOptions(new GlobalOptions { Global = new Global { UseUTC = false } })
                .InitChart(new Chart { ZoomType = ZoomTypes.X, SpacingRight = 20 })
                .SetTitle(new Title { Text = "Flujo de Efectivo" })
                .SetSubtitle(new Subtitle { Text = "Proyección" })
                .SetXAxis(new XAxis
                {
                    Type = AxisTypes.Datetime,
                    MinRange = 24 * 3600 * 1000,
                    Title = new XAxisTitle { Text = "RD$" }
                })
                //.SetXAxis(new XAxis
                //{
                //    Title = new XAxisTitle { Text = "RD$" },
                //    Categories = Categories
                //})
                .SetPlotOptions(new PlotOptions
                {
                    Series = new PlotOptionsSeries
                    {
                        Marker = new PlotOptionsSeriesMarker
                        {
                            Enabled = false,
                            States = new PlotOptionsSeriesMarkerStates
                            {
                                Hover = new PlotOptionsSeriesMarkerStatesHover
                                {
                                    Enabled = true,
                                    Radius = 5
                                }
                            }
                        },
                        //Shadow = false,
                        //States = new PlotOptionsAreaStates { Hover = new PlotOptionsAreaStatesHover { LineWidth = 1 } },
                        PointInterval = 24 * 3600 * 1000,
                        PointStart = new PointStart(datos.First().fecha)
                    }
                })
                .SetSeries(new Series
                {
                    Type = ChartTypes.Line,
                    Name = "USD to EUR",
                    Data = Data
                })
                //.SetSeries(new Series
                //{
                //    Data = Data
                //})
                ;

            return chart;
        }

        public Highcharts GraphBudget(DateTime fechaAsked)
        {
            var mes = (int)Math.Floor(fechaAsked.Subtract(DateTime.Now).TotalDays / (365.25 / 12));

            var startDate = DateTime.Now;

            var actualBalance = (from q in db.Cuentas
                          where q.User == User.Identity.Name
                                 select (double?)q.Monto).Sum() ?? 0;

            var income = (from q in db.TransaccionProyectadas
                          where q.Cuenta.User == User.Identity.Name
                          && q.Monto > 0
                          select (double?)q.Monto).Sum() ?? 0;

           var outcome = (from q in db.TransaccionProyectadas
                          where q.Cuenta.User == User.Identity.Name
                          && q.Monto <= 0
                          select (double?)q.Monto).Sum() ?? 0;

           //var fechas = new List<string>();
           //var fechas2 = new List<string>();
           //fechas.Add(startDate.ToShortDateString());
           //var lastDate = startDate;
           
           //for (int i = 0; i < mes; i++)
           //{
           //    lastDate = lastDate.AddMonths(1);

           //    var fecIng = new DateTime(lastDate.Year, lastDate.Month, 1);
           //    fechas.Add(fecIng.ToShortDateString());

           //    var fecSal = new DateTime(lastDate.Year, lastDate.Month, 15);
           //    fechas.Add(fecSal.ToShortDateString());
           //}
           
           //var Categories = fechas.ToArray();
           
           // var valores = new List<object>();
           // valores.Add(actualBalance);

           //var balance = actualBalance;
            //var fecha = startDate;

            //lastDate = startDate;

            //for (int i = 0; i < mes; i++)
            //{
            //    balance += income;
            //    valores.Add(balance);

            //    balance += outcome;
            //    valores.Add(balance);
            //}
            
            //var Data = new DData(valores.ToArray());

            object[,] Balance1 = new object[2 * mes + 2, 2];

            //if (fechaAsked.Day >= 15)
            //{
            //    Balance1 = new object[2 * mes + 2, 2];
            //}
            //else
            //{
            //    Balance1 = new object[2 * mes + 1, 2];
            //}

            Balance1[0, 0] = startDate;
            Balance1[0, 1] = actualBalance;

            var lastDate = startDate;
            var balance = actualBalance;

            var counter = 1;
            
            for (int i = 1; i <= mes; i++)
            {
                lastDate = lastDate.AddMonths(1);

                balance += income;
                Balance1[counter, 0] = new DateTime(lastDate.Year, lastDate.Month, 1);
                Balance1[counter, 1] = balance;
                counter++;
                
                balance += outcome;
                Balance1[counter, 0] = new DateTime(lastDate.Year, lastDate.Month, 15);
                Balance1[counter, 1] = balance;
                counter++;
                
            }

            Balance1[counter, 0] = fechaAsked;
            Balance1[counter, 1] = balance;

            var Gastos = new DData(Balance1);


            Series Flujo = new Series
                                              {
                                                  Name = "Flujo Efectivo",
                                                  Data = Gastos
                                              };

        

            Highcharts chart = new Highcharts("chart")
                                                    .InitChart(new Chart { DefaultSeriesType = ChartTypes.Spline })
                                                    .SetOptions(new GlobalOptions { Global = new Global { UseUTC = false } })
                                                    .SetTitle(new Title { Text = "Proyección a: " + fechaAsked.ToShortDateString() })
                                                    .SetSubtitle(new Subtitle { Text = "En base a los ingresos y el presupuesto." })
                                                    .SetXAxis(new XAxis
                                                    {
                                                        Type = AxisTypes.Datetime,
                                                        DateTimeLabelFormats = new DateTimeLabel { Month = "%e. %b", Year = "%b" }
                                                    })
                                                    .SetYAxis(new YAxis
                                                    {
                                                        Title = new XAxisTitle { Text = "Monto ($)" },
                                                        Min = 0
                                                    })
                                                    .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.series.name +'</b><br/>'+ Highcharts.dateFormat('%e %b', this.x) +': $'+ Highcharts.numberFormat(this.y, 0) +' '; }" })
                                                    .SetSeries(new[] { Flujo });


            //DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
            //    .SetOptions(new GlobalOptions { Global = new Global { UseUTC = false } })
            //    .InitChart(new Chart { ZoomType = ZoomTypes.X, SpacingRight = 20 })
            //    .SetTitle(new Title { Text = "Flujo de Efectivo" })
            //    .SetSubtitle(new Subtitle { Text = "Proyección" })
            //    .SetXAxis(new XAxis
            //    {
            //        Type = AxisTypes.Linear,
            //        //MinRange = 24 * 3600 * 1000,
            //        Categories = Categories,
            //        Title = new XAxisTitle { Text = "Fecha" }
            //    })
            //    //.SetXAxis(new XAxis
            //    //{
            //    //    Title = new XAxisTitle { Text = "RD$" },
            //    //    Categories = Categories
            //    //})
            //    //.SetPlotOptions(new PlotOptions
            //    //{
            //    //    Series = new PlotOptionsSeries
            //    //    {
            //    //        Marker = new PlotOptionsSeriesMarker
            //    //        {
            //    //            Enabled = true,
            //    //            States = new PlotOptionsSeriesMarkerStates
            //    //            {
            //    //                Hover = new PlotOptionsSeriesMarkerStatesHover
            //    //                {
            //    //                    Enabled = true,
            //    //                    Radius = 5
            //    //                }
            //    //            }
            //    //        },
            //    //        //Shadow = false,
            //    //        //States = new PlotOptionsAreaStates { Hover = new PlotOptionsAreaStatesHover { LineWidth = 1 } },
            //    //        PointInterval = 24 * 3600 * 1000,
            //    //        PointStart = new PointStart(startDate)
            //    //    }
            //    //})
            //    //.SetSeries(new Series
            //    //{
            //    //    Type = ChartTypes.Line,
            //    //    Name = "USD to EUR",
            //    //    Data = Data
            //    //})
            //    .SetSeries(new Series
            //    {
            //        Type = ChartTypes.Line,
            //        Name = "USD to EUR",
            //        Data = Data
            //    })
            //    ;

            return chart;
        }


        //
        // GET: /Proyeccion/Details/5

        public ActionResult Details(int id = 0)
        {
            TransaccionTemp transacciontemp = db.TransaccionTemps.Find(id);
            if (transacciontemp == null)
            {
                return HttpNotFound();
            }
            return View(transacciontemp);
        }

        //
        // GET: /Proyeccion/Create

        public ActionResult Create()
        {
            ViewBag.Cuenta_Id = new SelectList(db.Cuentas, "Cuenta_Id", "Descripcion");
            ViewBag.TransaccionProyectada_Id = new SelectList(db.TransaccionProyectadas, "TransaccionProyectada_Id", "Descripcion");
            return View();
        }

        //
        // POST: /Proyeccion/Create

        [HttpPost]
        public ActionResult Create(TransaccionTemp transacciontemp)
        {
            if (ModelState.IsValid)
            {
                db.TransaccionTemps.Add(transacciontemp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cuenta_Id = new SelectList(db.Cuentas, "Cuenta_Id", "Descripcion", transacciontemp.Cuenta_Id);
            ViewBag.TransaccionProyectada_Id = new SelectList(db.TransaccionProyectadas, "TransaccionProyectada_Id", "Descripcion", transacciontemp.TransaccionProyectada_Id);
            return View(transacciontemp);
        }

        //
        // GET: /Proyeccion/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TransaccionTemp transacciontemp = db.TransaccionTemps.Find(id);
            if (transacciontemp == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cuenta_Id = new SelectList(db.Cuentas, "Cuenta_Id", "Descripcion", transacciontemp.Cuenta_Id);
            ViewBag.TransaccionProyectada_Id = new SelectList(db.TransaccionProyectadas, "TransaccionProyectada_Id", "Descripcion", transacciontemp.TransaccionProyectada_Id);
            return View(transacciontemp);
        }

        //
        // POST: /Proyeccion/Edit/5

        [HttpPost]
        public ActionResult Edit(TransaccionTemp transacciontemp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transacciontemp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cuenta_Id = new SelectList(db.Cuentas, "Cuenta_Id", "Descripcion", transacciontemp.Cuenta_Id);
            ViewBag.TransaccionProyectada_Id = new SelectList(db.TransaccionProyectadas, "TransaccionProyectada_Id", "Descripcion", transacciontemp.TransaccionProyectada_Id);
            return View(transacciontemp);
        }

        //
        // GET: /Proyeccion/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TransaccionTemp transacciontemp = db.TransaccionTemps.Find(id);
            if (transacciontemp == null)
            {
                return HttpNotFound();
            }
            return View(transacciontemp);
        }

        //
        // POST: /Proyeccion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TransaccionTemp transacciontemp = db.TransaccionTemps.Find(id);
            db.TransaccionTemps.Remove(transacciontemp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}