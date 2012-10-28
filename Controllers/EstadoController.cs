using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Financy.Data;
using Financy.DataAccess;
using Financy.Models;
using Financy.BL;

namespace Financy.Controllers
{
    public class EstadoController : Controller
    {
        private FinancyDB db = new FinancyDB();

        //
        // GET: /Estado/

        public ActionResult Index()
        {
            var Estado = new EstadoModel();

            CuentaUtil.ActualizaMontos(db, User.Identity.Name);

            Estado.Cuentas = (from r in db.Cuentas
                              where r.User == User.Identity.Name
                                select r).ToList();

            Estado.Presupuesto = Balance.GetPresupuesto(db, DateTime.Now, User.Identity.Name);

            ViewBag.Ingresos = db.Transacciones.Where(x => x.Monto > 0).Sum(x => x.Monto).ToString("C0"); 
            ViewBag.Gastado = Estado.Presupuesto.Sum(x => x.Gastado).ToString("C0");
            ViewBag.Limite = Estado.Presupuesto.Sum(x => x.Limite).ToString("C0");

            return View(Estado);
        }

        //
        // GET: /Estado/Details/5

        public ActionResult Details(int id = 0)
        {
            TransaccionProyectada transaccionproyectada = db.TransaccionProyectadas.Find(id);
            if (transaccionproyectada == null)
            {
                return HttpNotFound();
            }
            return View(transaccionproyectada);
        }

        //
        // GET: /Estado/Create

        public ActionResult Create()
        {
            ViewBag.Cuenta_Id = new SelectList(db.Cuentas.Where(r => r.User == User.Identity.Name), "Cuenta_Id", "Descripcion");
            ViewBag.Tipo_Id = new SelectList(db.Tipos, "Tipo_Id", "Descripcion");
            return View();
        }

        //
        // POST: /Estado/Create

        [HttpPost]
        public ActionResult Create(TransaccionProyectada transaccionproyectada)
        {
            if (ModelState.IsValid)
            {
                db.TransaccionProyectadas.Add(transaccionproyectada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cuenta_Id = new SelectList(db.Cuentas.Where(r => r.User == User.Identity.Name), "Cuenta_Id", "Descripcion", transaccionproyectada.Cuenta_Id);
            ViewBag.Tipo_Id = new SelectList(db.Tipos, "Tipo_Id", "Descripcion", transaccionproyectada.Tipo_Id);
            return View(transaccionproyectada);
        }

        //
        // GET: /Estado/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TransaccionProyectada transaccionproyectada = db.TransaccionProyectadas.Find(id);
            if (transaccionproyectada == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cuenta_Id = new SelectList(db.Cuentas.Where(r => r.User == User.Identity.Name), "Cuenta_Id", "Descripcion", transaccionproyectada.Cuenta_Id);
            ViewBag.Tipo_Id = new SelectList(db.Tipos, "Tipo_Id", "Descripcion", transaccionproyectada.Tipo_Id);
            return View(transaccionproyectada);
        }

        //
        // POST: /Estado/Edit/5

        [HttpPost]
        public ActionResult Edit(TransaccionProyectada transaccionproyectada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaccionproyectada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cuenta_Id = new SelectList(db.Cuentas.Where(r => r.User == User.Identity.Name), "Cuenta_Id", "Descripcion", transaccionproyectada.Cuenta_Id);
            ViewBag.Tipo_Id = new SelectList(db.Tipos, "Tipo_Id", "Descripcion", transaccionproyectada.Tipo_Id);
            return View(transaccionproyectada);
        }

        //
        // GET: /Estado/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TransaccionProyectada transaccionproyectada = db.TransaccionProyectadas.Find(id);
            if (transaccionproyectada == null)
            {
                return HttpNotFound();
            }
            return View(transaccionproyectada);
        }

        //
        // POST: /Estado/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TransaccionProyectada transaccionproyectada = db.TransaccionProyectadas.Find(id);
            db.TransaccionProyectadas.Remove(transaccionproyectada);
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