using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Financy.Data
{
    public class Cuenta
    {
        [Key]
        public int Cuenta_Id { get; set; }

        public string Descripcion { get; set; }

        public double Monto { get; set; }
    }
}
