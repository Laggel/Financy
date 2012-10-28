using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Financy.Data;
using Financy.DataAccess;
using System.Web.Security;

namespace Financy.Controllers
{
    public class CuentaController : Controller
    {
        private FinancyDB db = new FinancyDB();

        //
        // GET: /Cuenta/

        public ActionResult Index()
        {
            return View(db.Cuentas.Where(x=>x.User == User.Identity.Name).ToList());
        }

        //
        // GET: /Cuenta/Details/5

        public ActionResult Details(int id = 0)
        {
            Cuenta cuenta = db.Cuentas.Find(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            return View(cuenta);
        }

        //
        // GET: /Cuenta/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cuenta/Create

        [HttpPost]
        public ActionResult Create(Cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                cuenta.User = User.Identity.Name;
                db.Cuentas.Add(cuenta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cuenta);
        }

        //
        // GET: /Cuenta/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Cuenta cuenta = db.Cuentas.Find(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            return View(cuenta);
        }

        //
        // POST: /Cuenta/Edit/5

        [HttpPost]
        public ActionResult Edit(Cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuenta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cuenta);
        }

        //
        // GET: /Cuenta/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Cuenta cuenta = db.Cuentas.Find(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            return View(cuenta);
        }

        //
        // POST: /Cuenta/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Cuenta cuenta = db.Cuentas.Find(id);
            db.Cuentas.Remove(cuenta);
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