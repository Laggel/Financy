using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Financy.Data;
using Financy.DataAccess;
using Financy.BL;

namespace Financy.Controllers
{
    public class HomeController : Controller
    {
        private FinancyDB db = new FinancyDB();


        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var Estado = new Financy.Models.EstadoModel();

            CuentaUtil.ActualizaMontos(db, User.Identity.Name);

            Estado.Cuentas = (from r in db.Cuentas
                              where r.User == User.Identity.Name
                              select r).ToList();

            Estado.Presupuesto = Balance.GetPresupuesto(db, DateTime.Now, User.Identity.Name);

            return View(Estado);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
