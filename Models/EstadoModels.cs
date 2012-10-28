using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Financy.Data;
using Financy.BL;

namespace Financy.Models
{
    public class EstadoModel
    {
        public List<Cuenta> Cuentas { get; set; }
        public List<Presupuesto> Presupuesto { get; set; }
    }

}