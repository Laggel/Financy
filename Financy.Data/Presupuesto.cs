using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Financy.Data
{
    public class Presupuesto
    {
        public int Presupuesto_Id { get; set; }

        public string Descripcion { get; set; }

        [DataType(DataType.Currency)]
        public double Gastado { get; set; }

        [DataType(DataType.Currency)]
        public double Limite { get; set; }
    }
}
