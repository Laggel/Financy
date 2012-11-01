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
    public class TipoController : Controller
    {
        private FinancyDB db = new FinancyDB();

        //
        // GET: /Tipo/

        public ActionResult Index()
        {
            return View(db.Tipos.ToList());
        }

        //
        // GET: /Tipo/Details/5

        public ActionResult Details(int id = 0)
        {
            Tipo tipo = db.Tipos.Find(id);
            if (tipo == null)
            {
                return HttpNotFound();
            }
            return View(tipo);
        }

        //
        // GET: /Tipo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Tipo/Create

        [HttpPost]
        public ActionResult Create(Tipo tipo)
        {
            if (ModelState.IsValid)
            {
                db.Tipos.Add(tipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo);
        }

        //
        // GET: /Tipo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Tipo tipo = db.Tipos.Find(id);
            if (tipo == null)
            {
                return HttpNotFound();
            }
            return View(tipo);
        }

        //
        // POST: /Tipo/Edit/5

        [HttpPost]
        public ActionResult Edit(Tipo tipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo);
        }

        //
        // GET: /Tipo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Tipo tipo = db.Tipos.Find(id);
            if (tipo == null)
            {
                return HttpNotFound();
            }
            return View(tipo);
        }

        //
        // POST: /Tipo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo tipo = db.Tipos.Find(id);
            db.Tipos.Remove(tipo);
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