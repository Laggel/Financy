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

namespace Financy.Controllers
{
    public class ProyeccionController : Controller
    {
        private FinancyDB db = new FinancyDB();

        //
        // GET: /Proyeccion/

        public ActionResult Index()
        {
            return View(Proyectar.Proyeccion(db, DateTime.Now.AddMonths(3), User.Identity.Name));
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