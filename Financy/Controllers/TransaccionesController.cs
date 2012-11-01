using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Financy.Data;
using Financy.DataAccess;

namespace Financy.Controllers
{
    public class TransaccionesController : Controller
    {
        private FinancyDB db = new FinancyDB();

        //
        // GET: /Transacciones/

        public ActionResult Index()
        {
            var transacciones = db.Transacciones
                                .Include(t => t.Cuenta)
                                .Include(t => t.TransaccionProyectada)
                                .Where(r => r.Cuenta.User == User.Identity.Name)
                                .OrderByDescending(r => r.Fecha);
            return View(transacciones.ToList());
        }

        //
        // GET: /Transacciones/Details/5

        public ActionResult Details(int id = 0)
        {
            Transaccion transaccion = db.Transacciones.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            return View(transaccion);
        }

        //
        // GET: /Transacciones/Create

        public ActionResult Create()
        {
            ViewBag.Cuenta_Id = new SelectList(db.Cuentas.Where(r => r.User == User.Identity.Name), "Cuenta_Id", "Descripcion");
            return View();
        }

        //
        // POST: /Transacciones/Create

        [HttpPost]
        public ActionResult Create(Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                db.Transacciones.Add(transaccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cuenta_Id = new SelectList(db.Cuentas.Where(r => r.User == User.Identity.Name), "Cuenta_Id", "Descripcion", transaccion.Cuenta_Id);
            return View(transaccion);
        }

        //
        // GET: /Transacciones/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Transaccion transaccion = db.Transacciones.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cuenta_Id = new SelectList(db.Cuentas.Where(r => r.User == User.Identity.Name), "Cuenta_Id", "Descripcion", transaccion.Cuenta_Id);
            return View(transaccion);
        }

        //
        // POST: /Transacciones/Edit/5

        [HttpPost]
        public ActionResult Edit(Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cuenta_Id = new SelectList(db.Cuentas.Where(r => r.User == User.Identity.Name), "Cuenta_Id", "Descripcion", transaccion.Cuenta_Id);
            return View(transaccion);
        }

        //
        // GET: /Transacciones/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Transaccion transaccion = db.Transacciones.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            return View(transaccion);
        }

        //
        // POST: /Transacciones/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaccion transaccion = db.Transacciones.Find(id);
            db.Transacciones.Remove(transaccion);
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