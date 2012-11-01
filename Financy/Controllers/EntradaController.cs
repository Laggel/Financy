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

namespace Financy.Controllers
{
    public class EntradaController : Controller
    {
        private FinancyDB db = new FinancyDB();

        //
        // GET: /Entrada/

        public ActionResult Index()
        {
            var transaccionproyectadas = (from r in db.TransaccionProyectadas.Include(t => t.Cuenta).Include(t => t.Tipo)
                                          where r.Monto > 0
                                          && r.Cuenta.User == User.Identity.Name
                                          select r);

            return View(transaccionproyectadas.ToList());
        }

        //
        // GET: /Entrada/Details/5

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
        // GET: /Entrada/Create

        public ActionResult Create()
        {
            ViewBag.Cuenta_Id = new SelectList(db.Cuentas.Where(r => r.User == User.Identity.Name), "Cuenta_Id", "Descripcion");
            ViewBag.Tipo_Id = new SelectList(db.Tipos, "Tipo_Id", "Descripcion");
            return View();
        }

        //
        // POST: /Entrada/Create

        [HttpPost]
        public ActionResult Create(TransaccionProyectada transaccionproyectada)
        {
            if (ModelState.IsValid)
            {
                transaccionproyectada.Tipo = db.Tipos.ToList().Find(x => x.Descripcion == "Mensual");
                db.TransaccionProyectadas.Add(transaccionproyectada);
                db.SaveChanges();
                Proyectar.Proyecta(db, transaccionproyectada, DateTime.Now, User.Identity.Name);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Cuenta_Id = new SelectList(db.Cuentas.Where(r => r.User == User.Identity.Name), "Cuenta_Id", "Descripcion", transaccionproyectada.Cuenta_Id);
            ViewBag.Tipo_Id = new SelectList(db.Tipos, "Tipo_Id", "Descripcion", transaccionproyectada.Tipo_Id);
            return View(transaccionproyectada);
        }

        //
        // GET: /Entrada/Edit/5

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
        // POST: /Entrada/Edit/5

        [HttpPost]
        public ActionResult Edit(TransaccionProyectada transaccionproyectada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaccionproyectada).State = EntityState.Modified;
                db.SaveChanges();
                Proyectar.Proyecta(db, transaccionproyectada, DateTime.Now, User.Identity.Name);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Cuenta_Id = new SelectList(db.Cuentas.Where(r => r.User == User.Identity.Name), "Cuenta_Id", "Descripcion", transaccionproyectada.Cuenta_Id);
            ViewBag.Tipo_Id = new SelectList(db.Tipos, "Tipo_Id", "Descripcion", transaccionproyectada.Tipo_Id);
            return View(transaccionproyectada);
        }

        //
        // GET: /Entrada/Delete/5

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
        // POST: /Entrada/Delete/5

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