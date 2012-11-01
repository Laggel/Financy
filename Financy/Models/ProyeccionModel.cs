using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Financy.Models
{
    public class ProyeccionModel
    {
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public List<Financy.Data.Cuenta> Cuentas { get; set; }

        public DotNet.Highcharts.Highcharts Chart { get; set; }

    }
}